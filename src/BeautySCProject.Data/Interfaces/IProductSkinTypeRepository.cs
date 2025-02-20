using BeautySCProject.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Data.Interfaces
{
    public interface IProductSkinTypeRepository
    {
        Task<bool> CreateProductSkinTypeAsync(ProductSkinType productSkinType);
        Task<bool> DeleteProductSkinTypesByProductIdAsync(int productId);
        Task<IEnumerable<ProductSkinType>> GetProductSkinTypesByProductIdAsync(int productId);
    }
}
