using EmployeesAPI.Application.Models;
using MediatR;

namespace EmployeesAPI.Application.Commands
{
    public class CreateUserCommand : IRequest<UserViewModel>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
