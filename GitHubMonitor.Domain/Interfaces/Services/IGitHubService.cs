using GitHubMonitor.Domain.Dtos;

namespace GitHubMonitor.Domain.Interfaces.Services
{
    public interface IGitHubService
    {
        Task<List<RepositoryResponseDto>?> GetAndStoreUserRepositories(string username);
    }
}
