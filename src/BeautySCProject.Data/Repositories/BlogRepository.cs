using AutoMapper;
using AutoMapper.QueryableExtensions;
using BeautySCProject.Data.Entities;
using BeautySCProject.Data.Interfaces;
using BeautySCProject.Data.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Data.Repositories
{
    public class BlogRepository : Repository<Blog>, IBlogRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly BeautyscDbContext _dbContext;
        private IMapper _mapper;

        public BlogRepository(IUnitOfWork unitOfWork, BeautyscDbContext dbContext, IMapper mapper) : base(dbContext)
        {
            _unitOfWork = unitOfWork;
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BlogViewModel>> GetBlogsAsync()
        {
            return await Entities.ProjectTo<BlogViewModel>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<BlogDetailViewModel> GetBlogDetailAsync(int blogId)
        {
            return await Entities.ProjectTo<BlogDetailViewModel>(_mapper.ConfigurationProvider)
                                 .FirstOrDefaultAsync(x => x.BlogId == blogId);
        }

        public async Task<Blog> GetBlogByIdAsync(int blogId)
        {
            return await Entities.FirstOrDefaultAsync(x => x.BlogId == blogId);
        }

        public async Task<bool> CreateBlogAsync(Blog blog)
        {
            try
            {
                await Entities.AddAsync(blog);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> UpdateBlogAsync(Blog blog)
        {
            try
            {
                Entities.Update(blog);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
