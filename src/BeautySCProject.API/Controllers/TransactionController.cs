using BeautySCProject.Common.Helpers;
using BeautySCProject.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BeautySCProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : BaseAPIController
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet("all-transactions")]
        [Authorize(Roles = Constants.USER_ROLE_MANAGER)]
        public async Task<IActionResult> GetAllTransactionAsync(
            [FromQuery] DateTime? startDate,
            [FromQuery] DateTime? endDate,
            [FromQuery] int pageSize = 20,
            [FromQuery] int pageIndex = 1)

        {
            var result = await _transactionService.GetAllTransactionAsync(startDate, endDate, pageSize, pageIndex);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        [HttpGet("my-transactions")]
        [Authorize]
        public async Task<IActionResult> GetTransactionByCustomerAsync()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            if (email == null) return Unauthorized();
            var result = await _transactionService.GetTransactionByCustomerAsync(email);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }
    }
}
