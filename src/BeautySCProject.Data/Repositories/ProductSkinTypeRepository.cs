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
    public class ProductSkinTypeRepository : Repository<ProductSkinType>, IProductSkinTypeRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly BeautyscDbContext _dbContext;
        public ProductSkinTypeRepository(IUnitOfWork unitOfWork, BeautyscDbContext dbContext) : base(dbContext)
        {
            _unitOfWork = unitOfWork;
            _dbContext = dbContext;
        }

        public async Task<bool> CreateProductSkinTypeAsync(ProductSkinType productSkinType)
        {
            try
            {
                await Entities.AddAsync(productSkinType);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteProductSkinTypesByProductIdAsync(int productId)
        {
            try
            {
                var productSkinTypes = await Entities.Where(x => x.ProductId == productId).ToListAsync();
                Entities.RemoveRange(productSkinTypes);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<IEnumerable<ProductSkinType>> GetProductSkinTypesByProductIdAsync(int productId)
        {
            return await Entities.Where(x => x.ProductId == productId).ToListAsync();
        }
    }
}
