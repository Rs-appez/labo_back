using System.Security.Claims;
using ParcBack.Domain.Entities;

namespace ParcBack.Domain.Tokens;

public interface ITokenService
{
    string GenerateToken(Guid userId, string email, Role role, IDictionary<string, string>? extraClaims = null);

    bool IsEmployeeToken(ClaimsPrincipal user);
    bool IsAdminToken(ClaimsPrincipal user);
    bool IsChiefToken(ClaimsPrincipal user);
}
