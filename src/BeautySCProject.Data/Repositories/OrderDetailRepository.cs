using AutoMapper;
using BeautySCProject.Common.Helpers;
using BeautySCProject.Data.Entities;
using BeautySCProject.Data.Interfaces;
using BeautySCProject.Data.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Data.Repositories
{
    public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly BeautyscDbContext _dbContext;
        private readonly IMapper _mapper;

        public OrderDetailRepository(IUnitOfWork unitOfWork, BeautyscDbContext dbContext, IMapper mapper) : base(dbContext)
        {
            _unitOfWork = unitOfWork;
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<bool> CreateOneAsync(OrderDetail orderDetail)
        {
            try
            {
                Entities.Add(orderDetail);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> UpdateOneAsync(OrderDetail orderDetail)
        {
            try
            {
                Entities.Update(orderDetail);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<OrderDetail?> ExistedOrderDetail(int customerId, int productId)
        {
            return await Entities.Include(x => x.Order).FirstOrDefaultAsync(x => x.Order.Status == Constants.ORDER_STATUS_INCART &&
                                                                                             x.Order.CustomerId == customerId &&
                                                                                             x.ProductId == productId);
        }

        public async Task<Order> GetOrderInCartAsync(int customerId)
        {
            return await _dbContext.Orders.FirstOrDefaultAsync(x => x.CustomerId == customerId && x.Status == Constants.ORDER_STATUS_INCART);
        }


        public async Task<decimal> CaculateOrderAsync(int orderId)
        {
            return await Entities.Where(x => x.OrderId == orderId).SumAsync(x => x.Price * x.Quantity);
        }

        public async Task<OrderDetail> GetOneByIdAsync(int orderDetailId)
        {
            return await Entities.Include(x => x.Order).FirstOrDefaultAsync(x => x.OrderDetailId == orderDetailId);
        }

        public async Task<bool> DeleteOneAsync(OrderDetail orderDetail)
        {
            try
            {
                Entities.Remove(orderDetail);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<IEnumerable<CartViewModel>> ViewCartAsync(int customerId)
        {
            return await Entities.Include(x => x.Order).Where(x => x.Order.CustomerId == customerId && x.Order.Status == Constants.ORDER_STATUS_INCART)
                                                       .Select(x => _mapper.Map<CartViewModel>(x))
                                                       .ToListAsync();
        }
    }
}
