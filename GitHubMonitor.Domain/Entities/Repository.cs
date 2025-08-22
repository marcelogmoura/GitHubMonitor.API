namespace GitHubMonitor.Domain.Entities
{
    public class Repository
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? HtmlUrl { get; set; }
        public string? Description { get; set; }
        public int StargazersCount { get; set; }
        public string? Language { get; set; }

        // Relacionamento com o dono do repositório
        public Guid OwnerId { get; set; }
        public Owner? Owner { get; set; }
    }
}
