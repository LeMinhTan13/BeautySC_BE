using BeautySCProject.Common.Helpers;
using BeautySCProject.Data.Entities;
using BeautySCProject.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Service.Interfaces
{
    public interface IOrderDetailService
    {
        Task<Order> GetOrderInCartAsync(int customerId);
        Task<MethodResult<string>> AddToCartAsync(int customerId, int productId);
        Task<decimal> CaculateOrderAsync(int orderId);
        Task<MethodResult<string>> UpdateQuantityInCartAsync(int customerId, int orderDetailId, int quantity);
        Task<MethodResult<string>> DeleteOneAsync(int customerId, int orderDetailId);
        Task<MethodResult<IEnumerable<CartViewModel>>> ViewCartAsync(int customerId);
    }
}
