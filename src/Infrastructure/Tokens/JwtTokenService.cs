using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using ParcBack.Domain.Tokens;
using System.Text;
using ParcBack.Domain.Entities;

namespace ParcBack.Infrastructure.Tokens;

public sealed class JwtTokenService : ITokenService
{
    private readonly JwtOptions _opt;

    public JwtTokenService(IOptions<JwtOptions> options)
    {
        _opt = options.Value;
    }

    public string GenerateToken(Guid userId, string email, Role role, IDictionary<string, string>? extraClaims = null)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new(JwtRegisteredClaimNames.Email, email),
            new ("role", role.Name),
        };

        if (extraClaims != null)
        {
            foreach (var kv in extraClaims)
                claims.Add(new Claim(kv.Key, kv.Value));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_opt.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _opt.Issuer,
            audience: _opt.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_opt.ExpiryMinutes),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private bool IsXToken(ClaimsPrincipal user, string role)
    {
        if (user.Identity?.IsAuthenticated != true)
            return false;

        var roleClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);

        if (roleClaim == null || roleClaim.Value != role)
            return false;

        var isActiveClaim = user.Claims.FirstOrDefault(c => c.Type == "isActive");
        if (isActiveClaim == null || !bool.TryParse(isActiveClaim.Value, out var isActive) || !isActive)
            return false;

        return true;
    }
    public bool IsEmployeeToken(ClaimsPrincipal user)
    {
        return IsXToken(user, "Employee") || IsChiefToken(user);
    }
    public bool IsAdminToken(ClaimsPrincipal user)
    {
        return IsXToken(user, "Admin");
    }
    public bool IsChiefToken(ClaimsPrincipal user)
    {
        return IsXToken(user, "Chief") || IsAdminToken(user);
    }
}
