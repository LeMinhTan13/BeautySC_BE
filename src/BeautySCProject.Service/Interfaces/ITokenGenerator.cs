using Microsoft.Extensions.Configuration;
using BeautySCProject.Data.Models.AuthenticationModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BeautySCProject.Data.Entities;

namespace BeautySCProject.Service.Interfaces
{
    public interface ITokenGenerator
    {
        AccessToken GenerateAccessTokenCustomer(Customer user);
        AccessToken GenerateAccessTokenOther(Account account);
        string GenerateRefreshToken();
        string GenerateEmailVerificationToken(string email);
        string GeneratePasswordResetToken(string email);
        string GenerateEmailUpdateToken(string oldEmail, string newEmail);
    }
}
