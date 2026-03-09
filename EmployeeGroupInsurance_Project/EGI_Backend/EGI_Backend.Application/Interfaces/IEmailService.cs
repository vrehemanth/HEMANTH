using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGI_Backend.Application.Interfaces
{
    public interface IEmailService
    {
        Task SendCredentialsEmailAsync(string Email, string tempPassword);
        Task SendRejectionEmailAsync(string email, string reason);
        Task SendPasswordResetEmailAsync(string email, string token);
        Task SendBlockNotificationEmailAsync(string email);
    }
}
