using BeautySCProject.Data.Entities;
using BeautySCProject.Data.Models.Category;
using BeautySCProject.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Data.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<CategoryViewModel>> GetCategoriesAsync();
        Task<Category> GetCategoryAsync(int categoryId);
        Task<CategoryCountProductModel> CountProductAsync(int categoryId);
    }
}
