using BeautySCProject.Data.Entities;
using BeautySCProject.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Data.Interfaces
{
    public interface IBlogRepository
    {
        Task<IEnumerable<BlogViewModel>> GetBlogsAsync();
        Task<BlogDetailViewModel> GetBlogDetailAsync(int blogId);
        Task<Blog> GetBlogByIdAsync(int blogId);
        Task<bool> CreateBlogAsync(Blog blog);
        Task<bool> UpdateBlogAsync(Blog blog);
    }
}
