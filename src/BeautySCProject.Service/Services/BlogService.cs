using AutoMapper;
using BeautySCProject.Common.Helpers;
using BeautySCProject.Data.Entities;
using BeautySCProject.Data.Interfaces;
using BeautySCProject.Data.Models.BlogModel;
using BeautySCProject.Data.Repositories;
using BeautySCProject.Data.ViewModels;
using BeautySCProject.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Service.Services
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IMapper _mapper;

        public BlogService(IBlogRepository blogRepository, IMapper mapper)
        {
            _blogRepository = blogRepository;
            _mapper = mapper;
        }

        public async Task<MethodResult<IEnumerable<BlogViewModel>>> GetBlogsAsync()
        {
            var result = await _blogRepository.GetBlogsAsync();
            return new MethodResult<IEnumerable<BlogViewModel>>.Success(result);
        }

        public async Task<MethodResult<BlogDetailViewModel>> GetBlogDetailAsync(int blogId)
        {
            var result = await _blogRepository.GetBlogDetailAsync(blogId);
            if (result == null)
            {
                return new MethodResult<BlogDetailViewModel>.Failure("Blog not found", StatusCodes.Status404NotFound);
            }
            return new MethodResult<BlogDetailViewModel>.Success(result);
        }

        public async Task<MethodResult<string>> CreateBlogAsync(int accountId, BlogCreateRequest request)
        {
            var blog = _mapper.Map<Blog>(request);
            blog.AccountId = accountId;

            var checkCreateBlog = await _blogRepository.CreateBlogAsync(blog);
            if (!checkCreateBlog)
            {
                return new MethodResult<string>.Failure("Create blog failed", StatusCodes.Status500InternalServerError);
            }

            return new MethodResult<string>.Success("Create blog successfully");
        }

        public async Task<MethodResult<string>> UpdateBlogAsync(int blogId, BlogUpdateRequest request)
        {
            var blog = await _blogRepository.GetBlogByIdAsync(blogId);
            if (blog == null)
            {
                return new MethodResult<string>.Failure("Blog not found", StatusCodes.Status404NotFound);
            }

            _mapper.Map(request, blog);

            var checkUpdateBlog = await _blogRepository.UpdateBlogAsync(blog);
            if (!checkUpdateBlog)
            {
                return new MethodResult<string>.Failure("Update blog failed", StatusCodes.Status500InternalServerError);
            }

            return new MethodResult<string>.Success("Update blog successfully");
        }
    }
}
