using Microsoft.EntityFrameworkCore;
using BeautySCProject.Data;
using BeautySCProject.Data.Entities;
using BeautySCProject.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Data.Repositories
{
    public class RefreshTokenRepository : Repository<RefreshToken>, IRefreshTokenRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly BeautyscDbContext _dbContext;
        public RefreshTokenRepository(IUnitOfWork unitOfWork, BeautyscDbContext dbContext) : base(dbContext)
        {
            _unitOfWork = unitOfWork;
            _dbContext = dbContext;
        }


        public async Task<bool> CreateAsync(RefreshToken refreshToken)
        {
            try
            {
                Entities.Add(refreshToken);
                await _unitOfWork.SaveChangesAsync();
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                return await Task.FromException<bool>(ex);
            }
        }
        public async Task<RefreshToken?> GetByAccountIdAsync(int accountId)
        {
            try
            {
                var refreshToken = await Entities.FirstOrDefaultAsync(x => x.AccountId == accountId);
                return refreshToken;
            }
            catch (Exception ex)
            {
                return await Task.FromException<RefreshToken?>(ex);
            }
        }
        public async Task<bool> DeleteAllByAccountIdAsync(int accountId)
        {
            IEnumerable<RefreshToken> refreshTokens = await Entities
                .Where(r => r.AccountId == accountId)
                .ToListAsync();
            try
            {
                Entities.RemoveRange(refreshTokens);
                await _unitOfWork.SaveChangesAsync();
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                return await Task.FromException<bool>(ex);
            }
        }
    }
}
