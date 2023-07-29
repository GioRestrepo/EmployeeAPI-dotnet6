using EmployeesAPI.Application.Models;
using MediatR;

namespace EmployeesAPI.Application.Commands
{
    public class DeleteEmployeeCommand : IRequest<bool>
    {
        
        public string Identification { get; set; }

        public DeleteEmployeeCommand(string identification)
        {
            Identification = identification;
        }
        
    }
}
