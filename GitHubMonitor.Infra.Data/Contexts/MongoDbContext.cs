using GitHubMonitor.Domain.Entities;
using MongoDB.Driver;

namespace GitHubMonitor.Infra.Data.Contexts
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase? _database;

        public MongoDbContext(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }

        public IMongoCollection<Repository> Repositories => _database.GetCollection<Repository>("repositories");
        public IMongoCollection<Owner> Owners => _database.GetCollection<Owner>("owners");
    }
}