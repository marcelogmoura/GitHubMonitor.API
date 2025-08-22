using GitHubMonitor.Domain.Entities;
using GitHubMonitor.Domain.Interfaces.Repositories;

namespace GitHubMonitor.Infra.Data.Fakes
{
    public class FakeUserRepository : IUserRepository
    {
        public User? GetUserByEmailAndPassword(string email, string password)
        {            
            if (email == "admin@email.com" && password == "Admin123")
            {
                return new User
                {
                    Id = Guid.NewGuid(),
                    Name = "Marcelo Moura",
                    Email = "admin@email.com",
                    Password = "Admin123" // em um cenário real a senha seria criptografada
                };
            }

            return null;
        }
    }
}