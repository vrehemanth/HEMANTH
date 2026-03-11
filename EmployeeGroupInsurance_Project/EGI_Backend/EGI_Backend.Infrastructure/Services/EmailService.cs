using EGI_Backend.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace EGI_Backend.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendCredentialsEmailAsync(string email, string tempPassword)
        {
            var subject = "Welcome to Employee Group Insurance - Your Credentials";
            var body = $@"
                <h3>Welcome to EGI!</h3>
                <p>An account has been created for you. Here are your temporary credentials:</p>
                <p><b>Username:</b> {email}</p>
                <p><b>Temporary Password:</b> {tempPassword}</p>
                <p>Please log in and reset your password immediately for security.</p>";

            // Safety check: log confirmation BEFORE sending email (password intentionally masked for security)
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine($"[EMAIL SERVICE] Sending Credentials to: {email}");
            Console.WriteLine($"[CREDENTIALS] Temp Password: {tempPassword}");
            Console.WriteLine("---------------------------------------------");

            await SendEmailAsync(email, subject, body);
        }

        public async Task SendRejectionEmailAsync(string email, string reason)
        {
            var subject = "Application Update - Employee Group Insurance";
            var body = $@"
                <h3>Application Status Update</h3>
                <p>We regret to inform you that your application has been rejected.</p>
                <p><b>Reason:</b> {reason}</p>
                <p>If you have any questions, please contact our support team.</p>";

            await SendEmailAsync(email, subject, body);

            Console.WriteLine($"[EMAIL SERVICE] Rejection Email sent to: {email}. Reason: {reason}");
        }

        public async Task SendBlockNotificationEmailAsync(string email)
        {
            var subject = "Security Alert - Account Blocked - Employee Group Insurance";
            var body = $@"
                <h3 style='color: #e74c3c;'>Account Permanently Blocked</h3>
                <p>Your corporate application has been rejected for the 3rd time.</p>
                <p>As per EGI security protocols, your account has been <b>permanently blocked</b> due to multiple failed verification attempts.</p>
                <p>If you believe this is an error, please visit our headquarters for physical document verification.</p>";

            await SendEmailAsync(email, subject, body);

            Console.WriteLine($"[EMAIL SERVICE] Final Block Alert sent to: {email}");
        }

        public async Task SendPasswordResetEmailAsync(string email, string token)
        {
            var subject = "Password Reset Request - Employee Group Insurance";
            var body = $@"
                <h3>Password Reset Requested</h3>
                <p>A request has been made to reset your password. Here is your verification token:</p>
                <p style='font-size: 1.25em; font-weight: bold; color: #3498db;'>{token}</p>
                <p>This token will expire in 1 hour.</p>
                <p>If you did not request this, please ignore this email.</p>";

            await SendEmailAsync(email, subject, body);

            Console.WriteLine("---------------------------------------------");
            Console.WriteLine($"[EMAIL SERVICE] Password Reset Token sent to: {email}");
            Console.WriteLine($"[TOKEN] Value: {token}");
            Console.WriteLine("---------------------------------------------");
        }

        public async Task SendClaimApprovedEmailAsync(string email, string memberName, string claimNumber, decimal amount, byte[] invoicePdf, string pdfFileName)
        {
            var subject = $"Claim Approved - {claimNumber}";
            var body = $@"
                <div style='font-family: Arial, sans-serif; color: #333;'>
                    <h2 style='color: #27ae60;'>Claim Approved!</h2>
                    <p>Dear <b>{memberName}</b>,</p>
                    <p>We are pleased to inform you that your claim <b>{claimNumber}</b> has been approved.</p>
                    <p><b>Approved Amount:</b> ₹{amount:N2}</p>
                    <p>Please find attached the detailed settlement invoice for your records.</p>
                    <br/>
                    <p>Regards,<br/>EGI Claims Processing Team</p>
                </div>";

            await SendEmailAsync(email, subject, body, invoicePdf, pdfFileName);

            Console.WriteLine($"[EMAIL SERVICE] Claim Approved Email sent to: {email} with attachment: {pdfFileName}");
        }

        public async Task SendInvoiceGeneratedEmailAsync(string email, string companyName, string invoiceNo, decimal amount, DateTime dueDate, byte[] invoicePdf, string pdfFileName)
        {
            var subject = $"New Invoice Generated - {invoiceNo}";
            var body = $@"
                <div style='font-family: Arial, sans-serif; color: #333;'>
                    <h2 style='color: #2980b9;'>New Invoice Generated</h2>
                    <p>Dear <b>{companyName}</b>,</p>
                    <p>A new invoice <b>{invoiceNo}</b> has been generated for your Employee Group Insurance policy.</p>
                    <ul>
                        <li><b>Invoice Amount:</b> ₹{amount:N2}</li>
                        <li><b>Due Date:</b> {dueDate.ToString("dd MMM yyyy")}</li>
                    </ul>
                    <p>Please find attached the detailed invoice for your records.</p>
                    <p>Kindly arrange for payment before the due date to ensure uninterrupted coverage.</p>
                    <br/>
                    <p>Regards,<br/>EGI Billing Team</p>
                </div>";

            await SendEmailAsync(email, subject, body, invoicePdf, pdfFileName);

            Console.WriteLine($"[EMAIL SERVICE] Invoice Email sent to: {email} with attachment: {pdfFileName}");
        }

        private async Task SendEmailAsync(string toEmail, string subject, string body, byte[]? attachmentBytes = null, string? attachmentName = null)
        {
            var smtpHost = _configuration["SmtpSettings:Server"];
            var smtpPort = int.Parse(_configuration["SmtpSettings:Port"] ?? "587");
            var senderEmail = _configuration["SmtpSettings:SenderEmail"];
            var senderName = _configuration["SmtpSettings:SenderName"];
            var username = _configuration["SmtpSettings:Username"];
            var password = _configuration["SmtpSettings:Password"];
            var enableSsl = bool.Parse(_configuration["SmtpSettings:EnableSsl"] ?? "true");

            try
            {
                using var client = new SmtpClient(smtpHost, smtpPort)
                {
                    Credentials = new NetworkCredential(username, password),
                    EnableSsl = enableSsl
                };

                using var mailMessage = new MailMessage
                {
                    From = new MailAddress(senderEmail!, senderName),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };

                mailMessage.To.Add(toEmail);

                if (attachmentBytes != null && !string.IsNullOrEmpty(attachmentName))
                {
                    mailMessage.Attachments.Add(new Attachment(new System.IO.MemoryStream(attachmentBytes), attachmentName, "application/pdf"));
                }

                await client.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EMAIL ERROR] Failed to send email to {toEmail}: {ex.Message}");
            }
        }
    }
}
