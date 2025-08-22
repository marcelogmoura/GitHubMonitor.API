using GitHubMonitor.Domain.Dtos;
using GitHubMonitor.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace GitHubMonitor.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GitHubController : ControllerBase
    {
        private readonly IGitHubService _gitHubService;

        public GitHubController(IGitHubService gitHubService)
        {
            _gitHubService = gitHubService;
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<List<RepositoryResponseDto>>> GetRepositories(string username)
        {
            try
            {
                var result = await _gitHubService.GetAndStoreUserRepositories(username);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocorreu um erro interno: " + ex.Message });
            }
        }
    }
}