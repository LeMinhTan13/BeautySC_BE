using AutoMapper;
using BeautySCProject.Common.Helpers;
using BeautySCProject.Data.Entities;
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
        private IMapper _mapper;
        public CategoryService(ICategoryRepository categoryRepository,IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<MethodResult<IEnumerable<CategoryViewModel>>> GetCategoriesAsync()
        {
            var result = await _categoryRepository.GetCategoriesAsync();
            return new MethodResult<IEnumerable<CategoryViewModel>>.Success(result);
        }
        public async Task<MethodResult<CategoryViewModel>> GetCategoryAsync(int categoryId)
        {
            var cate = await _categoryRepository.GetCategoryAsync(categoryId);
            var result = _mapper.Map<CategoryViewModel>(cate);
            return new MethodResult<CategoryViewModel>.Success(result);
        }
    }
}
