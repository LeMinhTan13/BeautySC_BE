using BeautySCProject.Common.Helpers;
using BeautySCProject.Data.Entities;
using BeautySCProject.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Service.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendMailAsync(MailData mailData);
        Task<MethodResult<string>> SendAccountVerificationEmailAsync(Customer user);
        Task<MethodResult<string>> SendEmailUpdateVerificationAsync(Customer user, string newEmail);
        Task<MethodResult<string>> SendPasswordResetEmailAsync(Customer user);
        Task<MethodResult<string>> ResendVerificationEmailAsync(Customer user);      
    }
}
