using BeautySCProject.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Service.Interfaces
{
    public interface IProductFunctionService
    {
        Task<bool> CreateProductFunctionAsync(ProductFunction productFunction);
        Task<bool> DeleteProductFunctionsByProductIdAsync(int productId);
        Task<IEnumerable<ProductFunction>> GetProductFunctionByProductIdAsync(int productId);
    }
}
