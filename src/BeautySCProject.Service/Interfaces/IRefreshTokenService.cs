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
    public interface IRefreshTokenService
    {
        Task<MethodResult<AccessToken>> RefreshTokenAsync(string token);
        Task<MethodResult<RefreshToken>> GetByCustomerIdAsync(int customerId);
    }
}
