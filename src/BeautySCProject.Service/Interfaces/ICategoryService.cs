using BeautySCProject.Common.Helpers;
using BeautySCProject.Data.Entities;
using BeautySCProject.Data.Models.Category;
using BeautySCProject.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Service.Interfaces
{
    public interface ICategoryService
    {
        Task<MethodResult<IEnumerable<CategoryViewModel>>> GetCategoriesAsync();
        Task<MethodResult<CategoryViewModel>> GetCategoryAsync(int categoryId);
        Task<MethodResult<CategoryCountProductModel>> CountProductAsync(int categoryId);
    }
}
