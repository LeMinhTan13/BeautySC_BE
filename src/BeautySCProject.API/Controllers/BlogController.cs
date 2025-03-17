using BeautySCProject.Common.Helpers;
using BeautySCProject.Data.Models.BlogModel;
using BeautySCProject.Data.Models.SkinTestModel;
using BeautySCProject.Service.Interfaces;
using BeautySCProject.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BeautySCProject.API.Controllers
{
    [Route("api/blogs")]
    [ApiController]
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpGet("blogs")]
        public async Task<IActionResult> GetAllBlogs()
        {
            var result = await _blogService.GetBlogsAsync();
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        [HttpGet("blog/{id}")]
        public async Task<IActionResult> GetSkinTest([FromRoute] int id)
        {
            var result = await _blogService.GetBlogDetailAsync(id);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        [HttpPost]
        [Authorize(Roles = Constants.USER_ROLE_STAFF)]
        public async Task<IActionResult> CreateBlog(BlogCreateRequest request)
        {
            var accountId = Int32.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value);
            var result = await _blogService.CreateBlogAsync(accountId, request);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        [HttpPut("blog/{id}")]
        [Authorize(Roles = Constants.USER_ROLE_STAFF)]
        public async Task<IActionResult> UpdateBlog([FromRoute]int id, BlogUpdateRequest request)
        {
            var result = await _blogService.UpdateBlogAsync(id, request);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        
    }
}
