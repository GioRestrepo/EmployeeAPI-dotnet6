using EmployeesAPI.Application.Services.Interfaces;
using EmployeesAPI.Infrastructure.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace EmployeesAPI.Application.Services
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey key;
        public TokenService(IConfiguration configuration)
        {
            // Obtener la clave secreta
            string secretKey = configuration["JWT:Key"].ToString();
            key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secretKey));
        }
        public string GenerateToken(User user)
        {
            

            // Definir los claims del token
            Claim[] claims = new Claim[]
            {
                new Claim("name", user.Name),
                new Claim("email", user.Email)
            };

            // Definir los parámetros del token
            SecurityTokenDescriptor tokenParams = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(24),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.Aes128CbcHmacSha256)
            };

            // Crear el token
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenParams);

            // Obtener el token en formato string
            return tokenHandler.WriteToken(token);
        }

        public ClaimsPrincipal ValidateToken(string token)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ValidateIssuer = false,
                ValidateAudience = false
            }, out SecurityToken validatedToken);
        }
    }
}
