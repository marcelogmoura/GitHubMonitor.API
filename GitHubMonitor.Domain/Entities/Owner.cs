namespace GitHubMonitor.Domain.Entities
{
    public class Owner
    {
        public Guid Id { get; set; }
        public string? Login { get; set; }
        public string? AvatarUrl { get; set; }
    }
}
