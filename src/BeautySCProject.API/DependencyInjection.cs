
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using BeautySCProject.Data.Models.Configuration;
using System.Text;

namespace BeautySCProject.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection ConfigureAuthentication(this IServiceCollection service) //MAY RENAME OR RELOCATE LATER
        {
            service.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                var serviceProvider = service.BuildServiceProvider();
                var authenticationConfiguration = serviceProvider.GetRequiredService<IOptions<AuthenticationConfiguration>>().Value;
                options.TokenValidationParameters = new TokenValidationParameters

                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationConfiguration.AccessTokenSecret)),
                    ValidIssuer = authenticationConfiguration.Issuer,
                    ValidAudience = authenticationConfiguration.Audience,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
            return service;
        }
    }
}
