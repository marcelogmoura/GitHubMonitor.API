// Adicione os using's para as nossas camadas
using GitHubMonitor.API.Settings;
using GitHubMonitor.Domain.Interfaces.Repositories;
using GitHubMonitor.Domain.Interfaces.Services;
using GitHubMonitor.Domain.Services;
using GitHubMonitor.Infra.Data.Contexts;
using GitHubMonitor.Infra.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);
var mongoDBSettings = builder.Configuration.GetSection("MongoDBSettings").Get<MongoDBSettings>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient();

builder.Services.AddScoped<IGitHubService, GitHubService>();
builder.Services.AddScoped<IRepositoryRepository, RepositoryRepository>();

builder.Services.AddSingleton<MongoDbContext>(new MongoDbContext(mongoDBSettings.ConnectionString, mongoDBSettings.DatabaseName));

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