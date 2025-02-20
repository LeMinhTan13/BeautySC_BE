using BeautySCProject.Common.Helpers;
using BeautySCProject.Data.Entities;
using BeautySCProject.Data.Models.AuthenticationModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Service.Interfaces
{
    public interface IAuthenticationService
    {
        Task<MethodResult<AccessToken>> AuthenticateAsync(Account account);
        Task<MethodResult<AccessToken>> SigninAsync(LoginRequest request);
        Task<MethodResult<string>> LogoutAsync(string email);

    }
}
