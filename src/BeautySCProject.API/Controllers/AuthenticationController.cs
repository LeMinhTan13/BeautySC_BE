using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using BeautySCProject.Service.Interfaces;
using BeautySCProject.Data.Models.AuthenticationModel;
using BeautySCProject.Data.Models.CustomerModel;
using BeautySCProject.Common.Helpers;


namespace BeautySCProject.API.Controllers
{

    public class AuthenticationController : BaseAPIController
    {
        private readonly ICustomerService _customerService;
        private readonly IAuthenticationService _authenticationService;
        private readonly IRefreshTokenService _refreshTokenService;

        public AuthenticationController(ICustomerService customerService,
            IAuthenticationService authenticationService,
            IRefreshTokenService refreshTokenService)
        {
            _customerService = customerService;
            _authenticationService = authenticationService;
            _refreshTokenService = refreshTokenService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] SignupRequest request)
        {
            var result = await _customerService.SignupAsync(request);
            return result.Match(
                (errorMessage, statusCode) => Problem(detail: errorMessage, statusCode: statusCode),
                Ok
            );
        }

        [HttpPatch]
        [Route("verify-email")]
        public async Task<IActionResult> VerifyAccount(string token)
        {
            var result = await _customerService.VerifyAccountAsync(token);
            return result.Match(
                (errorMessage, statusCode) => Problem(detail: errorMessage, statusCode: statusCode),
                successMessage => Ok(new { message = successMessage })
            );
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await _authenticationService.SigninAsync(request);
            return result.Match(
                (errorMessage, statusCode) => Problem(detail: errorMessage, statusCode: statusCode),
                Ok
            );
        }
        [HttpPost]
        [Route("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromQuery] string email)
        {
            var result = await _customerService.RequestPasswordResetAsync(email);
            return result.Match(
                (errorMessage, statusCode) => Problem(detail: errorMessage, statusCode: statusCode),
                Ok
            );
        }
        [HttpPut]
        [Route("reset-password")]
        public async Task<IActionResult> ResetPassword(string token, PasswordResetRequest request)
        {
            var result = await _customerService.VerifyPasswordResetAsync(token, request);
            return result.Match(
                (errorMessage, statusCode) => Problem(detail: errorMessage, statusCode: statusCode),
                Ok
            );
        }
        [HttpPut]
        [Route("refresh")]
        public async Task<IActionResult> Refresh()
        {
            var token = Request.Headers.Authorization.ToString().Replace("Bearer ", "");
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized();
            }
            var result = await _refreshTokenService.RefreshTokenAsync(token);
            return result.Match(
                (errorMessage, statusCode) => Problem(detail: errorMessage, statusCode: statusCode),
                Ok
            );
        }
        [Authorize]
        [HttpDelete("logout")]
        public async Task<IActionResult> Logout()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            if (email == null)
            {
                return Unauthorized();
            }
            var result = await _authenticationService.LogoutAsync(email);
            return result.Match(
                (errorMessage, statusCode) => Problem(detail: errorMessage, statusCode: statusCode),
                Ok
            );
        }
        [Authorize(Roles = Constants.USER_ROLE_CUSTOMER)]
        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            // Get user's email from JWT claims
            var email = User.FindFirst(ClaimTypes.Email)?.Value;

            if (email == null)
            {
                return Unauthorized();
            }
            // Fetch user details from database
            var result = await _customerService.GetProfileAsync(email);
            return result.Match(
                (errorMessage, statusCode) => Problem(detail: errorMessage, statusCode: statusCode),
                Ok
            );
        }        

        [HttpPost]
        [Route("resend-verification-email")]
        public async Task<IActionResult> RecoverAccount([FromBody] ResendVerificationLinkRequest request)
        {
            var result = await _customerService.RequestResendVerificationEmailAsync(request.Email);
            return result.Match(
                (errorMessage, statusCode) => Problem(detail: errorMessage, statusCode: statusCode),
                successMessage => Ok(new { message = successMessage })
            );
        }

        [Authorize]
        [HttpGet("Role")]
        public IActionResult GetRole()
        {
            // Get user's email from JWT claims
            var role = User.FindFirst(ClaimTypes.Role)?.Value;

            if (role == null)
            {
                return Unauthorized();
            }
            return Ok(new { Role = role });
        }
    }

}
