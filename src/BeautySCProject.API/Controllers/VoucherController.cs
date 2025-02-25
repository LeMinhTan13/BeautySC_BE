using BeautySCProject.Service.Interfaces;
using BeautySCProject.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    }
}
