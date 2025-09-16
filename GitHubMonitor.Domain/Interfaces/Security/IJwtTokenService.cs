using GitHubMonitor.Domain.Entities;

namespace GitHubMonitor.Domain.Interfaces.Security
{
    public interface IJwtTokenService
    {
        string GenerateToken(User user);
        DateTime GenerateExpirationDate();
    }
}
