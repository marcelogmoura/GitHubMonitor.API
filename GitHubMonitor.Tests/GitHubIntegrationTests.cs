using Bogus;
using FluentAssertions;
using GitHubMonitor.Domain.Dtos;
using GitHubMonitor.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Testing;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using Xunit;

namespace GitHubMonitor.Tests;

// Classe de teste para o fluxo de autenticação e busca de repositórios
public class GitHubIntegrationTests : IDisposable
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly MongoClient _mongoClient;
    private readonly IMongoDatabase _database;

    public GitHubIntegrationTests()
    {
        // Configura o serializador de Guid para o MongoDB, prevenindo erros de serialização.
        if (!BsonClassMap.IsClassMapRegistered(typeof(User)))
        {
            BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
        }

        _factory = new WebApplicationFactory<Program>();

        // Configuração de conexão para o MongoDB local (Docker)
        var connectionString = "mongodb://root:root@localhost:27017";
        _mongoClient = new MongoClient(connectionString);
        _database = _mongoClient.GetDatabase("GitHubMonitorDBTest");

        // Limpa o banco de dados antes de cada suíte de testes.
        _database.DropCollection("repositories");
        _database.DropCollection("users");
    }

    public void Dispose()
    {
        // Remove o banco de dados de teste após a execução dos testes.
        _mongoClient.DropDatabase("GitHubMonitorDBTest");
        _factory.Dispose();
    }

    // Testa o endpoint de login com credenciais válidas e verifica se um token é retornado.
    [Fact]
    public async Task Login_With_Valid_Credentials_Returns_Token()
    {
        // Arrange
        var client = _factory.CreateClient();
        var user = new User { Name = "Test User", Email = "test@user.com", Password = "Admin123" };
        var authRequest = new AutenticarUsuarioRequestDto { Email = user.Email, Password = user.Password };

        // Para este teste, vamos inserir o usuário diretamente no banco de dados para simular um usuário existente.
        // Em um cenário real, um repositório de testes seria usado.
        var usersCollection = _database.GetCollection<User>("users");
        await usersCollection.InsertOneAsync(user);

        // Act
        var jsonRequest = new StringContent(JsonConvert.SerializeObject(authRequest), Encoding.UTF8, "application/json");
        var response = await client.PostAsync("/api/Auth/login", jsonRequest);
        var responseContent = await response.Content.ReadAsStringAsync();
        var authResponse = JsonConvert.DeserializeObject<AutenticarUsuarioResponseDto>(responseContent);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        authResponse.Should().NotBeNull();
        authResponse?.Token.Should().NotBeNullOrEmpty();
    }

    // Testa se o endpoint protegido de repositórios retorna os dados com um token JWT válido.
    [Fact]
    public async Task GetRepositories_With_Valid_Token_Returns_Repositories()
    {
        // Arrange
        var client = _factory.CreateClient();
        var username = "octocat"; // Usuário do GitHub com repositórios públicos

        // 1. Inserir um usuário de teste para obter um token
        var user = new User { Name = "Test User", Email = "test@user.com", Password = "Admin123" };
        var usersCollection = _database.GetCollection<User>("users");
        await usersCollection.InsertOneAsync(user);

        // 2. Obter um token de login
        var authRequest = new AutenticarUsuarioRequestDto { Email = user.Email, Password = user.Password };
        var jsonRequest = new StringContent(JsonConvert.SerializeObject(authRequest), Encoding.UTF8, "application/json");
        var authResponse = await client.PostAsync("/api/Auth/login", jsonRequest);
        authResponse.EnsureSuccessStatusCode();
        var authContent = await authResponse.Content.ReadAsStringAsync();
        var token = JsonConvert.DeserializeObject<AutenticarUsuarioResponseDto>(authContent)?.Token;

        // 3. Adicionar o token ao cabeçalho da requisição
        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        // Act
        var repositoriesResponse = await client.GetAsync($"/api/GitHub/{username}");
        var repositoriesContent = await repositoriesResponse.Content.ReadAsStringAsync();
        var repositories = JsonConvert.DeserializeObject<List<RepositoryResponseDto>>(repositoriesContent);

        // Assert
        repositoriesResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        repositories.Should().NotBeNull().And.NotBeEmpty();
    }
}
