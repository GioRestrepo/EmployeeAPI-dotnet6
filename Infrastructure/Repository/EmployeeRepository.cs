using EmployeesAPI.Infrastructure.Models;
using EmployeesAPI.Infrastructure.Repository.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace EmployeesAPI.Infrastructure.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IMongoCollection<Employee> _collection;

        public EmployeeRepository(IMongoCollection<Employee> collection)
        {
            _collection = collection;
        }
        public async Task<Employee> CreateAsync(Employee employee)
        {
            await _collection.InsertOneAsync(employee);
            return employee;
        }
        public async Task<List<Employee>> GetAllAsync()
        {
            return await _collection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<Employee> GetByIdentificationAsync(string identification)
        {
            FilterDefinition<Employee> filter = Builders<Employee>.Filter.Eq(x => x.Identification, identification);

            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<Employee> UpdateAsync(Employee employeeNewData, string searchIdentification)
        {
            FilterDefinition<Employee> filter = Builders<Employee>.Filter.
                Eq(employee => employee.Identification, searchIdentification);
            UpdateDefinition<Employee> update = Builders<Employee>.Update
                .Set(x => x.Name, employeeNewData.Name)
                .Set(x => x.LastName, employeeNewData.LastName)
                .Set(x => x.Identification, employeeNewData.Identification)
                .Set(x => x.Email, employeeNewData.Email)
                .Set(x => x.Role, employeeNewData.Role)
                .Set(x => x.Salary, employeeNewData.Salary)
                .Set(x => x.Age, employeeNewData.Age)
                .Set(x => x.Gender, employeeNewData.Gender)
                .Set(x => x.YearsInCompany, employeeNewData.YearsInCompany)
                .Set(x => x.Active, employeeNewData.Active);

            var updateResult = await _collection.UpdateOneAsync(filter, update);
            if (updateResult.IsAcknowledged && updateResult.ModifiedCount > 0)
            {
                return employeeNewData;
            }

            throw new Exception("Employee not found");
        }
        public async Task<bool> DeleteAsync(string identification)
        {
            FilterDefinition<Employee> filter = Builders<Employee>.Filter.Eq(x => x.Identification, identification);

            DeleteResult deleteResult = await _collection.DeleteOneAsync(filter);
            return (deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0);
        }
    }
}
