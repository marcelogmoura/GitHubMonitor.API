using GitHubMonitor.Domain.Entities;
using GitHubMonitor.Domain.Interfaces.Repositories;
using GitHubMonitor.Infra.Data.Contexts;
using MongoDB.Driver;

namespace GitHubMonitor.Infra.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MongoDbContext _context;

        public UserRepository(MongoDbContext context)
        {
            _context = context;
        }

        public User? GetUserByEmailAndPassword(string email, string password)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Email, email) &
                         Builders<User>.Filter.Eq(u => u.Password, password);

            return _context.Users.Find(filter).FirstOrDefault();
        }
    }
}