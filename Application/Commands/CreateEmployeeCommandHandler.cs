using EmployeesAPI.Application.Models;
using EmployeesAPI.Infrastructure.Models;
using EmployeesAPI.Infrastructure.Repository.Interfaces;
using Mapster;
using MediatR;

namespace EmployeesAPI.Application.Commands
{
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, EmployeeViewModel>
    {
        private readonly IEmployeeRepository _employeeRepository;
        public CreateEmployeeCommandHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public async Task<EmployeeViewModel> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            Employee isAlreadyCreated = await _employeeRepository.GetByIdentificationAsync(request.Identification);
            if (isAlreadyCreated is not null)
            {
                throw new Exception("No es posible crear. Ya existe un usuario con esta identificación");
            }

            Employee employeeCreated = await _employeeRepository.CreateAsync(request.Adapt<Employee>());

            return employeeCreated.Adapt<EmployeeViewModel>();
        }
    }
}
