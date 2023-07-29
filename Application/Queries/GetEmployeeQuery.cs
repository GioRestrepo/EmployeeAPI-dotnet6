using EmployeesAPI.Application.Models;
using MediatR;

namespace EmployeesAPI.Application.Queries
{
    public class GetEmployeeQuery: IRequest<EmployeeViewModel>
    {
        public string Identification { get; set; }
        public GetEmployeeQuery(string identification)
        {
            Identification = identification;
        }
    }
}
