using EmployeesAPI.Application.Models;
using EmployeesAPI.Infrastructure.Models;
using EmployeesAPI.Infrastructure.Repository.Interfaces;
using MediatR;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using EmployeesAPI.Application.Services.Interfaces;

namespace EmployeesAPI.Application.Commands
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, UserViewModel>
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        public LoginCommandHandler(IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }
        public async Task<UserViewModel> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            User user = await _userRepository.GetUserAsync(request.Email);

            if(user is null)
            {
                throw new Exception("Usuario no encontrado");
            }

            bool isAuthorized = BCrypt.Net.BCrypt.Verify(request.Password, user.Password);

            if (isAuthorized is false)
            {
                throw new Exception("Usuario o contraseña incorrectos");
            }

            string token = _tokenService.GenerateToken(user);

            return new UserViewModel()
            { 
                AccessToken = token,
                ExpirationDate = DateTime.UtcNow.AddHours(24)
            };
        }
    }
}
