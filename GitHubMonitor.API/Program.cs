using GitHubMonitor.API.Settings;
using GitHubMonitor.Domain.Interfaces.Repositories;
using GitHubMonitor.Domain.Interfaces.Services;
using GitHubMonitor.Domain.Services;
using GitHubMonitor.Infra.Data.Contexts;
using GitHubMonitor.Infra.Data.Repositories;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

var builder = WebApplication.CreateBuilder(args);

// **NOVA LINHA:** Registra o serializador do Guid como a primeira coisa
BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

// Adiciona a configuração do MongoDB lendo do appsettings.json
var mongoDBSettings = builder.Configuration.GetSection("MongoDBSettings").Get<MongoDBSettings>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient();

builder.Services.AddScoped<IGitHubService, GitHubService>();
builder.Services.AddScoped<IRepositoryRepository, RepositoryRepository>();
builder.Services.AddSingleton<MongoDbContext>(_ => new MongoDbContext(mongoDBSettings.ConnectionString ?? string.Empty, mongoDBSettings.DatabaseName ?? string.Empty));


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();