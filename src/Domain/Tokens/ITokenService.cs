namespace ParcBack.Domain.Tokens;

public interface ITokenService
{
    string GenerateToken(Guid userId, string email, string? role = null, IDictionary<string, string>? extraClaims = null);
}
