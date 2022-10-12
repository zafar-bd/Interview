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
                return Created(string.Empty, JwtConfiguration.GenerateToken(new List<Claim>()
                     {
                         new Claim(JwtRegisteredClaimNames.Name, loginDto.UserName),
                         new Claim(ClaimTypes.Role, "User"),
                         new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    }));
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
