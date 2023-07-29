using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace EmployeesAPI.Infrastructure.Models
{
    public class User
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
