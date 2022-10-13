using Interview.Auth.API.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Security.Claims;

namespace Interview.Auth.API.Controllers
{
    [Authorize]
    [Route("api/token")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        [AllowAnonymous]
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
               
        [HttpPost("refresh")]
        public async Task<IActionResult> GenerateRefreshTokenWithCredentials(CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            if (!cancellationToken.IsCancellationRequested)
            {
                return Created(string.Empty, JwtConfiguration.GenerateTokens(new List<Claim>()
                     {
                         new Claim(JwtRegisteredClaimNames.Name, User.Identity.Name),
                         new Claim(ClaimTypes.Role, "User"),
                         new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                   }, DateTime.Now.AddMinutes(10), DateTime.Now.AddDays(30)));
            }

            return Ok();
        }

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
