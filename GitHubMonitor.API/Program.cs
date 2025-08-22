// Adicione os namespaces necessários
using GitHubMonitor.API.Settings;
using GitHubMonitor.Domain.Interfaces.Repositories;
using GitHubMonitor.Domain.Interfaces.Services;
using GitHubMonitor.Domain.Services;
using GitHubMonitor.Infra.Data.Contexts;
using GitHubMonitor.Infra.Data.Repositories;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Configuração de serialização do Guid para evitar o erro "GuidRepresentation is Unspecified"
BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
var mongoDBSettings = builder.Configuration.GetSection("MongoDBSettings").Get<MongoDBSettings>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("sua-chave-secreta-de-pelo-menos-16-caracteres")), // Substitua pela sua chave secreta
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
builder.Services.AddAuthorization();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();