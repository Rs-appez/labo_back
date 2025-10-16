using ParcBack.Domain.Tokens;

namespace ParcBack.Application.Employees;

public static class EmployeeJwtExtensions
{
    public static string GenerateToken(this ITokenService tokens, EmployeeDto employee)
    {
        return tokens.GenerateToken(
            userId: employee.Id,
            email: employee.Email,
            role: (employee.Email == "bob@test.cafe") ? "Admin" : "Employee", // Temporary hardcoded admin role
            extraClaims: new Dictionary<string, string>
            {
                { "isActive", employee.IsActive.ToString() },
            }
        );
    }
}
