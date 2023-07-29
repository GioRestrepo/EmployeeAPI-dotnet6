using EmployeesAPI.Application.Models;
using MediatR;

namespace EmployeesAPI.Application.Queries
{
    public class GetEmployeesQuery : IRequest<List<EmployeeViewModel>>
    {
    }
}
