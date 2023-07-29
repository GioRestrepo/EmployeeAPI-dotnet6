using EmployeesAPI.Infrastructure.Models;

namespace EmployeesAPI.Infrastructure.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<User> CreateAsync(User user);
        Task<User> GetUserAsync(string email);
    }
}
