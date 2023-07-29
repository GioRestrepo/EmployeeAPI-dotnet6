using EmployeesAPI.Application.Commands;
using EmployeesAPI.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EmployeesAPI.Controllers
{
    [ApiController]
    [Route("/api/users")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost(Name = "CreateUser") ]
        public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserCommand createUserCommand)
        {
            UserViewModel createUser = await _mediator.Send(createUserCommand);
            return Ok(createUser);
        }

        [HttpPost("auth", Name = "Login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginCommand loginCommand)
        {
            UserViewModel createLogin = await _mediator.Send(loginCommand);
            return Ok(createLogin);
        }
    }
}
