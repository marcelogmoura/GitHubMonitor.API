using GitHubMonitor.Domain.Entities;
using MongoDB.Driver;
using MongoDB.Driver.Core.Connections;
using MongoDB.Driver.Core.Events;
using System.Security.Authentication;

namespace GitHubMonitor.Infra.Data.Contexts
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase? _database;

        public MongoDbContext(string connectionString, string databaseName)
        {
            var settings = MongoClientSettings.FromConnectionString(connectionString);

            // Adiciona a configuração de ServerApi para garantir a compatibilidade
            // com versões recentes do MongoDB.
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);

            var client = new MongoClient(settings);
            _database = client.GetDatabase(databaseName);
        }

        public IMongoCollection<Repository> Repositories => _database.GetCollection<Repository>("repositories");
        public IMongoCollection<Owner> Owners => _database.GetCollection<Owner>("owners");
    }
}