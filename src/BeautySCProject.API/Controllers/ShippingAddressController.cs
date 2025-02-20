using BeautySCProject.Data.Models.ProductModel;
using BeautySCProject.Data.Models.ShippingAddressModel;
using BeautySCProject.Service.Interfaces;
using BeautySCProject.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BeautySCProject.API.Controllers
{
    public class ShippingAddressController : BaseAPIController
    {
        private readonly IShippingAddressService _shippingAddressService;

        public ShippingAddressController(IShippingAddressService shippingAddressService)
        {
            _shippingAddressService = shippingAddressService;
        }

        [HttpPost("create-shipping-address")]
        [Authorize]
        public async Task<IActionResult> CreateShippingAddress(ShippingAdressCreateRequest request)
        {
            var customerId = Int32.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value);
            var result = await _shippingAddressService.CreateOneAsync(customerId, request);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        [HttpPut("update_shipping_address")]
        [Authorize]
        public async Task<IActionResult> UpdateShippingAddress(ShippingAdressUpdateRequest request)
        {
            var customerId = Int32.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value);
            var result = await _shippingAddressService.UpdateOneAsync(customerId, request);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        [HttpDelete("delete_shipping_address")]
        [Authorize]
        public async Task<IActionResult> DeleteShippingAddress(int shippingAddressId)
        {
            var customerId = Int32.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value);
            var result = await _shippingAddressService.DeleteOneAsync(customerId, shippingAddressId);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        [HttpPatch("default_shipping_address")]
        [Authorize]
        public async Task<IActionResult> DefaultShippingAddress(int shippingAddressId)
        {
            var customerId = Int32.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value);
            var result = await _shippingAddressService.DefaultAsync(customerId, shippingAddressId);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        [HttpPatch("get_all_shipping_address")]
        [Authorize]
        public async Task<IActionResult> GetAllByCustomerId()
        {
            var customerId = Int32.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value);
            var result = await _shippingAddressService.GetAllByCustomerIdAsync(customerId);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }
    }
}
