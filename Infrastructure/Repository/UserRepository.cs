using EmployeesAPI.Infrastructure.Models;
using EmployeesAPI.Infrastructure.Repository.Interfaces;
using MongoDB.Driver;

namespace EmployeesAPI.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _collection;

        public UserRepository(IMongoCollection<User> collection)
        {
            _collection = collection;
        }
        public async Task<User> CreateAsync(User user)
        {
            await _collection.InsertOneAsync(user);
            return user;
        }
        public async Task<User> GetUserAsync(string email)
        {
            FilterDefinition<User> filter = Builders<User>.Filter.Eq(x => x.Email, email);

            return await _collection.Find(filter).FirstOrDefaultAsync();
        }
    }
}
