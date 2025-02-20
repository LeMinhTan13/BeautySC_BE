using BeautySCProject.Data.Entities;
using BeautySCProject.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Data.Repositories
{
    public class ShippingAddressRepository : Repository<ShippingAddress>, IShippingAddressRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly BeautyscDbContext _dbContext;
        public ShippingAddressRepository(IUnitOfWork unitOfWork, BeautyscDbContext dbContext) : base(dbContext)
        {
            _unitOfWork = unitOfWork;
            _dbContext = dbContext;
        }

        public async Task<bool> CreateOneAsync(ShippingAddress shippingAddress)
        {
            try
            {
                Entities.Add(shippingAddress);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> UpdateOneAsync(ShippingAddress shippingAddress)
        {
            try
            {
                Entities.Update(shippingAddress);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<ShippingAddress> GetOneByIdAsync(int shippingAddressId)
        {
            return await Entities.FirstOrDefaultAsync(x => x.ShippingAddressId == shippingAddressId);
        }

        public async Task<IEnumerable<ShippingAddress>> GetAllByCustomerIdAsync(int customerId)
        {
            return await Entities.Where(x => x.CustomerId == customerId).ToListAsync();
        }

        public async Task<bool> CheckExistAsync(int customerId)
        {
            return await Entities.AnyAsync(x => x.CustomerId == customerId);
        }

        public async Task<bool> CancelDefaultAsync(int customerId)
        {
            try
            {
                var shippingAddress = await Entities.FirstOrDefaultAsync(x => x.CustomerId == customerId && x.IsDefault == true);
                shippingAddress.IsDefault = false;

                Entities.Update(shippingAddress);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }            
        }

        public async Task<bool> DeleteOneAsync(ShippingAddress shippingAddress)
        {
            try
            {
                Entities.Remove(shippingAddress);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
