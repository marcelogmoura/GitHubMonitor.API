using System.Text.Json.Serialization;

namespace GitHubMonitor.Domain.Dtos
{
    public class GithubRepositoryDto
    {
        public string? Name { get; set; }
        [JsonPropertyName("html_url")]
        public string? HtmlUrl { get; set; }
        public string? Description { get; set; }
        [JsonPropertyName("stargazers_count")]
        public int StargazersCount { get; set; }
        public string? Language { get; set; }
        public GithubOwnerDto? Owner { get; set; }
    }
}