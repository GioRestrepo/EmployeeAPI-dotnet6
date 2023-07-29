using EmployeesAPI.Application.Models;
using EmployeesAPI.Application.Services.Interfaces;
using EmployeesAPI.Infrastructure.Models;
using EmployeesAPI.Infrastructure.Repository.Interfaces;
using Mapster;
using MediatR;

namespace EmployeesAPI.Application.Commands
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserViewModel>
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        public CreateUserCommandHandler(IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public async Task<UserViewModel> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            User existUser =  await _userRepository.GetUserAsync(request.Email);
            if (existUser is not null)
            {
                throw new Exception("Usuario ya existe");
            }

            string salt = BCrypt.Net.BCrypt.GenerateSalt(10);
            string hash = BCrypt.Net.BCrypt.HashPassword(request.Password, salt);

            request.Password = hash;

            _ = await _userRepository.CreateAsync(request.Adapt<User>());

            string token = _tokenService.GenerateToken(request.Adapt<User>());

            return new UserViewModel()
            {
                AccessToken = token,
                ExpirationDate = DateTime.UtcNow.AddHours(24)
            };
        }
    }
}
