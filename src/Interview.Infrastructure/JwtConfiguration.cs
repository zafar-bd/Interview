using Interview.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Interview.Auth.API
{
    public static class JwtConfiguration
    {
        public static void AddJwt(this IServiceCollection services, IConfiguration configuration)
        {
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SecretKey#479879fsdf$5454564"));

            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            });

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = "Issuer",

                ValidateAudience = true,
                ValidAudience = "Audience",

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                AuthenticationType = "JWT",
                RequireExpirationTime = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
            };

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = tokenValidationParameters;
                });
        }

        public static JwtDto GenerateTokens(IEnumerable<Claim> claims, DateTime accessTokenLifeTime, DateTime refreshTokenLifeTime)
        {
            (string AccessToken, DateTime AccessTokenValidTo) = GenerateToken(claims, accessTokenLifeTime);
                        
            return new JwtDto
            {
                AccessToken = AccessToken,
                Expiration = AccessTokenValidTo,
                RefreshToken = Guid.NewGuid().ToString()
            };
        }

        private static (string Token, DateTime ValidTo) GenerateToken(IEnumerable<Claim> claims, DateTime lifeTime)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SecretKey#479879fsdf$5454564"));
            var credentials = new SigningCredentials(key, "HS256");

            var token = new JwtSecurityToken(
                issuer: "Issuer",
                audience: "Audience",
                claims: claims,
                expires: lifeTime,
                signingCredentials: credentials
            );

            return (new JwtSecurityTokenHandler().WriteToken(token), token.ValidTo);
        }
    }
}
