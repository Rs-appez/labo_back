namespace ParcBack.Domain.Tokens;

public sealed class JwtOptions
{
    public string Issuer { get; init; } = string.Empty;
    public string Audience { get; init; } = string.Empty;
    public string Key { get; init; } = string.Empty; // symmetric key (32+ chars)
    public int ExpiryMinutes { get; init; } = 60;
}

