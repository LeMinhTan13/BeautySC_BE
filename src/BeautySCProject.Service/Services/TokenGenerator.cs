using BeautySCProject.Data.Entities;
using BeautySCProject.Data.Models.AuthenticationModel;
using BeautySCProject.Data.Models.Configuration;
using BeautySCProject.Service.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Service.Services
{
    public class TokenGenerator : ITokenGenerator
    {
        private readonly AuthenticationConfiguration _configuration;
        public TokenGenerator(IOptions<AuthenticationConfiguration> options)
        {
            _configuration = options.Value;
        }
        private string GenerateToken(string secretKey, string issuer, string audience, DateTime utcExpirationTime,
            IEnumerable<Claim>? claims)
        {
            SecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer,
                audience,
                claims,
                DateTime.UtcNow,
                utcExpirationTime,
                credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public AccessToken GenerateAccessTokenCustomer(Customer user)
        {
            List<Claim> claims =
            [
                new Claim(ClaimTypes.Sid, user.CustomerId.ToString()),
                new Claim(ClaimTypes.Email, user.Account.Email),            
                new Claim(ClaimTypes.Role, user.Account.Role)
            ];
            DateTime expirationTime = DateTime.UtcNow.AddMinutes(_configuration.AccessTokenExpiration);
            return new AccessToken
            {
                Token = GenerateToken(
                   _configuration.AccessTokenSecret,
                   _configuration.Issuer,
                   _configuration.Audience,
                   expirationTime,
                   claims),
                ExpirationTime = expirationTime            
            };
        }

        public AccessToken GenerateAccessTokenOther(Account account)
        {
            List<Claim> claims =
            [
                new Claim(ClaimTypes.Sid, account.AccountId.ToString()),
                new Claim(ClaimTypes.Email, account.Email),
                new Claim(ClaimTypes.Role, account.Role)
            ];
            DateTime expirationTime = DateTime.UtcNow.AddMinutes(_configuration.AccessTokenExpiration);
            return new AccessToken
            {
                Token = GenerateToken(
                   _configuration.AccessTokenSecret,
                   _configuration.Issuer,
                   _configuration.Audience,
                   expirationTime,
                   claims),
                ExpirationTime = expirationTime
            };
        }

        public string GenerateRefreshToken()
        {
            DateTime expirationTime = DateTime.UtcNow.AddMinutes(_configuration.RefreshTokenExpiration);
            return GenerateToken(
                    _configuration.RefreshTokenSecret,
                    _configuration.Issuer,
                    _configuration.Audience,
                    expirationTime, null);
        }
        public string GenerateEmailVerificationToken(string email)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, email)
            };
            DateTime expirationTime = DateTime.UtcNow.AddMinutes(_configuration.EmailVerificationExpiration);
            return GenerateToken(
                    _configuration.EmailVerificationSecret,
                    _configuration.Issuer,
                    _configuration.Audience,
                    expirationTime,
                    claims);
        }
        public string GeneratePasswordResetToken(string email)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, email)
            };
            DateTime expirationTime = DateTime.UtcNow.AddMinutes(_configuration.PasswordResetExpiration);
            return GenerateToken(
                    _configuration.PasswordResetSecret,
                    _configuration.Issuer,
                    _configuration.Audience,
                    expirationTime,
                    claims);
        }

        public string GenerateEmailUpdateToken(string oldEmail, string newEmail)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("old_email", oldEmail),
                new Claim("new_email", newEmail)
            };
            DateTime expirationTime = DateTime.UtcNow.AddMinutes(_configuration.EmailVerificationExpiration);
            return GenerateToken(
                    _configuration.EmailVerificationSecret,
                    _configuration.Issuer,
                    _configuration.Audience,
                    expirationTime,
                    claims);
        }
    }
}
