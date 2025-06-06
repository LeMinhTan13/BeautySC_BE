﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BeautySCProject.API.Controllers;
using BeautySCProject.Data.Models.CustomerModel;
using BeautySCProject.Service.Interfaces;
using System.Security.Claims;
using BeautySCProject.Common.Helpers;

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

        [HttpGet("get-number-customer")]
        public async Task<IActionResult> GetNumberCustomer()
        {
            var result = await _customerService.GetNumberCustomerAsync();
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        [HttpGet("customers")]
        [Authorize(Roles = Constants.USER_ROLE_MANAGER)]
        public async Task<IActionResult> GetCustomers()
        {
            var result = await _customerService.GetCustomersAsync();
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        [HttpPatch("active-customer")]
        [Authorize(Roles = Constants.USER_ROLE_MANAGER)]
        public async Task<IActionResult> ActiveCustomer(int userId)
        {
            var result = await _customerService.ActiveCustomerAsync(userId);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }

        [HttpPatch("deactive-customer")]
        [Authorize(Roles = Constants.USER_ROLE_MANAGER)]
        public async Task<IActionResult> DeactiveCustomer(int userId)
        {
            var result = await _customerService.DeactiveCustomerAsync(userId);
            return result.Match(
                (l, c) => Problem(detail: l, statusCode: c),
                Ok
            );
        }
    }
}
