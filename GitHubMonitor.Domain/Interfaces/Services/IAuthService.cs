using GitHubMonitor.Domain.Dtos;

namespace GitHubMonitor.Domain.Interfaces.Services
{
    public interface IAuthService
    {
        Task<AutenticarUsuarioResponseDto> AuthenticateUser(AutenticarUsuarioRequestDto request);
    }
}
