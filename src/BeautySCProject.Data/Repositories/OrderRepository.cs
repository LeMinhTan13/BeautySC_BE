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
using System.Reflection.Metadata;
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
                                .Where(x => x.CustomerId == customerId &&                                             
                                            (string.IsNullOrEmpty(status) || x.Status.ToLower() == status.ToLower()))
                                .ProjectTo<OrderViewModel>(_mapper.ConfigurationProvider)
                                .ToListAsync();
        }

        public async Task<IEnumerable<OrderViewModel>> GetAllOrderAsync(string? status)
        {
            return await Entities
                                .Where(x => string.IsNullOrEmpty(status) || x.Status.ToLower() == status.ToLower())
                                .ProjectTo<OrderViewModel>(_mapper.ConfigurationProvider)
                                .ToListAsync();
        }

        public async Task<OrderViewModel> GetOrderByOrderIdAsync(int orderId)
        {
            return await Entities.ProjectTo<OrderViewModel>(_mapper.ConfigurationProvider)
                                .FirstOrDefaultAsync(x => x.OrderId == orderId);
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
            return await Entities
                .Where(x => x.Status == Constants.ORDER_STATUS_COMPLETE)
                .SumAsync(x => x.TotalAmount);
        }
        public async Task<int> GetNumberOfOrderAsync()
        {
            return await Entities.CountAsync();
        }
        public async Task<int> GetNumberOfCompleteOrderAsync()
        {
            return await Entities.CountAsync(x => x.Status == Constants.ORDER_STATUS_COMPLETE);
        }
        public async Task<RevenueViewModel> GetRevenueByDayMonYearAsync(int day, int month, int year)
        {
            var startDate = new DateTime(year, month, day);
            var endDate = startDate.AddDays(1);

            var revenue = await Entities
                .Where(x => x.Status == Constants.ORDER_STATUS_COMPLETE && x.CreatedDate >= startDate && x.CreatedDate < endDate)
                .SumAsync(x => (x.TotalAmount ?? 0) - x.ShippingPrice);
            return new RevenueViewModel
            {
                Date = DateOnly.FromDateTime(startDate), 
                Revenue = revenue
            };
        }
        
        public async Task<IEnumerable<RevenueViewModel>> GetRevenueByYearAsync(int startYear, int endYear)
        {
            var startDate = new DateTime(startYear, 1, 1);
            var endDate = new DateTime(endYear, 12, 31, 23, 59, 59);
            // tại vì lấy theo năm nhưng hiển thị doanh thu theo tháng
            var revenueByMonth = Entities
                .Where(x => x.Status == "Complete" && x.CreatedDate >= startDate && x.CreatedDate <= endDate)
                .AsEnumerable() 
                .GroupBy(x => new { x.CreatedDate.Value.Year, x.CreatedDate.Value.Month })
                .Select(g => new RevenueViewModel
                {
                    Date = new DateOnly(g.Key.Year, g.Key.Month, 1),
                    Revenue = g.Sum(x => (x.TotalAmount ?? 0m) - x.ShippingPrice)
                })
                .OrderBy(x => x.Date)
                .ToList(); 

            return await Task.FromResult(revenueByMonth);
        }

        public async Task<IEnumerable<RevenueViewModel>> GetRevenueByMonYearAsync(int month, int year)
        {
            var startDate = new DateTime(year, month, 1);
            var endDate = startDate.AddMonths(1).AddSeconds(-1); 

            var revenueByDay = Entities
                .Where(x => x.Status == "Complete" && x.CreatedDate >= startDate && x.CreatedDate <= endDate)
                .AsEnumerable() 
                .GroupBy(x => x.CreatedDate.Value.Day)
                .Select(g => new RevenueViewModel
                {
                    Date = new DateOnly(year, month, g.Key), // Ngày cụ thể
                    Revenue = g.Sum(x => (x.TotalAmount ?? 0m) - x.ShippingPrice)
                })
                .OrderBy(x => x.Date)
                .ToList(); 

            return await Task.FromResult(revenueByDay);
        }



    }
}
