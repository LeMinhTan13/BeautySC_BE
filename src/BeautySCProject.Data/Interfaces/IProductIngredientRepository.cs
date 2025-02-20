using BeautySCProject.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Data.Interfaces
{
    public interface IProductIngredientRepository
    {
        Task<bool> CreateProductIngredientAsync(ProductIngredient productIngredient);
        Task<bool> DeleteProductIngredientsByProductIdAsync(int productId);
    }
}
