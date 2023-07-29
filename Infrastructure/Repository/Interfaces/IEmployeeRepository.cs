using EmployeesAPI.Infrastructure.Models;

namespace EmployeesAPI.Infrastructure.Repository.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<Employee> CreateAsync(Employee employee);
        Task<List<Employee>> GetAllAsync();
        Task<Employee> GetByIdentificationAsync(string identification);
        Task<Employee> UpdateAsync(Employee employeeNewData, string searchIdentification);
        Task<bool> DeleteAsync(string identification);
    }
}
