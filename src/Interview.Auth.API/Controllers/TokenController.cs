using Interview.Auth.API.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Security.Claims;

namespace Interview.Auth.API.Controllers
{
    
    [Route("api/token")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> GenerateTokenWithCredentials([FromBody] LoginDto loginDto, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            if (!cancellationToken.IsCancellationRequested)
            {
                return Created(string.Empty, JwtConfiguration.GenerateTokens(new List<Claim>()
                     {
                         new Claim(JwtRegisteredClaimNames.Name, loginDto.UserName),
                         new Claim(ClaimTypes.Role, "User"),
                         new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    }, DateTime.Now.AddMinutes(10), DateTime.Now.AddDays(30)));
            }

            return Ok();
        }
               
        [HttpPost("{refreshToken}/refresh")]
        public async Task<IActionResult> GenerateRefreshTokenWithCredentials([FromRoute] string refreshToken, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            if (!cancellationToken.IsCancellationRequested)
            {
                return Created(string.Empty, JwtConfiguration.GenerateTokens(new List<Claim>()
                     {
                         new Claim(JwtRegisteredClaimNames.Name, "Test Refresh Token User"),
                         new Claim(ClaimTypes.Role, "User"),
                         new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                   }, DateTime.Now.AddMinutes(10), DateTime.Now.AddDays(30)));
            }

            return Ok();
        }

        [Authorize]
        [HttpGet("claims")]
        public async Task<IActionResult> GetClaims(CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            if (!cancellationToken.IsCancellationRequested)
            {
                return Ok(User.Claims.Select(s => new
                {
                    s.Type,
                    s.Value
                }));
            }
            return Ok();
        }
    }
}
