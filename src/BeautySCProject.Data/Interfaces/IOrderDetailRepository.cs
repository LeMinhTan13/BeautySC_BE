using BeautySCProject.Data.Entities;
using BeautySCProject.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Data.Interfaces
{
    public interface IOrderDetailRepository
    {
        Task<bool> CreateOneAsync(OrderDetail orderDetail);
        Task<bool> UpdateOneAsync(OrderDetail orderDetail);
        Task<OrderDetail?> ExistedOrderDetail(int customerId, int productId);
        Task<Order> GetOrderInCartAsync(int customerId);
        Task<decimal> CaculateOrderAsync(int orderId);
        Task<OrderDetail> GetOneByIdAsync(int orderDetailId);
        Task<bool> DeleteOneAsync(OrderDetail orderDetail);
        Task<IEnumerable<CartViewModel>> ViewCartAsync(int customerId);
    }
}
