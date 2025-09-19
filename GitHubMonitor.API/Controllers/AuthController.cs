using GitHubMonitor.Domain.Dtos;
using GitHubMonitor.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace GitHubMonitor.API.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AutenticarUsuarioRequestDto request)
        {
            try
            {
                var response = await _authService.AuthenticateUser(request);

                return Ok(response);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ops, tente mais tarde: " + ex.Message });
            }
        }
    }
}