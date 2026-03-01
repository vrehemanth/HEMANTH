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
        private string GenerateTemporaryPassword()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, 10)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public AuthService(IUserRepository repo, IJwtTokenGenerator jwt, IMapper mapper, IEmailService emailService, ICorporateClientRepository clientRepo)
        {
            _repo = repo;
            _jwt = jwt;
            _mapper = mapper;
            _emailService = emailService;
            _clientRepo = clientRepo;
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
            await _emailService.SendCredentialsEmailAsync(req.Email, tempPassword);
        }
        public async Task ChangePassword(Guid userId, string newPassword)
        {
            var user = await _repo.GetByIdAsync(userId);

            if (user == null)
                throw new NotFoundException("User not found");

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
            user.MustChangePassword = false;

            await _repo.UpdateAsync(user);
        }

        public async Task ForgotPassword(ForgotPasswordRequest req)
        {
            var user = await _repo.GetByEmailAsync(req.Email);
            if (user == null) return; 

            // Generate a secure temporary password
            var tempPassword = GenerateTemporaryPassword();
            
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(tempPassword);
            user.MustChangePassword = true; // Force them to change it on next login
            user.ResetToken = null;
            user.ResetTokenExpires = null;

            await _repo.UpdateAsync(user);
            
            // Re-using the Credentials email logic as it sends a temporary password
            await _emailService.SendCredentialsEmailAsync(user.Email, tempPassword);
        }
    }
}
