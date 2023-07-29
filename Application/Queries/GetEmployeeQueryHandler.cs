using EmployeesAPI.Application.Models;
using EmployeesAPI.Infrastructure.Models;
using EmployeesAPI.Infrastructure.Repository.Interfaces;
using Mapster;
using MediatR;

namespace EmployeesAPI.Application.Queries
{
    public class GetEmployeeQueryHandler : IRequestHandler<GetEmployeeQuery, EmployeeViewModel>
    {
        private readonly IEmployeeRepository _employeeRepository;
        public GetEmployeeQueryHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public async Task<EmployeeViewModel> Handle(GetEmployeeQuery request, CancellationToken cancellationToken)
        {
            Employee employee = await _employeeRepository.GetByIdentificationAsync(request.Identification);

            return employee.Adapt<EmployeeViewModel>();
        }
    }
}
