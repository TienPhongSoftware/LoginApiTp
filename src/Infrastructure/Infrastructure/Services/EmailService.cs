using Application.Interfaces.Services;
using Microsoft.Extensions.Options;
using Infrastructure.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;
        private readonly BaseUrlSettings _baseUrlSettings;

        public EmailService(IOptions<EmailSettings> options, IOptions<BaseUrlSettings> optionsBaseUrl)
        {
            _emailSettings = options.Value;
            _baseUrlSettings = optionsBaseUrl.Value;
        }

        public async Task ConfirmationMailAsync(string link, string email)
        {
            link = _baseUrlSettings.ConfirmedEmailUrl + link;
            var client = new SmtpClient(_emailSettings.Host, _emailSettings.Port)
            {
                Credentials = new NetworkCredential(_emailSettings.Email, _emailSettings.Password),
                EnableSsl = _emailSettings.EnableSSL
            };
            var subject = $"Smartool - Verify your email address";
            var body = "<h1>Verify your email address</h1><hr/>";
            body += $"<p>Dear,</p>";
            body += $"<p>Please click this link for confirm email</p>";
            body += $"<a href='{link}'>Confirmation Link</a>";
            body += $"<p>Smartool team</p>";
            await client.SendMailAsync(
            new MailMessage(_emailSettings.Email, email, subject, body) { IsBodyHtml = true }
            );
        }

        public async Task ForgetPasswordMailAsync(string link, string email)
        {
            link = _baseUrlSettings.ForgetPasswordUrl + link;
            var client = new SmtpClient(_emailSettings.Host, _emailSettings.Port)
            {
                Credentials = new NetworkCredential(_emailSettings.Email, _emailSettings.Password),
                EnableSsl = _emailSettings.EnableSSL
            };
            var subject = $"Smartool - Reset Password";
            var body = "<h1>Reset your password</h1><hr/>";
            body += $"<p>Dear,</p>";
            body += $"<p>Please click this link for reset password</p>";
            body += $"<a href='{link}'>reset password link</a>";
            body += $"<p>Smartool team</p>";
            await client.SendMailAsync(
            new MailMessage(_emailSettings.Email, email, subject, body) { IsBodyHtml = true }
            );
        }

        public async Task SendNewPasswordAsync(string password, string email)
        {
            var client = new SmtpClient(_emailSettings.Host, _emailSettings.Port)
            {
                Credentials = new NetworkCredential(_emailSettings.Email, _emailSettings.Password),
                EnableSsl = _emailSettings.EnableSSL
            };
            var subject = $"Smartool - New Password";
            var body = "<h1>Your New Password</h1><hr/>";
            body += $"<p>Dear,</p>";
            body += $"<p>Here is your new password</p>";
            body += $"<p>Password : {password}</p>";
            body += $"<p>Smartool team</p>";
            await client.SendMailAsync(
            new MailMessage(_emailSettings.Email, email, subject, body) { IsBodyHtml = true }
            );
        }
    }
}
