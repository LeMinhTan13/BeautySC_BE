using BeautySCProject.Data.Entities;
using BeautySCProject.Data.Models.OrderDetailModel;
using BeautySCProject.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Data.Interfaces
{
    public interface IOrderRepository
    {
        Task<bool> CreateOneAsync(Order order);
        Task<Order> GetOrderByIdAsync(int orderId);
        Task<bool> UpdateOrderAsync(Order order);
        Task<IEnumerable<OrderViewModel>> GetOrderByCustomerAsync(int customerId, string? status);
        Task<IEnumerable<OrderViewModel>> GetAllOrderAsync(string? status);
        Task<bool> CheckUsedVoucherAsync(int customerId, int voucherId);
        Task<IEnumerable<PaymentMethodViewModel>> GetAllPaymentMethodAsync();
        Task<decimal> GetShippingPriceAsync(bool inRegion, List<OrderDetailCreateRequest> request);
    }
}
