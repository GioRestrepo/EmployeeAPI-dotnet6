using EmployeesAPI.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace EmployeesAPI.Application.Filter
{
    public class JwtAuthorizationFilter : IAuthorizationFilter
    {
        private readonly ITokenService _tokenService;
        public JwtAuthorizationFilter(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Obtener el token del header Authorization
            var authorizationHeader = context.HttpContext.Request.Headers["Authorization"].ToString();

            if (authorizationHeader != null && authorizationHeader.StartsWith("Bearer "))
            {
                // Removemos el prefijo Bearer
                string token = authorizationHeader.Substring("Bearer ".Length).Trim();

                try
                {
                    // Validamos el token y sacamos los contactos principales
                    ClaimsPrincipal principalClaims = _tokenService.ValidateToken(token);

                    // guardamos los claims principales en el contexto por si llegaramos a necesitarlos
                    context.HttpContext.User = principalClaims;
                }
                catch
                {
                    // El token no es válido o ha expirado
                    context.Result = new UnauthorizedResult();
                }
            }
            else
            {
                // No hay token en los headers
                context.Result = new UnauthorizedResult();
            }
        }
    }

}
