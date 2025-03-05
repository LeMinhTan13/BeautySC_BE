using BeautySCProject.Common.Helpers;
using BeautySCProject.Data.Models.VoucherModel;
using BeautySCProject.Service.Interfaces;
using BeautySCProject.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BeautySCProject.API.Controllers
{
    
    public class VoucherController : BaseAPIController
    {
        private readonly IVoucherService _voucherService;
        public VoucherController(IVoucherService voucherService)
        {
            _voucherService = voucherService;
        }
        [HttpGet("Get-voucher-by-voucher-id")]
        public async Task<IActionResult> GetVoucher(int voucherId)
        {
            var result = await _voucherService.GetVoucherDetailByIdAsync(voucherId);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }
        [HttpGet("Get-all-voucher")]
        
        public async Task<IActionResult> GetAllVoucher()
        {
            var result = await _voucherService.GetAllVoucherAsync();
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }
        [HttpGet("Get-all-voucher-by-customer-id")]
        [Authorize(Roles = Constants.USER_ROLE_CUSTOMER)]
        public async Task<IActionResult> GetAllVoucherByCustomerId()
        {
            try
            {
                var customerId = Int32.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value);
                var result = await _voucherService.GetAllVoucherByCustomerIdAsync(customerId);
                return result.Match(
                    (l, c) => Problem(detail: l, statusCode: c),
                    Ok
                );
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi controller: {ex.Message}");
            }
        }
        [HttpPut("Update-voucher")]
        [Authorize]
        public async Task<IActionResult> Update(VoucherUpdateRequestModel request)
        {
            var result = await _voucherService.UpdateVoucherAsync(request);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }
        [HttpPost("Create-voucher")]
        public async Task<IActionResult> Create(VoucherCreateRequestModel request)
        {
            var result = await _voucherService.CreateVoucherAsync(request);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }
    }
}
