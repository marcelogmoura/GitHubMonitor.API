using FluentValidation;
using GitHubMonitor.Domain.Dtos;
using GitHubMonitor.Domain.Entities;
using GitHubMonitor.Domain.Interfaces.Repositories;
using GitHubMonitor.Domain.Interfaces.Services;
using GitHubMonitor.Domain.Validations;
using System.Text.Json;

namespace GitHubMonitor.Domain.Services
{
    public class GitHubService : IGitHubService
    {
        private readonly IRepositoryRepository _repositoryRepository;
        private readonly HttpClient _httpClient;

        public GitHubService(IRepositoryRepository repositoryRepository, HttpClient httpClient)
        {
            _repositoryRepository = repositoryRepository;
            _httpClient = httpClient;
        }

        public async Task<List<RepositoryResponseDto>?> GetAndStoreUserRepositories(string username)
        {
            var githubUrl = $"https://api.github.com/users/{username}/repos";

            _httpClient.DefaultRequestHeaders.Add("User-Agent", "GitHubMonitor-App-v1.0");

            var response = await _httpClient.GetAsync(githubUrl);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var githubRepositories = JsonSerializer.Deserialize<List<GithubRepositoryDto>>(content);
                        
            var repositories = githubRepositories?
                .Where(repo => repo.Owner != null)
                .Select(repo => new Repository
                {
                    Id = Guid.NewGuid(),
                    Name = repo.Name,
                    HtmlUrl = repo.HtmlUrl,
                    Description = repo.Description,
                    StargazersCount = repo.StargazersCount,
                    Language = repo.Language,
                    OwnerId = Guid.NewGuid(),
                    Owner = new Owner
                    {
                        Id = Guid.NewGuid(),
                        Login = repo.Owner.Login,
                        AvatarUrl = repo.Owner.AvatarUrl
                    }
                }).ToList();

            if (repositories != null)
            {
                foreach (var repo in repositories)
                {
                    var validation = new RepositoryValidation().Validate(repo);
                    if (!validation.IsValid)
                    {
                        throw new ValidationException(validation.Errors);
                    }
                    await _repositoryRepository.Add(repo);
                }
            }

            return repositories?.Select(repo => new RepositoryResponseDto
            {
                Name = repo.Name,
                HtmlUrl = repo.HtmlUrl,
                StargazersCount = repo.StargazersCount,
                Language = repo.Language,
                OwnerLogin = repo.Owner?.Login,
                OwnerAvatarUrl = repo.Owner?.AvatarUrl
            }).ToList();
        }

        
    }
}