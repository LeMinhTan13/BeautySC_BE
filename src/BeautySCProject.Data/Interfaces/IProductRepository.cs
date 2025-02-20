using BeautySCProject.Data.Entities;
using BeautySCProject.Data.Models.ProductModel;
using BeautySCProject.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Data.Interfaces
{
    public interface IProductRepository
    {
        Task<bool> CreateProductAsync(Product product);
        Task<bool> UpdateProductAsync(Product product);
        Task<Product> GetProductByIdAsync(int productId);
        Task<bool> ActiveProductAsync(Product product);
        Task<bool> InactiveProductAsync(Product product);
        Task<IEnumerable<ProductViewModel>> GetAllProductAsync(int pageIndex, int pageSize, ProductSortContent sorts);
        Task<ProductDetailViewModel?> GetProductDetailAsync(int productId);
        Task<IEnumerable<ProductViewModel?>> GetNewProductAsync();
        Task<IEnumerable<ProductViewModel?>> GetBestSellerProductAsync();
    }
}
