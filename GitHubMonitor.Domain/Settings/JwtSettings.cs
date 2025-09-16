namespace GitHubMonitor.Domain.Settings
{
    public class JwtSettings
    {
        public string SecretKey { get; set; }
        public int ExpirationInMinutes { get; set; }
    }
}
