using AutoMapper;
using AutoMapper.QueryableExtensions;
using BeautySCProject.Data.Entities;
using BeautySCProject.Data.Interfaces;
using BeautySCProject.Data.Models.Category;
using BeautySCProject.Data.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Data.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly BeautyscDbContext _dbContext;
        private IMapper _mapper;

        public CategoryRepository(IUnitOfWork unitOfWork, BeautyscDbContext dbContext, IMapper mapper) : base(dbContext)
        {
            _unitOfWork = unitOfWork;
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryViewModel>> GetCategoriesAsync()
        {
            return await Entities.ProjectTo<CategoryViewModel>(_mapper.ConfigurationProvider).ToListAsync();
        }
        public async Task<Category> GetCategoryAsync(int categoryId)
        {
            var category = await Entities.FindAsync(categoryId);
            await _unitOfWork.SaveChangesAsync();
            return category;
        }
        public async Task<CategoryCountProductModel> CountProductAsync(int categoryId)
        {
            var result = await Entities.Where(x => x.CategoryId == categoryId)
                .Select(x => new CategoryCountProductModel
                {
                    CategoryId = x.CategoryId,
                    CategoryName = x.CategoryName,
                    ProductCount = x.Products.Count
                }).FirstOrDefaultAsync();
            return result;
        }
    }
}
