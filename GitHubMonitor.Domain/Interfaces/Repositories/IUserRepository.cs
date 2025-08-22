using GitHubMonitor.Domain.Entities;

namespace GitHubMonitor.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        User? GetUserByEmailAndPassword(string email, string password);
    }
}
