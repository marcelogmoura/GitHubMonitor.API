namespace GitHubMonitor.Domain.Dtos
{
    public class RepositoryResponseDto
    {
        public string? Name { get; set; }
        public string? HtmlUrl { get; set; }
        public int StargazersCount { get; set; }
        public string? Language { get; set; }
        public string? OwnerLogin { get; set; }
        public string? OwnerAvatarUrl { get; set; }
    }
}
