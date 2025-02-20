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
    public class ProductIngredientRepository : Repository<ProductIngredient>, IProductIngredientRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly BeautyscDbContext _dbContext;
        public ProductIngredientRepository(IUnitOfWork unitOfWork, BeautyscDbContext dbContext) : base(dbContext)
        {
            _unitOfWork = unitOfWork;
            _dbContext = dbContext;
        }

        public async Task<bool> CreateProductIngredientAsync(ProductIngredient productIngredient)
        {
            try
            {
                await Entities.AddAsync(productIngredient);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteProductIngredientsByProductIdAsync(int productId)
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
    }
}
