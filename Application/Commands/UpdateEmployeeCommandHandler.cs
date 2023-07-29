using EmployeesAPI.Application.Models;
using EmployeesAPI.Infrastructure.Models;
using EmployeesAPI.Infrastructure.Repository.Interfaces;
using Mapster;
using MediatR;

namespace EmployeesAPI.Application.Commands
{
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, EmployeeViewModel>
    {
        private readonly IEmployeeRepository _employeeRepository;
        public UpdateEmployeeCommandHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public async Task<EmployeeViewModel> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            
            Employee employeeUpdated =  await _employeeRepository.UpdateAsync(request.Adapt<Employee>(), request.SearchIdentification);

            return employeeUpdated.Adapt<EmployeeViewModel>();
        }
    }
}
