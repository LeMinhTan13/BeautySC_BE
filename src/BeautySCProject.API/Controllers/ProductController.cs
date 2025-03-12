using BeautySCProject.Common.Helpers;
using BeautySCProject.Data.Models.ProductModel;
using BeautySCProject.Data.Models.CustomerModel;
using BeautySCProject.Service.Interfaces;
using BeautySCProject.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BeautySCProject.API.Controllers
{
    public class ProductController : BaseAPIController
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost("create-product")]
        [Authorize(Roles = Constants.USER_ROLE_STAFF)]
        public async Task<IActionResult> CreateProduct(ProductCreateRequest request)
        {
            var result = await _productService.CreateProductAsync(request);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        [HttpPut("update-product")]
        [Authorize(Roles = Constants.USER_ROLE_STAFF)]
        public async Task<IActionResult> UpdateProduct(int productId, ProductUpdateRequest request)
        {
            var result = await _productService.UpdateProductAsync(productId, request);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        [HttpPatch("active-product")]
        [Authorize(Roles = Constants.USER_ROLE_STAFF)]
        public async Task<IActionResult> ActiveProduct(int productId)
        {
            var result = await _productService.ActiveProductAsync(productId);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        [HttpPatch("inactive-product")]
        [Authorize(Roles = Constants.USER_ROLE_STAFF)]
        public async Task<IActionResult> InactiveProduct(int productId)
        {
            var result = await _productService.InactiveProductAsync(productId);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        [HttpPost("get-all-product")]
        public async Task<IActionResult> GetAllProduct([FromForm] ProductSortContent sorts, int pageIndex = 1, int pageSize = 10)
        {
            var result = await _productService.GetAllProductAsync(pageIndex, pageSize, sorts);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        [HttpGet("get-product-detail")]
        public async Task<IActionResult> GetProductDetail(int productId)
        {
            var result = await _productService.GetProductDetailAsync(productId);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        [HttpGet("get-new-product")]
        public async Task<IActionResult> GetNewProduct()
        {
            var result = await _productService.GetNewProductAsync();
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        [HttpGet("get-best-seller-product")]
        public async Task<IActionResult> GetBestSellerProduct()
        {
            var result = await _productService.GetBestSellerProductAsync();
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }
        [HttpGet("get-number-of-products")]
        public async Task<IActionResult> CountProductInSystem()
        {
            var result = await _productService.GetNumberOfProductAsync();
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }
    }
}

