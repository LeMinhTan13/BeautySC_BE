using AutoMapper;
using BeautySCProject.Common.Helpers;
using BeautySCProject.Data;
using BeautySCProject.Data.Entities;
using BeautySCProject.Data.Interfaces;
using BeautySCProject.Data.Models.OrderModel;
using BeautySCProject.Data.Models.ProductModel;
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
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;

        public OrderService(IOrderRepository orderRepository, IOrderDetailService orderDetailService, IMapper mapper, IUnitOfWork uow)
        {
            _orderRepository = orderRepository;
            _orderDetailService = orderDetailService;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<MethodResult<string>> CreateOrderAsync(int customerId, OrderCreateRequest request)
        {
            await _uow.BeginTransactionAsync();
            try
            {                
                var order = await _orderDetailService.GetOrderInCartAsync(customerId);                
                if (order == null)
                {
                    return new MethodResult<string>.Failure("Order not found", StatusCodes.Status404NotFound);
                }

                //if (request.VoucherId != null)
                //{

                //}

                _mapper.Map(request, order);
                order.TotalAmount = await _orderDetailService.CaculateOrderAsync(order.OrderId);

                var checkUpOrder = await _orderRepository.UpdateOrderAsync(order);
                if (!checkUpOrder)
                {
                    return new MethodResult<string>.Failure("Fail while update order", StatusCodes.Status500InternalServerError);
                }                

                var checkCreOrder = await CreateNewOrderAsync(customerId);
                if (!checkCreOrder)
                {
                    return new MethodResult<string>.Failure("Fail while create new order", StatusCodes.Status500InternalServerError);
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

        public async Task<bool> CreateNewOrderAsync(int customerId)
        {
            var newOrder = new Order
            {
                CustomerId = customerId,
                Status = Constants.ORDER_STATUS_INCART
            };

            return await _orderRepository.CreateOneAsync(newOrder);
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

        public async Task<MethodResult<string>> CompleteOrderAsync(int customerId, int orderId)
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
