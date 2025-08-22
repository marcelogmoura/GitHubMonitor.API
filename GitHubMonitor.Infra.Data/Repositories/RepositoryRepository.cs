using GitHubMonitor.Domain.Entities;
using GitHubMonitor.Domain.Interfaces.Repositories;
using GitHubMonitor.Infra.Data.Contexts;
using MongoDB.Driver;

namespace GitHubMonitor.Infra.Data.Repositories
{
    public class RepositoryRepository : IRepositoryRepository
    {
        private readonly MongoDbContext _context;

        public RepositoryRepository(MongoDbContext context)
        {
            _context = context;
        }

        public async Task Add(Repository obj)
        {
            await _context.Repositories.InsertOneAsync(obj);
        }

        public async Task Update(Repository obj)
        {
            await _context.Repositories.ReplaceOneAsync(x => x.Id == obj.Id, obj);
        }

        public async Task Delete(Guid id)
        {
            await _context.Repositories.DeleteOneAsync(x => x.Id == id);
        }

        public async Task<List<Repository>?> GetAll()
        {
            return await _context.Repositories.Find(_ => true).ToListAsync();
        }

        public async Task<Repository?> GetById(Guid id)
        {
            return await _context.Repositories.Find(x => x.Id == id).FirstOrDefaultAsync();
        }
    }
}