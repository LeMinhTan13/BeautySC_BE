using BeautySCProject.Common.Helpers;
using BeautySCProject.Data.Entities;
using BeautySCProject.Data.Models.ProductModel;
using BeautySCProject.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Service.Interfaces
{
    public interface IProductService
    {
        Task<Product> GetProductByIdAsync(int productId);
        Task<MethodResult<string>> CreateProductAsync(ProductCreateRequest request);
        Task<MethodResult<string>> UpdateProductAsync(ProductUpdateRequest request);
        Task<MethodResult<string>> ActiveProductAsync(int productId);
        Task<MethodResult<string>> InactiveProductAsync(int productId);
        Task<MethodResult<IEnumerable<ProductViewModel>>> GetAllProductAsync(int pageIndex, int pageSize, ProductSortContent sorts);
        Task<MethodResult<ProductDetailViewModel>> GetProductDetailAsync(int productId);
        Task<MethodResult<IEnumerable<ProductViewModel>>> GetNewProductAsync();
        Task<MethodResult<IEnumerable<ProductViewModel>>> GetBestSellerProductAsync();
    }
}
