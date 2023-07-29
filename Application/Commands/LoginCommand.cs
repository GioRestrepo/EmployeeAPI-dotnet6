
using EmployeesAPI.Application.Models;
using MediatR;

namespace EmployeesAPI.Application.Commands
{
    public class LoginCommand : IRequest<UserViewModel>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
