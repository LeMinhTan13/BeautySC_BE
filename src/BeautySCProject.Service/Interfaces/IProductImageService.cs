using BeautySCProject.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Service.Interfaces
{
    public interface IProductImageService
    {
        Task<bool> CreateProductImageAsync(ProductImage productImage);
        Task<bool> DeleteProductImagesByProductIdAsync(int productId);
        Task<IEnumerable<ProductImage>> GetProductImagesByProductIdAsync(int productId);
    }
}
