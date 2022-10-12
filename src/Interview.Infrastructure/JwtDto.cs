namespace Interview.Infrastructure
{
    public sealed class JwtDto
    {
        public string AccessToken { get; set; }
        public DateTime Expiration { get; set; }
        public string RefreshToken { get; set; }
    }
}
