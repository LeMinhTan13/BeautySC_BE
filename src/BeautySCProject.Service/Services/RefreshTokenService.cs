using BeautySCProject.Common.Helpers;
using BeautySCProject.Data.Entities;
using BeautySCProject.Data.Interfaces;
using BeautySCProject.Data.Models.AuthenticationModel;
using BeautySCProject.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Service.Services
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly ICustomerService _userService;
        private readonly ITokenValidator _tokenValidator;
        public RefreshTokenService(IAuthenticationService authenticationService,
            IRefreshTokenRepository refreshTokenRepository,
            ICustomerService userService,
            ITokenValidator tokenValidator)
        {
            _authenticationService = authenticationService;
            _refreshTokenRepository = refreshTokenRepository;
            _userService = userService;
            _tokenValidator = tokenValidator;
        }
        public async Task<MethodResult<AccessToken>> RefreshTokenAsync(string accessToken) => await
            _tokenValidator.GetEmailFromExpiredAccessToken(accessToken)
            .Bind(email => _userService.GetAccountByEmailAsync(email)).Result
            .Bind(account => GetByCustomerIdAsync(account.AccountId).Result.Bind(async refreshToken =>
            {
                var isValid = _tokenValidator.ValidateRefreshToken(refreshToken.Token);
                if (!isValid)
                {
                    return new MethodResult<AccessToken>.Failure("Invalid token", 401);
                }
                return await _authenticationService.AuthenticateAsync(account);
            }));

        public async Task<MethodResult<RefreshToken>> GetByCustomerIdAsync(int accountId)
        {
            var refreshToken = await _refreshTokenRepository.GetByAccountIdAsync(accountId);
            if (refreshToken == null)
            {
                return new MethodResult<RefreshToken>.Failure("No refresh token found for this user", 400);
            }
            return new MethodResult<RefreshToken>.Success(refreshToken);
        }
    }
}
