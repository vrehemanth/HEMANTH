using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGI_Backend.Application.Interfaces
{
    public interface IEmailService
    {
        Task SendCredentialsEmailAsync(string email, string tempPassword, decimal? salaryLPA = null);
        Task SendRejectionEmailAsync(string email, string reason);
        Task SendPasswordResetEmailAsync(string email, string token);
        Task SendBlockNotificationEmailAsync(string email);
        Task SendClaimApprovedEmailAsync(string email, string memberName, string claimNumber, decimal amount, byte[] invoicePdf, string pdfFileName);
        Task SendInvoiceGeneratedEmailAsync(string email, string companyName, string invoiceNo, decimal amount, DateTime dueDate, byte[] invoicePdf, string pdfFileName);
    }
}
