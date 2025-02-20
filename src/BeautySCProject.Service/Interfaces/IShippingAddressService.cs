using BeautySCProject.Common.Helpers;
using BeautySCProject.Data.Entities;
using BeautySCProject.Data.Models.ShippingAddressModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Service.Interfaces
{
    public interface IShippingAddressService
    {
        Task<MethodResult<string>> CreateOneAsync(int customerId, ShippingAdressCreateRequest request);
        Task<MethodResult<string>> UpdateOneAsync(int customerId, ShippingAdressUpdateRequest request);
        Task<ShippingAddress> GetOneByIdAsync(int shippingAddressId);
        Task<MethodResult<IEnumerable<ShippingAddress>>> GetAllByCustomerIdAsync(int customerId);
        Task<MethodResult<string>> DefaultAsync(int customerId, int shippingAddressId);
        Task<MethodResult<string>> DeleteOneAsync(int customerId, int shippingAddressId);
    }
}
