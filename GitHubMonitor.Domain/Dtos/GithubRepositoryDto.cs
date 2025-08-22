using System.Text.Json.Serialization;

namespace GitHubMonitor.Domain.Dtos
{
    public class GithubRepositoryDto
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("html_url")]
        public string? HtmlUrl { get; set; }
        [JsonPropertyName("description")]
        public string? Description { get; set; }
        [JsonPropertyName("stargazers_count")]
        public int StargazersCount { get; set; }
        [JsonPropertyName("language")]
        public string? Language { get; set; }
        [JsonPropertyName("owner")]
        public GithubOwnerDto? Owner { get; set; }
    }
}