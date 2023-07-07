namespace Infrastructure.Authentication
{
    public record JwtOptions
    {
        public required string Issuer { get; init; }

        public required string Audience { get; init; }

        public required string SecretKey { get; init; }
    }
}
