using BeautySCProject.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Service.Interfaces
{
    public interface IProductSkinTypeService
    {
        Task<bool> CreateProductSkinTypeAsync(ProductSkinType productSkinType);
        Task<bool> DeleteProductSkinTypesByProductIdAsync(int productId);
        Task<IEnumerable<ProductSkinType>> GetProductSkinTypesByProductIdAsync(int productId);
    }
}
