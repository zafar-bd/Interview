using Interview.Infrastructure;
using Mapster;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Interview.Auth.API
{
    public static class JwtConfiguration
    {
        private static TokenValidationParameters GetAccessTokenValidationParameters()
        {
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SecretKey#479879fsdf$5454564"));

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
            return tokenValidationParameters;
        }

        private static TokenValidationParameters GetRefreshTokenValidationParameters()
        {
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SecretKey#479879fsdf$5454564"));

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                AuthenticationType = "JWT",
                RequireExpirationTime = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
            };
            return tokenValidationParameters;
        }

        public static void AddJwt(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            });

            var tokenValidationParameters = GetAccessTokenValidationParameters();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = tokenValidationParameters;
                });
        }

        public static JwtDto GenerateTokens(IEnumerable<Claim> claims, DateTime accessTokenLifeTime, DateTime refreshTokenLifeTime)
        {
            (string AccessToken, DateTime AccessTokenValidTo) = GenerateToken(claims, accessTokenLifeTime);

            JwtDto tokens = new()
            {
                AccessToken = AccessToken,
                Expiration = AccessTokenValidTo
            };

            var refreshTokenClaims = claims.Where(c => c.Type == JwtRegisteredClaimNames.Name).ToList();
            refreshTokenClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            tokens.RefreshToken = GenerateRefreshToken(claims, refreshTokenLifeTime);

            return tokens;
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

        private static string GenerateRefreshToken(IEnumerable<Claim> claims, DateTime lifeTime)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SecretKey#479879fsdf$5454564"));
            var credentials = new SigningCredentials(key, "HS256");

            var token = new JwtSecurityToken(
                claims: claims,
                expires: lifeTime,
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static ClaimsPrincipal ValidateRefreshToken(string token)
        {
            var tokenValidationParameters = GetRefreshTokenValidationParameters();

            ClaimsPrincipal claims = new JwtSecurityTokenHandler().ValidateToken(token, tokenValidationParameters, out _);
            return claims;
        }

        public static bool IsValidRefreshToken(string token)
        {
            var claimsPrinicpal = ValidateRefreshToken(token);

            return claimsPrinicpal?.Claims?.Any() ?? false;
        }
    }
}
