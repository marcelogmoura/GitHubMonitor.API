using System.Text.Json.Serialization;

namespace GitHubMonitor.Domain.Dtos
{
    public class GithubOwnerDto
    {
        [JsonPropertyName("login")]
        public string? Login { get; set; }
        [JsonPropertyName("avatar_url")]
        public string? AvatarUrl { get; set; }
    }
}