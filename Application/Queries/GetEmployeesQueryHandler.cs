using EmployeesAPI.Application.Models;
using EmployeesAPI.Infrastructure.Models;
using EmployeesAPI.Infrastructure.Repository.Interfaces;
using Mapster;
using MediatR;

namespace EmployeesAPI.Application.Queries
{
    public class GetEmployeesQueryHandler : IRequestHandler<GetEmployeesQuery, List<EmployeeViewModel>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        public GetEmployeesQueryHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public async Task<List<EmployeeViewModel>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
        {
            List<Employee> employees = await _employeeRepository.GetAllAsync();

            return employees.Adapt<List<EmployeeViewModel>>();
        }
    }
}
