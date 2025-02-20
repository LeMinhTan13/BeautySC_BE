using AutoMapper;
using BeautySCProject.Common.Helpers;
using BeautySCProject.Data.Entities;
using BeautySCProject.Data.Interfaces;
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
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public OrderDetailService(IOrderDetailRepository orderDetailRepository, IProductService productService, IMapper mapper)
        {
            _orderDetailRepository = orderDetailRepository;
            _productService = productService;
            _mapper = mapper;
        }

        public async Task<MethodResult<string>> AddToCartAsync(int customerId, int productId)                   
        {
            var product = await _productService.GetProductByIdAsync(productId);
            if (product == null)
            {
                return new MethodResult<string>.Failure("Product not found", StatusCodes.Status404NotFound);
            }

            var orderDetail = await _orderDetailRepository.ExistedOrderDetail(customerId, productId);
            if (orderDetail != null)
            {
                orderDetail.Quantity++;
                var checkUpdate = await _orderDetailRepository.UpdateOneAsync(orderDetail);
                if (!checkUpdate)
                {
                    return new MethodResult<string>.Failure("Update order detail fail", StatusCodes.Status404NotFound);
                }                
            }
            else
            {               
                var orderInCart = await _orderDetailRepository.GetOrderInCartAsync(customerId);
                orderDetail = _mapper.Map<OrderDetail>(product);
                orderDetail.OrderId = orderInCart.OrderId;

                var checkCreate = await _orderDetailRepository.CreateOneAsync(orderDetail);
                if (!checkCreate)
                {
                    return new MethodResult<string>.Failure("Create order detail fail", StatusCodes.Status404NotFound);
                }
            }

            return new MethodResult<string>.Success("Add to cart successfully");
        }

        public async Task<Order> GetOrderInCartAsync(int customerId)
        {
            return await _orderDetailRepository.GetOrderInCartAsync(customerId);
        }

        public async Task<decimal> CaculateOrderAsync(int orderId)
        {
            return await _orderDetailRepository.CaculateOrderAsync(orderId);
        }

        public async Task<MethodResult<string>> UpdateQuantityInCartAsync(int customerId, int orderDetailId, int quantity)
            => await ValidateOrderdetailAsync(customerId, orderDetailId).Result.Bind<string>(async orderDetail =>
            {
                orderDetail.Quantity = quantity;
                var checkUpOrDe = await _orderDetailRepository.UpdateOneAsync(orderDetail);
                if (!checkUpOrDe)
                {
                    return new MethodResult<string>.Failure("Fail while update order detail", StatusCodes.Status500InternalServerError);
                }

                return new MethodResult<string>.Success("Update successfully");
            });

        public async Task<MethodResult<string>> DeleteOneAsync(int customerId, int orderDetailId)
            => await ValidateOrderdetailAsync(customerId, orderDetailId).Result.Bind<string>(async orderDetail =>
            {     
                var checkDelOrDe = await _orderDetailRepository.DeleteOneAsync(orderDetail);
                if (!checkDelOrDe)
                {
                    return new MethodResult<string>.Failure("Fail while delete order detail", StatusCodes.Status500InternalServerError);
                }

                return new MethodResult<string>.Success("Delete successfully");
            });

        private async Task<MethodResult<OrderDetail>> ValidateOrderdetailAsync(int customerId, int orderDetailId)
        {
            var orderDetail = await _orderDetailRepository.GetOneByIdAsync(orderDetailId);
            if (orderDetail == null)
            {
                return new MethodResult<OrderDetail>.Failure("Order detail not found", StatusCodes.Status404NotFound);
            }

            if (orderDetail.Order.CustomerId != customerId)
            {
                return new MethodResult<OrderDetail>.Failure("This cart not belong to this customer", StatusCodes.Status400BadRequest);
            }

            if (orderDetail.Order.Status != Constants.ORDER_STATUS_INCART)
            {
                return new MethodResult<OrderDetail>.Failure($"The order status is {orderDetail.Order.Status}", StatusCodes.Status400BadRequest);
            }

            return new MethodResult<OrderDetail>.Success(orderDetail);
        }

        public async Task<MethodResult<IEnumerable<CartViewModel>>> ViewCartAsync(int customerId)
        {
            var result = await _orderDetailRepository.ViewCartAsync(customerId);
            return new MethodResult<IEnumerable<CartViewModel>>.Success(result);
        }
    }
}
