using EmployeesAPI.Infrastructure.Models;
using System.Security.Claims;

namespace EmployeesAPI.Application.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(User user);
        ClaimsPrincipal ValidateToken(string token);
    }
}
