using BeautySCProject.Common.Helpers;
using BeautySCProject.Data.Interfaces;
using BeautySCProject.Data.ViewModels;
using BeautySCProject.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Service.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<MethodResult<IEnumerable<CategoryViewModel>>> GetCategoriesAsync()
        {
            var result = await _categoryRepository.GetCategoriesAsync();
            return new MethodResult<IEnumerable<CategoryViewModel>>.Success(result);
        }
    }
}
