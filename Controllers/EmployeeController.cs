using EmployeesAPI.Application.Commands;
using EmployeesAPI.Application.Filter;
using EmployeesAPI.Application.Models;
using EmployeesAPI.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EmployeesAPI.Controllers
{
    [ServiceFilter(typeof(JwtAuthorizationFilter))]
    [ApiController]
    [Route("/api/employees")]
    public class EmployeeController : ControllerBase 
    {
        private readonly IMediator _mediator;
        public EmployeeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(Name = "GetEmployees")]
        public async Task<IActionResult> GetEmployeesAsync()
        {
            List<EmployeeViewModel> employees = await _mediator.Send(new GetEmployeesQuery());

            return Ok(employees);
        }

        [HttpGet("{identification}", Name = "GetEmployee")]
        public async Task<IActionResult> GetEmployeeAsync(string identification)
        {
            EmployeeViewModel employe = await _mediator.Send(new GetEmployeeQuery(identification));
            return Ok(employe); 
        }

        [HttpPost(Name = "CreateEmployee")]
        public async Task<IActionResult> CreateEmployeeAsync([FromBody] CreateEmployeeCommand createEmployeeCommand)
        {
            EmployeeViewModel createEmployee = await _mediator.Send(createEmployeeCommand);
            return Ok(createEmployee);
        }

        [HttpPut("{id}", Name = "UpdateEmployee")]
        public async Task<IActionResult> UpdateEmployeeAsync(string id, UpdateEmployeeCommand updateEmployeeCommand)
        {
            updateEmployeeCommand.SetSearchIdentification(id);
            EmployeeViewModel updateEmployee = await _mediator.Send(updateEmployeeCommand);
            return Ok(updateEmployee);
        }

        [HttpDelete("{id}", Name = "DeleteEmployee")]
        public async Task<IActionResult> DeleteEmployeeAsync(string id)
        {
            bool deleteEmployee = await _mediator.Send(new DeleteEmployeeCommand(id));
            return Ok(deleteEmployee);
        }
    }
}
