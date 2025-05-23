﻿using BeautySCProject.Common.Helpers;
using BeautySCProject.Data.Models.OrderDetailModel;
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

        [HttpPatch("deny-order")]
        [Authorize(Roles = Constants.USER_ROLE_STAFF)]
        public async Task<IActionResult> DenyOrder(int orderId)
        {
            var result = await _orderService.DenyOrderAsync(orderId);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        [HttpPatch("confirm-order")]
        [Authorize(Roles = Constants.USER_ROLE_STAFF)]
        public async Task<IActionResult> ConfirmOrder(int orderId)
        {
            var result = await _orderService.ConfirmOrderAsync(orderId);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        [HttpPatch("shipping-order")]
        [Authorize(Roles = Constants.USER_ROLE_STAFF)]
        public async Task<IActionResult> ShippingOrder(int orderId)
        {
            var result = await _orderService.ShippingOrderAsync(orderId);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        [HttpPatch("return-order")]
        [Authorize(Roles = Constants.USER_ROLE_STAFF)]
        public async Task<IActionResult> ReturnOrder(int orderId)
        {
            var result = await _orderService.ReturnOrderAsync(orderId);
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
        [Authorize(Roles = $"{Constants.USER_ROLE_CUSTOMER}, {Constants.USER_ROLE_STAFF}")]
        public async Task<IActionResult> CancelOrder(int orderId)
        {
            var result = await _orderService.CancelOrderAsync(orderId);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        [HttpGet("get-user-order")]
        [Authorize]
        public async Task<IActionResult> GetOrderByCustomer(string? status)
        {
            var customerId = Int32.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value);
            var result = await _orderService.GetOrderByCustomerAsync(customerId, status);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        [HttpGet("get_all_order")]
        [Authorize(Roles = Constants.USER_ROLE_STAFF)]
        public async Task<IActionResult> GetAllOrder(string? status)
        {
            var result = await _orderService.GetAllOrderAsync(status);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        [HttpGet("get_order_by_id")]
        public async Task<IActionResult> GetOrderById(int orderId)
        {
            var result = await _orderService.GetOrderByOrderIdAsync(orderId);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        [HttpPost("get_shipping_price")]
        public async Task<IActionResult> GetShippingPrice(bool inRegion, List<OrderDetailCreateRequest> request)
        {
            var result = await _orderService.GetShippingPriceAsync(inRegion, request);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        [HttpGet("get-all-revenue")]
        public async Task<IActionResult> GetAllRevenue()
        {
            var result = await _orderService.GetAllRevenueAsync();
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        [HttpGet("get-number-orders")]
        public async Task<IActionResult> GetNumberOfOrders()
        {
            var result = await _orderService.GetNumberOfOrderAsync();
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }
        [HttpGet("get-number-orders-complete")]
        public async Task<IActionResult> GetNumberOfCompleteOrders()
        {
            var result = await _orderService.GetNumberOfCompleteOrderAsync();
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        [HttpGet("get-revenue-by-day-mon-year")]
        public async Task<IActionResult> GetRevenueByDayMonYear(int day, int month, int year)
        {
            var result = await _orderService.GetRevenueByDayMonYearAsync(day, month, year);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        [HttpGet("get-all-revuenue-by-year")]
        public async Task<IActionResult> GetRevenueByYear(int startYear, int endYear)
        {
            var result = await _orderService.GetRevenueByYearAsync(startYear, endYear);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }
        [HttpGet("get-revenue-by-month-year")]
        public async Task<IActionResult> GetRevenueByMonthYear(int month, int year)
        {
            var result = await _orderService.GetRevenueByMonYearAsync( month, year);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

    }
}
