using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BeautySCProject.API.Controllers;
using BeautySCProject.Data.Models.CustomerModel;
using BeautySCProject.Service.Interfaces;
using System.Security.Claims;

namespace BeautySCProject.API.Controllers
{
	public class CustomerController : BaseAPIController
	{
		private readonly ICustomerService _customerService;
        private readonly IAuthenticationService _authenticationService;

        public CustomerController(ICustomerService customerService, IAuthenticationService authenticationService)
        {
            _customerService = customerService;
            _authenticationService = authenticationService;
        }

        [HttpPatch("update-profile")]
        [Authorize]
        public async Task<IActionResult> UpdateProfile([FromForm] ProfileUpdateRequest request)
        {
            var email = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            if (email == null) return Unauthorized();
            var result = await _customerService.RequestProfileUpdateAsync(email, request);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        [HttpPatch("change-password")]
        [Authorize]
        public async Task<IActionResult> UpdatePassword([FromBody] PasswordUpdateRequest request)
        {
            var email = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            if (email == null) return Unauthorized();
            var result = await _customerService.RequestPasswordUpdateAsync(email, request);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        [HttpPut("change-email")]
        [Authorize]
        public async Task<IActionResult> UpdateEmail(string newEmail)
        {
            var currentCustomerEmail = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            if (currentCustomerEmail == null) return Unauthorized();
            var result = await _customerService.RequestEmailUpdateAsync(newEmail, currentCustomerEmail);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );

        }

        [HttpGet("verify-updated-email")]
        public async Task<IActionResult> VerifyUpdatedEmail(string token)
        {
            var result = await _customerService.VerifyEmailUpdateAsync(token);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        [HttpPatch("update-avatar")]
        public async Task<IActionResult> UpdateAvatar(string image)
        {
            var email = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            if (email == null) return Unauthorized();

            var result = await _customerService.UpdateAvatarAsync(email, image);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

    }
}
