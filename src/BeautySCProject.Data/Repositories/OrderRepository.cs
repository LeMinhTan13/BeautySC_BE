using BeautySCProject.Common.Helpers;
using BeautySCProject.Data.Entities;
using BeautySCProject.Data.Interfaces;
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
        public OrderRepository(IUnitOfWork unitOfWork, BeautyscDbContext dbContext) : base(dbContext)
        {
            _unitOfWork = unitOfWork;
            _dbContext = dbContext;
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
            return await Entities.FirstOrDefaultAsync(x => x.OrderId == orderId);
        }

        public async Task<IEnumerable<OrderViewModel>> GetOrderByCustomerAsync(int customerId)
        {
            return await Entities
                                .Include(x => x.OrderDetails)
                                    .ThenInclude(od => od.Product)
                                .Where(x => x.CustomerId == customerId && x.Status != Constants.ORDER_STATUS_CANCEL)
                                .Select(x => new OrderViewModel
                                {
                                    OrderId = x.OrderId,
                                    Address = x.Address,
                                    PhoneNumber = x.PhoneNumber,
                                    CreatedDate = x.CreatedDate,
                                    TotalAmount = (decimal)x.TotalAmount,
                                    Status = x.Status,
                                    Details = x.OrderDetails
                                        //.Where(od => od.Product != null)
                                        .Select(od => new OrderDetailViewModel
                                        {
                                            OrderDetailId = od.OrderDetailId,
                                            ProductId = od.ProductId,
                                            ProductName = od.Product.ProductName,
                                            Size = od.Product.Size,
                                            Quantity = od.Quantity,
                                            Price = od.Product.Price,
                                            Discount = od.Product.Discount
                                        })
                                        .ToList()
                                })
                                .ToListAsync();
        }

        public async Task<IEnumerable<OrderViewModel>> GetAllOrderAsync()
        {
            return await Entities
                                .Include(x => x.OrderDetails)
                                    .ThenInclude(od => od.Product)
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
                                            Size = od.Product.Size,
                                            Quantity = od.Quantity,
                                            Price = od.Product.Price,
                                            Discount = od.Product.Discount
                                        })
                                        .ToList()
                                })
                                .ToListAsync();
        }
    }
}
