using BeautySCProject.Data.Entities;
using BeautySCProject.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Data.Repositories
{
    public class ProductFunctionRepository : Repository<ProductFunction>, IProductFunctionRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly BeautyscDbContext _dbContext;
        public ProductFunctionRepository(IUnitOfWork unitOfWork, BeautyscDbContext dbContext) : base(dbContext)
        {
            _unitOfWork = unitOfWork;
            _dbContext = dbContext;
        }

        public async Task<bool> CreateProductFunctionAsync(ProductFunction productFunction)
        {
            try
            {
                await Entities.AddAsync(productFunction);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteProductFunctionsByProductIdAsync(int productId)
        {
            try
            {
                var productFunctions = await Entities.Where(x => x.ProductId == productId).ToListAsync();
                Entities.RemoveRange(productFunctions);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<IEnumerable<ProductFunction>> GetProductFunctionByProductIdAsync(int productId)
        {
            return await Entities.Where(x => x.ProductId == productId).ToListAsync();
        }

    }
}
