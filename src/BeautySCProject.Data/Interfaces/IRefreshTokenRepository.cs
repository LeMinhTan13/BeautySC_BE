using BeautySCProject.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Data.Interfaces
{
    public interface IRefreshTokenRepository
    {
        Task<bool> CreateAsync(RefreshToken refreshToken);
        Task<RefreshToken?> GetByAccountIdAsync(int accountId);
        Task<bool> DeleteAllByAccountIdAsync(int accountId);
    }
}
