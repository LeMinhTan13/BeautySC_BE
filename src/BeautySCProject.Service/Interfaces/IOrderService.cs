using BeautySCProject.Common.Helpers;
using BeautySCProject.Data.Entities;
using BeautySCProject.Data.Models.OrderDetailModel;
using BeautySCProject.Data.Models.OrderModel;
using BeautySCProject.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Service.Interfaces
{
    public interface IOrderService
    {
        Task<MethodResult<CreateOrderViewModel>> CreateOrderAsync(int customerId, int? voucherId, OrderCreateRequest request);
        Task<MethodResult<Order>> GetOrderByIdAsync(int orderId);
        Task<bool> UpdateOrderAsync(Order order);
        Task<MethodResult<string>> DenyOrderAsync(int orderId);
        Task<MethodResult<string>> ConfirmOrderAsync(int orderId);
        Task<MethodResult<string>> ShippingOrderAsync(int orderId);
        Task<MethodResult<string>> CompleteOrderAsync(int orderId);
        Task<MethodResult<string>> ReturnOrderAsync(int orderId);
        Task<MethodResult<string>> CancelOrderAsync(int orderId);
        Task<MethodResult<IEnumerable<OrderViewModel>>> GetOrderByCustomerAsync(int customerId, string? status);
        Task<MethodResult<IEnumerable<OrderViewModel>>> GetAllOrderAsync(string? status);
        Task<MethodResult<string>> GetShippingPriceAsync(bool inRegion, List<OrderDetailCreateRequest> request);
        Task<MethodResult<OrderViewModel>> GetOrderByOrderIdAsync(int orderId);
    }
}
