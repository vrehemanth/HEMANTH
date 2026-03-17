using System;
using System.Threading.Tasks;
using EGI_Backend.Application.Contracts.Auth;

namespace EGI_Backend.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponse> Register(RegisterRequest req);
        Task<AuthResponse> Login(LoginRequest req);
        Task<AuthResponse> RegisterAdmin(AdminRegisterRequest req);
        Task CreateStaffByAdmin(AdminCreateStaffRequest req);
        Task ChangePassword(Guid userId, string newPassword);
        Task ForgotPassword(ForgotPasswordRequest req);
        Task ResetPassword(ResetPasswordRequest req);
    }
}
