using Bogus;
using FluentAssertions;
using GitHubMonitor.Domain.Dtos;
using GitHubMonitor.Domain.Entities;
using GitHubMonitor.Domain.Interfaces.Repositories;
using GitHubMonitor.Infra.Data.Fakes;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using Xunit;

namespace GitHubMonitor.Tests;

// Classe que estende WebApplicationFactory para substituir dependências em tempo de teste
public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Remove o registro do UserRepository real para evitar conflitos
            var userRepositoryDescriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(IUserRepository));

            if (userRepositoryDescriptor != null)
            {
                services.Remove(userRepositoryDescriptor);
            }

            // Adiciona a implementação falsa do repositório para os testes
            services.AddScoped<IUserRepository, FakeUserRepository>();
        });
    }
}

public class GitHubIntegrationTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public GitHubIntegrationTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Login_With_Valid_Credentials_Returns_Token()
    {
        // Arrange
        var user = new User { Name = "Admin Teste", Email = "admin@email.com", Password = "Admin123" };
        var authRequest = new AutenticarUsuarioRequestDto { Email = user.Email, Password = user.Password };

        // Act
        var jsonRequest = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(authRequest), Encoding.UTF8, "application/json");
        var response = await _client.PostAsync("/api/Auth/login", jsonRequest);
        var responseContent = await response.Content.ReadAsStringAsync();
        var authResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<AutenticarUsuarioResponseDto>(responseContent);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        authResponse.Should().NotBeNull();
        authResponse?.Token.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task GetRepositories_With_Valid_Token_Returns_Repositories()
    {
        // Arrange
        var user = new User { Name = "Admin Teste", Email = "admin@email.com", Password = "Admin123" };
        var authRequest = new AutenticarUsuarioRequestDto { Email = user.Email, Password = user.Password };

        var jsonRequest = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(authRequest), Encoding.UTF8, "application/json");
        var authResponse = await _client.PostAsync("/api/Auth/login", jsonRequest);
        authResponse.EnsureSuccessStatusCode();
        var authContent = await authResponse.Content.ReadAsStringAsync();
        var token = Newtonsoft.Json.JsonConvert.DeserializeObject<AutenticarUsuarioResponseDto>(authContent)?.Token;

        _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        // Act
        var repositoriesResponse = await _client.GetAsync("/api/GitHub/octocat");
        var repositoriesContent = await repositoriesResponse.Content.ReadAsStringAsync();
        var repositories = Newtonsoft.Json.JsonConvert.DeserializeObject<List<RepositoryResponseDto>>(repositoriesContent);

        // Assert
        repositoriesResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        repositories.Should().NotBeNull().And.NotBeEmpty();
    }
}
