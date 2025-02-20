using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Net;
using BeautySCProject.Data.Models.Configuration;
using BeautySCProject.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using BeautySCProject.Data.Models;
using BeautySCProject.Common.Helpers;
using BeautySCProject.Data.Entities;
using Microsoft.AspNetCore.Routing;

namespace BeautySCProject.Service.Services
{
    public class EmailService : IEmailService
    {
        private readonly MailConfiguration _configuration;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly LinkGenerator _linkGenerator;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISmtpClient _smtpClient;
        public EmailService(IOptions<MailConfiguration> options,
            ITokenGenerator tokenGenerator,
            LinkGenerator linkGenerator, 
            IHttpContextAccessor httpContextAccessor,
            ISmtpClient smtpClient)
        {
            _configuration = options.Value;
            _tokenGenerator = tokenGenerator;
            _linkGenerator = linkGenerator;
            _httpContextAccessor = httpContextAccessor;
            _smtpClient = smtpClient;
        }
        private string? GenerateLinkWithToken(string action, string controller, object values)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
            {
                return null;
            }
            //return _linkGenerator.GetUriByAction(httpContext, action, controller, values);
            return "asd";
        }
        public async Task<bool> SendMailAsync(MailData mailData)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_configuration.SenderName, _configuration.FromEmail));
            message.To.Add(new MailboxAddress(mailData.ReceiverName, mailData.ToEmail));
            message.Subject = mailData.Subject;
            message.Body = new TextPart(TextFormat.Html) { Text = mailData.Body };

            try
            {
                await _smtpClient.ConnectAsync(_configuration.Server, _configuration.Port, SecureSocketOptions.StartTls);
                await _smtpClient.AuthenticateAsync(_configuration.FromEmail, _configuration.Password);
                await _smtpClient.SendAsync(message);
                await _smtpClient.DisconnectAsync(true);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<MethodResult<string>> SendAccountVerificationEmailAsync(Customer user)
        {
            var token = _tokenGenerator.GenerateEmailVerificationToken(user.Account.Email);
            var link = $"https://localhost:7105/api/Authentication/verify-email?token={token}";
            var content = @$"Customer {user.FullName} Register";
            var des = "Click to confirm your account";
            var mail = new MailData
            {
                ReceiverName = user.FullName,
                ToEmail = user.Account.Email,
                Subject = $"[Shien] {(user.Account.Role)} Account Registration Verification",
                Body = EmailTemplates.GetAccountVerificationEmailBody(content, des, link)
            };
            var emailSent = await SendMailAsync(mail);
            if (!emailSent)
            {
                return new MethodResult<string>.Failure(
                    "Failed to send verification email",
                    StatusCodes.Status500InternalServerError);
            }
            return new MethodResult<string>.Success("Please check your email for account verification link");
        }
        public async Task<MethodResult<string>> SendEmailUpdateVerificationAsync(Customer user, string newEmail)
        {
            var token = _tokenGenerator.GenerateEmailUpdateToken(user.Account.Email, newEmail);
            var link = $"https://localhost:7105/api/Customer/verify-updated-email?token={token}";
            var content = "Email Update Verification";
            var des = @$"Dear {user.FullName}, You have requested to update your email address. Please click the link below to verify your new email address:";
            var mail = new MailData
            {
                ReceiverName = user.FullName,
                ToEmail = newEmail,
                Subject = "[Shien] Email Update Verification",
                Body = EmailTemplates.GetAccountVerificationEmailBody(content, des, link)
            };
            var emailSent = await SendMailAsync(mail);
            if (!emailSent)
            {
                return new MethodResult<string>.Failure(
                    "Failed to send verification email",
                    StatusCodes.Status500InternalServerError);
            }
            return new MethodResult<string>.Success("Please check your new email for verification link");
        }
        public async Task<MethodResult<string>> SendPasswordResetEmailAsync(Customer user)
        {
            var token = _tokenGenerator.GeneratePasswordResetToken(user.Account.Email);
            var link = $"https://localhost:7105/api/Authentication/reset-password?token={token}";
            var content = "Request Password Reset";
            var des = @$"Dear {user.FullName}, You have requested to reset your password. Please follow the link below to enter your new password:";
            var mail = new MailData
            {
                ReceiverName = user.FullName,
                ToEmail = user.Account.Email,
                Subject = "[Shien] Request Password Reset",
                Body = EmailTemplates.GetAccountVerificationEmailBody(content, des, link)
            };
            var emailSent = await SendMailAsync(mail);
            if (!emailSent)
            {
                return new MethodResult<string>.Failure(
                    "Failed to send request password reset email",
                    StatusCodes.Status500InternalServerError);
            }
            return new MethodResult<string>.Success("Please check your email for request password reset link");
        }
        public async Task<MethodResult<string>> SendPasswordResetEmailAsync2(Customer user, string password)
        {
            var content = "Send Password Reset";
            var des = @$"Dear {user.FullName}, Your password is: {password}";
            var mail = new MailData
            {
                ReceiverName = user.FullName,
                ToEmail = user.Account.Email,
                Subject = "[Shien] Send Password Reset",
                Body = EmailTemplates.GetApprovalEmailBody(content, des)
            };
            var emailSent = await SendMailAsync(mail);
            if (!emailSent)
            {
                return new MethodResult<string>.Failure(
                    "Failed to send password reset email",
                    StatusCodes.Status500InternalServerError);
            }
            return new MethodResult<string>.Success("Please check your email for new password");
        }
        public async Task<MethodResult<string>> ResendVerificationEmailAsync(Customer user)
        {
            var token = _tokenGenerator.GenerateEmailVerificationToken(user.Account.Email);
            var link = $"https://localhost:7105/api/Customer/verify-email/{token}";
            var content = "Resending Verification link";
            var des = @$"Dear {user.FullName}, We received a request for another account verification email. Please follow the link below to verify your account:";
            var mail = new MailData
            {
                ReceiverName = user.FullName,
                ToEmail = user.Account.Email,
                Subject = "[Shien] Verify Your Account",
                Body = EmailTemplates.GetAccountVerificationEmailBody(content, des, link)
            };

            // Attempt to send the email
            var emailSent = await SendMailAsync(mail);
            if (!emailSent)
            {
                return new MethodResult<string>.Failure(
                    "Failed to resend verification email.",
                    (int)HttpStatusCode.InternalServerError);
            }
            return new MethodResult<string>.Success("Please check your email for the verification link.");
        }        
    }
}
