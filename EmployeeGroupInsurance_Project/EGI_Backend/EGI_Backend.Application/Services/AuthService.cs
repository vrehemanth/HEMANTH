using AutoMapper;
using EGI_Backend.Application.Contracts.Auth;
using EGI_Backend.Application.Interfaces;
using EGI_Backend.Application.Exceptions;
using EGI_Backend.Domain.Entities;
using EGI_Backend.Domain.Enums;
using System.Linq;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EGI_Backend.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _repo;
        private readonly IJwtTokenGenerator _jwt;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly ICorporateClientRepository _clientRepo;
        private readonly INotificationService _notificationService;

        private string GenerateTemporaryPassword()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, 10)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public AuthService(IUserRepository repo, IJwtTokenGenerator jwt, IMapper mapper, IEmailService emailService, ICorporateClientRepository clientRepo, INotificationService notificationService)
        {
            _repo = repo;
            _jwt = jwt;
            _mapper = mapper;
            _emailService = emailService;
            _clientRepo = clientRepo;
            _notificationService = notificationService;
        }

        public async Task<AuthResponse> Register(RegisterRequest req)
        {
            var existingUser = await _repo.GetByEmailAsync(req.Email);
            if (existingUser != null)
                throw new ConflictException("Email already exists");

            var user = _mapper.Map<User>(req);
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(req.Password);
            await _repo.AddAsync(user);
            if (user.Role == UserRole.Customer)
            {
                var client = new CorporateClient
                {
                    UserId = user.Id,
                    Status = VerificationStatus.Draft,
                    IsBlocked = false
                };

                await _clientRepo.AddAsync(client);
            }
            return _jwt.GenerateToken(user);
        }

        public async Task<AuthResponse> Login(LoginRequest req)
        {
            var user = await _repo.GetByEmailAsync(req.Email);
            if (user == null)
                throw new UnauthorizedException("Invalid Credentials");

            var validUser = BCrypt.Net.BCrypt.Verify(req.Password, user.PasswordHash);
            if (!validUser)
                throw new UnauthorizedException("Invalid Credentials");

            // Allow Inactive users to login (needed for customers whose profile is Rejected/Draft)
            // But Block Suspended users
            if (user.Status == UserStatus.Suspended)
            {
                throw new UnauthorizedException("Your account has been suspended. Please contact support.");
            }

            user.LastLogin = DateTime.UtcNow;
            await _repo.UpdateAsync(user);

            var response = _jwt.GenerateToken(user);
            response.RequirePasswordChange = user.MustChangePassword;
            if (user.Role == UserRole.Customer)
            {
                var corporateClient = await _clientRepo.GetByUserIdAsync(user.Id);

                if (corporateClient == null)
                {
                    response.IsProfileCompleted = false;
                }
                else
                {
                    if (corporateClient.IsBlocked)
                        throw new UnauthorizedException("Your account has been permanently blocked due to multiple verification failures. Please contact EGI support.");

                    response.IsProfileCompleted =
                        !string.IsNullOrEmpty(corporateClient.CompanyName) &&
                        !string.IsNullOrEmpty(corporateClient.Address);
                }
            }
            return response;
        }

        public async Task<AuthResponse> RegisterAdmin(AdminRegisterRequest req)
        {
            var existingUser = await _repo.GetByEmailAsync(req.Email);
            if (existingUser != null)
                throw new ConflictException("Email already exists");

            var user = _mapper.Map<User>(req);
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(req.Password);
            await _repo.AddAsync(user);
            return _jwt.GenerateToken(user);
        }
        public async Task CreateStaffByAdmin(AdminCreateStaffRequest req)
        {
            if (req.Role != UserRole.Agent && req.Role != UserRole.ClaimsOfficer)
                throw new BadRequestException("Invalid role");
            var existingUser = await _repo.GetByEmailAsync(req.Email);
            if (existingUser != null)
                throw new ConflictException("Email already exists");
            var tempPassword = GenerateTemporaryPassword();
            var user = _mapper.Map<User>(req);
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(tempPassword);
            user.MustChangePassword = true;
            await _repo.AddAsync(user);
            await _emailService.SendCredentialsEmailAsync(req.Email, tempPassword, req.SalaryLPA);


            await _notificationService.CreateNotificationAsync(user.Id, "Account Created", $"Welcome to EGI! Your {user.Role} account has been created by the administrator.", "Info");
        }
        public async Task ChangePassword(Guid userId, string newPassword)
        {
            var user = await _repo.GetByIdAsync(userId);

            if (user == null)
                throw new NotFoundException("User not found");

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
            user.MustChangePassword = false;

            await _repo.UpdateAsync(user);

            await _notificationService.CreateNotificationAsync(userId, "Password Changed", "Your password has been updated successfully.", "Success");
        }

        public async Task ForgotPassword(ForgotPasswordRequest req)
        {
            var user = await _repo.GetByEmailAsync(req.Email);
            if (user == null) return; // Don't reveal user existence

            // Generate a secure, time-bound reset token
            var resetToken = Guid.NewGuid().ToString("N").ToUpper().Substring(0, 12);

            user.ResetToken = resetToken;
            user.ResetTokenExpires = DateTime.UtcNow.AddHours(24);

            await _repo.UpdateAsync(user);

            // Notify user with the token
            await _emailService.SendCredentialsEmailAsync(user.Email, $"Your password reset token is: {resetToken}");

            await _notificationService.CreateNotificationAsync(user.Id, "Password Reset Initiated", "A reset token has been sent to your registered email address.", "Info");
        }

        public async Task ResetPassword(ResetPasswordRequest req)
        {
            var user = await _repo.GetByEmailAsync(req.Email);
            if (user == null || user.ResetToken != req.Token || user.ResetTokenExpires < DateTime.UtcNow)
            {
                throw new BadRequestException("Invalid or expired reset token.");
            }

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(req.NewPassword);
            user.MustChangePassword = false;
            user.ResetToken = null;
            user.ResetTokenExpires = null;

            await _repo.UpdateAsync(user);

            await _notificationService.CreateNotificationAsync(user.Id, "Password Reset Successful", "Your password has been reset using the security token.", "Success");
        }
    }
}
