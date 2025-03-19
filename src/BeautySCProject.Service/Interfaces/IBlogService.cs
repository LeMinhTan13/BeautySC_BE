using BeautySCProject.Common.Helpers;
using BeautySCProject.Data.Models.BlogModel;
using BeautySCProject.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Service.Interfaces
{
    public interface IBlogService
    {
        Task<MethodResult<IEnumerable<BlogViewModel>>> GetBlogsAsync();
        Task<MethodResult<BlogDetailViewModel>> GetBlogDetailAsync(int blogId);
        Task<MethodResult<string>> CreateBlogAsync(int accountId, BlogCreateRequest request);
        Task<MethodResult<string>> UpdateBlogAsync(int blogId, BlogUpdateRequest request);
    }
}
