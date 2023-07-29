namespace EmployeesAPI.Infrastructure.Settings
{
    public class MongoSettings
    {
        public const string SectionName = "MongoSettigns";
        public string ConnectionString { get; set; }
        public string Database { get; set; }
        public MongoCollectionsSettings Collections { get; set; }

    }
    public class MongoCollectionsSettings
    {
        public string Employees { get; set; }
        public string Users { get; set; }
    }
}
