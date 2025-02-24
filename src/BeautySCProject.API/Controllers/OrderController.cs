using BeautySCProject.Common.Helpers;
using BeautySCProject.Data.Models.OrderModel;
using BeautySCProject.Data.Models.ShippingAddressModel;
using BeautySCProject.Service.Interfaces;
using BeautySCProject.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BeautySCProject.API.Controllers
{
    public class OrderController : BaseAPIController
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("create-order")]
        [Authorize]
        public async Task<IActionResult> CreateOrder(int? voucherId, OrderCreateRequest request)
        {
            var customerId = Int32.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value);
            var result = await _orderService.CreateOrderAsync(customerId, voucherId, request);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        [HttpPatch("complete-order")]
        [Authorize(Roles = Constants.USER_ROLE_STAFF)]
        public async Task<IActionResult> CompleteOrder(int orderId)
        {
            var result = await _orderService.CompleteOrderAsync(orderId);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        [HttpPatch("cancel-order")]
        [Authorize(Roles = Constants.USER_ROLE_CUSTOMER)]
        public async Task<IActionResult> CancelOrder(int orderId)
        {
            var customerId = Int32.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value);
            var result = await _orderService.CancelOrderAsync(customerId, orderId);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        [HttpGet("get-user-order")]
        [Authorize]
        public async Task<IActionResult> GetOrderByCustomer()
        {
            var customerId = Int32.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value);
            var result = await _orderService.GetOrderByCustomerAsync(customerId);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        [HttpGet("get_all_order")]
        [Authorize(Roles = Constants.USER_ROLE_MANAGER)]
        public async Task<IActionResult> GetAllOrder()
        {
            var result = await _orderService.GetAllOrderAsync();
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }
    }
}
