using GitHubMonitor.Domain.Entities;
using MongoDB.Driver;

namespace GitHubMonitor.Infra.Data.Contexts
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase? _database;

        public MongoDbContext(string connectionString, string databaseName)
        {
            var settings = MongoClientSettings.FromConnectionString(connectionString);
                        
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);

            var client = new MongoClient(settings);
            _database = client.GetDatabase(databaseName);
        }

        public IMongoCollection<Repository> Repositories => _database.GetCollection<Repository>("repositories");
        public IMongoCollection<Owner> Owners => _database.GetCollection<Owner>("owners");

        public IMongoCollection<User> Users => _database.GetCollection<User>("users");
    }
}