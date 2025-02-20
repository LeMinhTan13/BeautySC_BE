using BeautySCProject.Common.Helpers;
using BeautySCProject.Data.Models.OrderModel;
using BeautySCProject.Service.Interfaces;
using BeautySCProject.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BeautySCProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = Constants.USER_ROLE_CUSTOMER)]
    public class CartController : BaseAPIController
    {
        private readonly IOrderDetailService _orderDetailService;

        public CartController(IOrderDetailService orderDetailService)
        {
            _orderDetailService = orderDetailService;
        }

        [HttpPost("add-to-cart")]
        public async Task<IActionResult> AddToCart(int productId)
        {
            var customerId = Int32.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value);
            var result = await _orderDetailService.AddToCartAsync(customerId, productId);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        [HttpGet("view-cart")]
        public async Task<IActionResult> ViewCart()
        {
            var customerId = Int32.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value);
            var result = await _orderDetailService.ViewCartAsync(customerId);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        [HttpPut("update-quantity-in-cart")]
        public async Task<IActionResult> UpdateQuantityInCartAsync(int orderDetailId, int quantity)
        {
            var customerId = Int32.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value);
            var result = await _orderDetailService.UpdateQuantityInCartAsync(customerId, orderDetailId, quantity);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        [HttpDelete("delete-in-cart")]
        public async Task<IActionResult> DeleteInCart(int orderDetailId)
        {
            var customerId = Int32.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value);
            var result = await _orderDetailService.DeleteOneAsync(customerId, orderDetailId);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }
    }
}
