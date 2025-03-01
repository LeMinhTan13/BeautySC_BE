using AutoMapper;
using BeautySCProject.Common.Helpers;
using BeautySCProject.Data.Entities;
using BeautySCProject.Data.Interfaces;
using BeautySCProject.Data.Models.AuthenticationModel;
using BeautySCProject.Data.ViewModels;
using BeautySCProject.Service.Interfaces;
using Org.BouncyCastle.Asn1.Ocsp;

namespace BeautySCProject.Service.Services
{
	public class AuthenticationService : IAuthenticationService
	{
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;

        public AuthenticationService(ITokenGenerator tokenGenerator,
            IRefreshTokenRepository refreshTokenRepository,
            ICustomerService customerService,
            IMapper mapper)
        {
            _tokenGenerator = tokenGenerator;
            _refreshTokenRepository = refreshTokenRepository;
            _customerService = customerService;
            _mapper = mapper;
        }
        public async Task<MethodResult<AccessToken>> AuthenticateAsync(Account account)
           => await _customerService.GetByEmailAsync(account.Email).Result.Bind<AccessToken>(async customer =>
           {
               AccessToken accessToken = new AccessToken();
               if (account.Role == Constants.USER_ROLE_CUSTOMER)
               {                   
                       accessToken = _tokenGenerator.GenerateAccessTokenCustomer(customer);
               }
               else
               {
                   accessToken = _tokenGenerator.GenerateAccessTokenOther(account);
               }

               string refreshToken = _tokenGenerator.GenerateRefreshToken();

               RefreshToken refreshTokenDTO = new()
               {
                   Token = refreshToken,
                   AccountId = account.AccountId,
               };
               var hasDeleted = await _refreshTokenRepository.DeleteAllByAccountIdAsync(account.AccountId);
               var created = await _refreshTokenRepository.CreateAsync(refreshTokenDTO);
               if (!created || !hasDeleted)
               {
                   return new MethodResult<AccessToken>.Failure("Internal server error. Failed to authenticate", 500);
               }
               return new MethodResult<AccessToken>.Success(accessToken);
           });
  
        public async Task<MethodResult<AccessToken>> SigninAsync(LoginRequest request)
        {
            return await _customerService.GetAccountByEmailAsync(request.Email).Result.Bind(async account =>
                 await _customerService.GetByEmailAsync(request.Email).Result.Bind(async customer =>
                {
                    bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(request.Password, account.Password);
                    if (!isPasswordCorrect)
                    {
                        return new MethodResult<AccessToken>.Failure("Invalid email or password", 400);
                    }
                    if (account.Role == Constants.USER_ROLE_CUSTOMER)
                    {
                        if (customer.Status == Constants.USER_STATUS_INACTIVE)
                        {
                            return new MethodResult<AccessToken>.Failure("Your account has been inactivated", 400);
                        }
                        //if (customer.ConfirmedEmail == false)
                        //{
                        //    return new MethodResult<AccessToken>.Failure("Please verify your email", 400);
                        //}
                    }                    
                    return await AuthenticateAsync(account);
                }));
        }
        public async Task<MethodResult<string>> LogoutAsync(string email)
            => await _customerService.GetByEmailAsync(email).Result.Bind<string>(async user =>
            {
                var result = await _refreshTokenRepository.DeleteAllByAccountIdAsync(user.AccountId);
                if (!result)
                {
                    return new MethodResult<string>.Failure("Failed to logout", 500);
                }
                return new MethodResult<string>.Success("Logged out successfully");
            });        
    }
}
