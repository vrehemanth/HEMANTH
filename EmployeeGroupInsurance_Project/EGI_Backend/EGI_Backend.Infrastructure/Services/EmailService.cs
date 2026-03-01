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

            await SendEmailAsync(email, subject, body);

            // Safety check: print to console
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine($"[EMAIL SERVICE] Sending Credentials to: {email}");
            Console.WriteLine($"[CREDENTIALS] Temp Password: {tempPassword}");
            Console.WriteLine("---------------------------------------------");
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

        private async Task SendEmailAsync(string toEmail, string subject, string body)
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

                await client.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EMAIL ERROR] Failed to send email to {toEmail}: {ex.Message}");
            }
        }
    }
}
