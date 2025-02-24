using AutoMapper;
using BeautySCProject.Common.Helpers;
using BeautySCProject.Data;
using BeautySCProject.Data.Entities;
using BeautySCProject.Data.Interfaces;
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

        public async Task<MethodResult<string>> CreateOrderAsync(int customerId, int? voucherId, OrderCreateRequest request)
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
                        return new MethodResult<string>.Failure($"Proudct {product.ProductName} do not enough quantity", StatusCodes.Status400BadRequest);
                    }

                    product.Quantity -= item.Quantity;

                    var checkUpPro = await _productRepository.UpdateProductAsync(product);
                    if (!checkUpPro)
                    {
                        return new MethodResult<string>.Failure($"Fail while update product", StatusCodes.Status500InternalServerError);
                    }

                    item.Price = product.Price * (1 - product.Discount / 100);
                    order.TotalAmount += item.Price;
                }

                if (voucherId.HasValue)
                {
                    var voucher = await _voucherService.GetVoucherByIdAsync((int) voucherId);
                    if (voucher == null)
                    {
                        return new MethodResult<string>.Failure("Voucher not found", StatusCodes.Status404NotFound);
                    }

                    if (voucher.StartDate > DateTime.Now || voucher.EndDate < DateTime.Now)
                    {
                        return new MethodResult<string>.Failure("Invalid date of voucher", StatusCodes.Status400BadRequest);
                    }

                    if (voucher.MinimumPurchase > order.TotalAmount)
                    {
                        return new MethodResult<string>.Failure("Order do not enough amount to apply voucher", StatusCodes.Status400BadRequest);
                    }

                    order.TotalAmount -= voucher.DiscountAmount;
                }

                var checkCreOrder = await _orderRepository.CreateOneAsync(order);
                if (!checkCreOrder)
                {
                    return new MethodResult<string>.Failure("Fail while create order", StatusCodes.Status500InternalServerError);
                }
               
                await _uow.CommitTransactionAsync();
                return new MethodResult<string>.Success("Create order succesfully");
            }
            catch (Exception e)
            {
                await _uow.RollbackTransactionAsync();
                return new MethodResult<string>.Failure(e.ToString(), StatusCodes.Status500InternalServerError);
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

        public async Task<MethodResult<string>> CancelOrderAsync(int customerId, int orderId)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            if (order == null)
            {
                return new MethodResult<string>.Failure("Order not found", StatusCodes.Status404NotFound);
            }
            if (order.CustomerId != customerId)
            {
                return new MethodResult<string>.Failure("Customer does not have this order", StatusCodes.Status400BadRequest);
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

        public async Task<MethodResult<IEnumerable<OrderViewModel>>> GetOrderByCustomerAsync(int customerId) 
        {
            var result = await _orderRepository.GetOrderByCustomerAsync(customerId);
            return new MethodResult<IEnumerable<OrderViewModel>>.Success(result);
        }

        public async Task<MethodResult<IEnumerable<OrderViewModel>>> GetAllOrderAsync()
        {
            var result = await _orderRepository.GetAllOrderAsync();
            return new MethodResult<IEnumerable<OrderViewModel>>.Success(result);
        }
    }
}
