using EmployeesAPI.Application.Models;
using EmployeesAPI.Infrastructure.Repository.Interfaces;
using MediatR;

namespace EmployeesAPI.Application.Commands
{
    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, bool>
    {
        private readonly IEmployeeRepository _employeeRepository;
        public DeleteEmployeeCommandHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public async Task<bool> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            return await _employeeRepository.DeleteAsync(request.Identification);
        }
    }
}
