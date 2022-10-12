using Interview.Auth.API.Dto;
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
        public async Task<IActionResult> GetTokenWithCredentials([FromBody] LoginDto loginDto, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            if (!cancellationToken.IsCancellationRequested)
            {
                return Created(string.Empty, JwtConfiguration.GenerateToken(new List<Claim>()
                     {
                         new Claim(ClaimTypes.Name, loginDto.UserName),
                         new Claim(ClaimTypes.Role, "User"),
                         new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }));
            }

            return Ok();
        }
    }
}
