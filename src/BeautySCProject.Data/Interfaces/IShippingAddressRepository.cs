using BeautySCProject.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Data.Interfaces
{
    public interface IShippingAddressRepository
    {
        Task<bool> CreateOneAsync(ShippingAddress shippingAddress);
        Task<bool> UpdateOneAsync(ShippingAddress shippingAddress);
        Task<ShippingAddress> GetOneByIdAsync(int shippingAddressId);
        Task<IEnumerable<ShippingAddress>> GetAllByCustomerIdAsync(int customerId);
        Task<bool> CheckExistAsync(int customerId);
        Task<bool> CancelDefaultAsync(int customerId);
        Task<bool> DeleteOneAsync(ShippingAddress shippingAddress);
    }
}
