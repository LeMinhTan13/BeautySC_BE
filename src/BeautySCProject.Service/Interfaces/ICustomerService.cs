using BeautySCProject.Data.Entities;
using BeautySCProject.Data.Models;
using BeautySCProject.Data.Models.AuthenticationModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeautySCProject.Common.Helpers;
using BeautySCProject.Data.Models.CustomerModel;
using BeautySCProject.Data.ViewModels;


namespace BeautySCProject.Service.Interfaces
{
    public interface ICustomerService
    {
        Task<MethodResult<Customer>> CreateCustomerAsyc(SignupRequest request);
        Task<MethodResult<Customer>> GetByEmailAsync(string email);
        Task<MethodResult<Customer>> GetByPhoneNumberAsync(string number);
        Task<MethodResult<string>> SignupAsync(SignupRequest request);
        Task<MethodResult<string>> RequestProfileUpdateAsync(string email, ProfileUpdateRequest request);
        Task<MethodResult<string>> UpdateProfileAsync(Customer user, ProfileUpdateRequest request);
        Task<MethodResult<string>> RequestPasswordUpdateAsync(string email, PasswordUpdateRequest request);
        Task<MethodResult<string>> UpdatePasswordAsync(Customer user, string newPassword);
        Task<MethodResult<string>> RequestEmailUpdateAsync(string newEmail, string oldEmail);
        Task<MethodResult<string>> UpdateEmailAsync(Customer user, string newEmail);
        Task<MethodResult<string>> RequestPasswordResetAsync(string email);
        Task<MethodResult<string>> VerifyCreatedEmailAsync(Customer user);
        Task<MethodResult<string>> VerifyAccountAsync(string token);
        Task<MethodResult<string>> VerifyEmailUpdateAsync(string token);
        Task<MethodResult<string>> VerifyPasswordResetAsync(string token, PasswordResetRequest request);
        Task<MethodResult<string>> RequestResendVerificationEmailAsync(string email);
        Task<MethodResult<Customer>> GetCustomerByIdAsync(int id);
        Task<MethodResult<Account>> GetAccountByEmailAsync(string email);
        Task<MethodResult<ProfileViewModel>> GetProfileAsync(string email);
        Task<MethodResult<string>> UpdateAvatarAsync(string email, string image);
        Task<MethodResult<string>> GetNumberCustomerAsync();
        Task<MethodResult<IEnumerable<CustomerViewModel>>> GetCustomersAsync();
        Task<MethodResult<string>> ActiveCustomerAsync(int userId);
        Task<MethodResult<string>> DeactiveCustomerAsync(int userId);

    }
}
