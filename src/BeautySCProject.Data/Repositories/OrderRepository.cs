using AutoMapper;
using AutoMapper.QueryableExtensions;
using BeautySCProject.Common.Helpers;
using BeautySCProject.Data.Entities;
using BeautySCProject.Data.Interfaces;
using BeautySCProject.Data.Models.OrderDetailModel;
using BeautySCProject.Data.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Data.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly BeautyscDbContext _dbContext;
        private readonly IMapper _mapper;

        public OrderRepository(IUnitOfWork unitOfWork, BeautyscDbContext dbContext, IMapper mapper) : base(dbContext)
        {
            _unitOfWork = unitOfWork;
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<bool> CreateOneAsync(Order order)
        {
            try
            {
                Entities.Add(order);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> UpdateOrderAsync(Order order)
        {
            try
            {
                Entities.Update(order);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            return await Entities.Include(x => x.OrderDetails).FirstOrDefaultAsync(x => x.OrderId == orderId);
        }

        public async Task<IEnumerable<OrderViewModel>> GetOrderByCustomerAsync(int customerId, string? status)
        {
            return await Entities
                                .Include(x => x.OrderDetails)
                                    .ThenInclude(od => od.Product)
                                .Include(x => x.PaymentMethod)
                                .Where(x => x.CustomerId == customerId &&                                             
                                            (string.IsNullOrEmpty(status) || x.Status.ToLower() == status.ToLower()))
                                .Select(x => new OrderViewModel
                                {
                                    OrderId = x.OrderId,
                                    Address = x.Address,
                                    PhoneNumber = x.PhoneNumber,
                                    CreatedDate = x.CreatedDate,
                                    TotalAmount = (decimal)x.TotalAmount,
                                    PaymentMethodName = x.PaymentMethod.PaymentMethodName,
                                    Status = x.Status,
                                    Details = x.OrderDetails
                                        //.Where(od => od.Product != null)
                                        .Select(od => new OrderDetailViewModel
                                        {
                                            OrderDetailId = od.OrderDetailId,
                                            ProductId = od.ProductId,
                                            ProductName = od.Product.ProductName,
                                            ProductImage = od.Product.ProductImages.Any() ? od.Product.ProductImages.FirstOrDefault().Url : "",
                                            Size = od.Product.Size,
                                            Quantity = od.Quantity,
                                            Price = od.Product.Price,
                                            Discount = od.Product.Discount
                                        })
                                        .ToList()
                                })
                                .ToListAsync();
        }

        public async Task<IEnumerable<OrderViewModel>> GetAllOrderAsync(string? status)
        {
            return await Entities
                                .Include(x => x.OrderDetails)
                                    .ThenInclude(od => od.Product)
                                .Where(x => string.IsNullOrEmpty(status) || x.Status.ToLower() == status.ToLower())
                                .Select(x => new OrderViewModel
                                {
                                    OrderId = x.OrderId,
                                    Address = x.Address,
                                    PhoneNumber = x.PhoneNumber,
                                    CreatedDate = x.CreatedDate,
                                    TotalAmount = (decimal)x.TotalAmount,
                                    Status = x.Status,
                                    Details = x.OrderDetails
                                        .Where(od => od.Product != null)
                                        .Select(od => new OrderDetailViewModel
                                        {
                                            OrderDetailId = od.OrderDetailId,
                                            ProductId = od.ProductId,
                                            ProductName = od.Product.ProductName,
                                            ProductImage = od.Product.ProductImages.Any() ? od.Product.ProductImages.FirstOrDefault().Url : "",
                                            Size = od.Product.Size,
                                            Quantity = od.Quantity,
                                            Price = od.Product.Price,
                                            Discount = od.Product.Discount
                                        })
                                        .ToList()
                                })
                                .ToListAsync();
        }

        public async Task<bool> CheckUsedVoucherAsync(int customerId, int voucherId)
        {
            return await Entities.AnyAsync(x => x.CustomerId == customerId && x.VoucherId == voucherId);
                                
        }

        public async Task<IEnumerable<PaymentMethodViewModel>> GetAllPaymentMethodAsync()
        {
            return await _dbContext.PaymentMethods.ProjectTo<PaymentMethodViewModel>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<decimal> GetShippingPriceAsync(bool inRegion, List<OrderDetailCreateRequest> request)
        {
            float weight = 0;
            foreach (var item in request)
            {
                var product = await _dbContext.Products.FirstOrDefaultAsync(x => x.ProductId == item.ProductId);
                weight += product.Weight * item.Quantity;
            }

            var priceEnoughWeight = await _dbContext.ShippingPriceTables.FirstOrDefaultAsync(x => x.FromWeight < weight && x.ToWeight > weight);
            if (priceEnoughWeight != null)
            {
                if (inRegion)
                {
                    return priceEnoughWeight.InRegion;
                }
                else
                {
                    return priceEnoughWeight.OutRegion;
                }
            }
            else
            {
                var priceOverWeight = await _dbContext.ShippingPriceTables.OrderByDescending(x => x.FromWeight).FirstOrDefaultAsync();
                if (inRegion)
                {
                    return priceOverWeight.InRegion + (decimal) (Math.Ceiling(weight - priceOverWeight.FromWeight) * (double)priceOverWeight.Pir);
                }
                else
                {
                    return priceOverWeight.OutRegion + (decimal)(Math.Ceiling(weight - priceOverWeight.FromWeight) * (double)priceOverWeight.Por);
                }
            }
        }
        public async Task<decimal?> GetAllRevenueAsync()
        {
            return await Entities.SumAsync(x => x.TotalAmount);
        }
    }
}
