﻿using AutoMapper;
using BeautySCProject.Common.Helpers;
using BeautySCProject.Data;
using BeautySCProject.Data.Entities;
using BeautySCProject.Data.Interfaces;
using BeautySCProject.Data.Models.OrderDetailModel;
using BeautySCProject.Data.Models.OrderModel;
using BeautySCProject.Data.Models.ProductModel;
using BeautySCProject.Data.Repositories;
using BeautySCProject.Data.ViewModels;
using BeautySCProject.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Service.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailService _orderDetailService;
        private readonly IProductService _productService;
        private readonly IProductRepository _productRepository;
        private readonly IVoucherService _voucherService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;

        public OrderService(IOrderRepository orderRepository, IOrderDetailService orderDetailService, IProductService productService, IProductRepository productRepository, IVoucherService voucherService, IMapper mapper, IUnitOfWork uow)
        {
            _orderRepository = orderRepository;
            _orderDetailService = orderDetailService;
            _productService = productService;
            _productRepository = productRepository;
            _voucherService = voucherService;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<MethodResult<CreateOrderViewModel>> CreateOrderAsync(int customerId, int? voucherId, OrderCreateRequest request)
        {
            await _uow.BeginTransactionAsync();
            try
            {
                var order = _mapper.Map<Order>(request);
                order.CustomerId = customerId;                

                foreach (var item in order.OrderDetails)
                {
                    var product = await _productService.GetProductByIdAsync(item.ProductId);

                    if (product.Quantity < item.Quantity)
                    {
                        return new MethodResult<CreateOrderViewModel>.Failure($"Proudct {product.ProductName} do not enough quantity", StatusCodes.Status400BadRequest);
                    }

                    product.Quantity -= item.Quantity;

                    var checkUpPro = await _productRepository.UpdateProductAsync(product);
                    if (!checkUpPro)
                    {
                        return new MethodResult<CreateOrderViewModel>.Failure($"Fail while update product", StatusCodes.Status500InternalServerError);
                    }

                    item.Price = product.Price * (1 - product.Discount);
                    order.TotalAmount += item.Price * item.Quantity;
                }

                order.ShippingPrice = await _orderRepository.GetShippingPriceAsync(request.InRegion, request.OrderDetailRequests);
                order.TotalAmount += order.ShippingPrice;

                if (voucherId.HasValue)
                {
                    var voucher = await _voucherService.GetVoucherByIdAsync((int) voucherId);
                    if (voucher == null)
                    {
                        return new MethodResult<CreateOrderViewModel>.Failure("Voucher not found", StatusCodes.Status404NotFound);
                    }

                    if (voucher.StartDate > DateTime.Now || voucher.EndDate < DateTime.Now)
                    {
                        return new MethodResult<CreateOrderViewModel>.Failure("Invalid date of voucher", StatusCodes.Status400BadRequest);
                    }

                    if (voucher.MinimumPurchase > order.TotalAmount)
                    {
                        return new MethodResult<CreateOrderViewModel>.Failure("Order do not enough amount to apply voucher", StatusCodes.Status400BadRequest);
                    }

                    var checkUsedVoucher = await _orderRepository.CheckUsedVoucherAsync(customerId, (int) voucherId);
                    if (checkUsedVoucher)
                    {
                        return new MethodResult<CreateOrderViewModel>.Failure("this voucher is used", StatusCodes.Status400BadRequest);
                    }

                    order.VoucherId = voucherId;
                    order.TotalAmount -= voucher.DiscountAmount;
                }

                var checkCreOrder = await _orderRepository.CreateOneAsync(order);
                if (!checkCreOrder)
                {
                    return new MethodResult<CreateOrderViewModel>.Failure("Fail while create order", StatusCodes.Status500InternalServerError);
                }

                order.OrderCode = $"ORD{DateTime.Now.ToString("yyyyMMdd")}{order.OrderId}";
                var checkUpOrd = await _orderRepository.UpdateOrderAsync(order);
                if (!checkUpOrd)
                {
                    return new MethodResult<CreateOrderViewModel>.Failure("Fail while update order", StatusCodes.Status500InternalServerError);
                }

                await _uow.CommitTransactionAsync();

                var result = new CreateOrderViewModel
                {
                    OrderId = order.OrderId,
                };
                return new MethodResult<CreateOrderViewModel>.Success(result);
            }
            catch (Exception e)
            {
                await _uow.RollbackTransactionAsync();
                return new MethodResult<CreateOrderViewModel>.Failure(e.ToString(), StatusCodes.Status500InternalServerError);
            }
        }
        
        public async Task<MethodResult<Order>> GetOrderByIdAsync(int orderId)
        {
            var result = await _orderRepository.GetOrderByIdAsync(orderId);
            return new MethodResult<Order>.Success(result);
        }

        public async Task<bool> UpdateOrderAsync(Order order)
        {
            return await _orderRepository.UpdateOrderAsync(order);
        }

        public async Task<MethodResult<string>> DenyOrderAsync(int orderId)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            if (order == null)
            {
                return new MethodResult<string>.Failure("Order not found", StatusCodes.Status404NotFound);
            }
            
            foreach (var orderDetail in order.OrderDetails)
            {
                var product = await _productService.GetProductByIdAsync(orderDetail.ProductId);
                product.Quantity += orderDetail.Quantity;

                var checkUpPro = await _productRepository.UpdateProductAsync(product);
                if (!checkUpPro)
                {
                    return new MethodResult<string>.Failure($"Fail while update product", StatusCodes.Status500InternalServerError);
                }
            }

            order.Status = Constants.ORDER_STATUS_DENIED;

            var checkUpOrd = await _orderRepository.UpdateOrderAsync(order);
            if (!checkUpOrd)
            {
                return new MethodResult<string>.Failure("Fail while update order", StatusCodes.Status500InternalServerError);
            }

            return new MethodResult<string>.Success("Deny order successfully");
        }

        public async Task<MethodResult<string>> ConfirmOrderAsync(int orderId)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            if (order == null)
            {
                return new MethodResult<string>.Failure("Order not found", StatusCodes.Status404NotFound);
            }            

            order.Status = Constants.ORDER_STATUS_CONFIRMED;

            var checkUpOrd = await _orderRepository.UpdateOrderAsync(order);
            if (!checkUpOrd)
            {
                return new MethodResult<string>.Failure("Fail while update order", StatusCodes.Status500InternalServerError);
            }

            return new MethodResult<string>.Success("Confirm order successfully");
        }

        public async Task<MethodResult<string>> ShippingOrderAsync(int orderId)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            if (order == null)
            {
                return new MethodResult<string>.Failure("Order not found", StatusCodes.Status404NotFound);
            }
            if (order.Status != Constants.ORDER_STATUS_CONFIRMED)
            {
                return new MethodResult<string>.Failure($"Status order is {order.Status}", StatusCodes.Status400BadRequest);
            }

            order.Status = Constants.ORDER_STATUS_SHIPPING;

            var checkUpOrd = await _orderRepository.UpdateOrderAsync(order);
            if (!checkUpOrd)
            {
                return new MethodResult<string>.Failure("Fail while update order", StatusCodes.Status500InternalServerError);
            }

            return new MethodResult<string>.Success("Shipping order successfully");
        }

        public async Task<MethodResult<string>> CompleteOrderAsync(int orderId)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            if (order == null)
            {
                return new MethodResult<string>.Failure("Order not found", StatusCodes.Status404NotFound);
            }
            if (order.Status != Constants.ORDER_STATUS_SHIPPING)
            {
                return new MethodResult<string>.Failure($"Status order is {order.Status}", StatusCodes.Status400BadRequest);
            }

            order.Status = Constants.ORDER_STATUS_COMPLETE;

            var checkUpOrd = await _orderRepository.UpdateOrderAsync(order);
            if (!checkUpOrd)
            {
                return new MethodResult<string>.Failure("Fail while update order", StatusCodes.Status500InternalServerError);
            }

            return new MethodResult<string>.Success("Complete order successfully");
        }

        public async Task<MethodResult<string>> ReturnOrderAsync(int orderId)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            if (order == null)
            {
                return new MethodResult<string>.Failure("Order not found", StatusCodes.Status404NotFound);
            }            
            if (order.Status != Constants.ORDER_STATUS_SHIPPING)
            {
                return new MethodResult<string>.Failure($"Status order is {order.Status}", StatusCodes.Status400BadRequest);
            }

            foreach (var orderDetail in order.OrderDetails)
            {
                var product = await _productService.GetProductByIdAsync(orderDetail.ProductId);
                product.Quantity += orderDetail.Quantity;

                var checkUpPro = await _productRepository.UpdateProductAsync(product);
                if (!checkUpPro)
                {
                    return new MethodResult<string>.Failure($"Fail while update product", StatusCodes.Status500InternalServerError);
                }
            }

            order.Status = Constants.ORDER_STATUS_RETURNED;

            var checkUpOrd = await _orderRepository.UpdateOrderAsync(order);
            if (!checkUpOrd)
            {
                return new MethodResult<string>.Failure("Fail while update order", StatusCodes.Status500InternalServerError);
            }

            return new MethodResult<string>.Success("Return order successfully");
        }

        public async Task<MethodResult<string>> CancelOrderAsync(int orderId)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            if (order == null)
            {
                return new MethodResult<string>.Failure("Order not found", StatusCodes.Status404NotFound);
            }            

            foreach (var orderDetail in order.OrderDetails)
            {
                var product = await _productService.GetProductByIdAsync(orderDetail.ProductId);
                product.Quantity += orderDetail.Quantity;

                var checkUpPro = await _productRepository.UpdateProductAsync(product);
                if (!checkUpPro)
                {
                    return new MethodResult<string>.Failure($"Fail while update product", StatusCodes.Status500InternalServerError);
                }
            }

            order.Status = Constants.ORDER_STATUS_CANCEL;

            var checkUpOrd = await _orderRepository.UpdateOrderAsync(order);
            if (!checkUpOrd)
            {
                return new MethodResult<string>.Failure("Fail while update order", StatusCodes.Status500InternalServerError);
            }

            return new MethodResult<string>.Success("Cancel order successfully");
        }

        public async Task<MethodResult<IEnumerable<OrderViewModel>>> GetOrderByCustomerAsync(int customerId, string? status) 
        {
            var result = await _orderRepository.GetOrderByCustomerAsync(customerId, status);
            return new MethodResult<IEnumerable<OrderViewModel>>.Success(result);
        }

        public async Task<MethodResult<IEnumerable<OrderViewModel>>> GetAllOrderAsync(string? status)
        {
            var result = await _orderRepository.GetAllOrderAsync(status);
            return new MethodResult<IEnumerable<OrderViewModel>>.Success(result);
        }

        public async Task<MethodResult<string>> GetShippingPriceAsync(bool inRegion, List<OrderDetailCreateRequest> request)
        {
            var result = await _orderRepository.GetShippingPriceAsync(inRegion, request);
            return new MethodResult<string>.Success(result.ToString());
        }

        public async Task<MethodResult<string>> GetAllRevenueAsync()
        {
            var result = await _orderRepository.GetAllRevenueAsync();
            return new MethodResult<string>.Success(result.ToString());
        }

        public async Task<MethodResult<OrderViewModel>> GetOrderByOrderIdAsync(int orderId)
        {
            var result = await _orderRepository.GetOrderByOrderIdAsync(orderId);
            return new MethodResult<OrderViewModel>.Success(result);

        }
        public async Task<MethodResult<string>> GetNumberOfOrderAsync()
        {
            var result = await _orderRepository.GetNumberOfOrderAsync();
            return new MethodResult<string>.Success(result.ToString());
        }
        public async Task<MethodResult<string>> GetNumberOfCompleteOrderAsync()
        {
            var result = await _orderRepository.GetNumberOfCompleteOrderAsync();
            return new MethodResult<string>.Success(result.ToString());
        }
        public async Task<MethodResult<RevenueViewModel>> GetRevenueByDayMonYearAsync(int day,int month, int year)
        {
            var result = await _orderRepository.GetRevenueByDayMonYearAsync(day,month,year);
            return new MethodResult<RevenueViewModel>.Success(result);
        }
        public async Task<MethodResult<IEnumerable<RevenueViewModel>>> GetRevenueByYearAsync(int startYear, int endYear)
        {
            var result = await _orderRepository.GetRevenueByYearAsync(startYear, endYear);
            return new MethodResult<IEnumerable<RevenueViewModel>>.Success(result);
        }
        public async Task<MethodResult<IEnumerable<RevenueViewModel>>> GetRevenueByMonYearAsync(int month, int year)
        {
            var result = await _orderRepository.GetRevenueByMonYearAsync(month, year);
            return new MethodResult<IEnumerable<RevenueViewModel>>.Success(result);
        }
    }
}
