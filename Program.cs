using EmployeesAPI.Application.Filter;
using EmployeesAPI.Application.Services;
using EmployeesAPI.Application.Services.Interfaces;
using EmployeesAPI.Infrastructure.Models;
using EmployeesAPI.Infrastructure.Repository;
using EmployeesAPI.Infrastructure.Repository.Interfaces;
using EmployeesAPI.Infrastructure.Settings;
using MongoDB.Driver;

namespace EmployeesAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });
            builder.Services.AddMediatR(cfg => cfg
                .RegisterServicesFromAssemblies(typeof(Program).Assembly));


            MongoSettings mongoSetting = new();
            builder.Configuration.GetSection(MongoSettings.SectionName).Bind(mongoSetting);

            MongoClient mongoClient = new(mongoSetting.ConnectionString);
            IMongoDatabase database = mongoClient.GetDatabase(mongoSetting.Database);

            string employeesCollection = mongoSetting.Collections.Employees;
            string usersCollection = mongoSetting.Collections!.Users;
            builder.Services.AddSingleton(s => database.GetCollection<Employee>(employeesCollection));
            builder.Services.AddSingleton(s => database.GetCollection<User>(usersCollection));

            // Services
            builder.Services.AddSingleton<ITokenService, TokenService>();

            // Repositories
            builder.Services.AddSingleton<IUserRepository, UserRepository>();
            builder.Services.AddSingleton<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<JwtAuthorizationFilter>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseCors("CorsPolicy");


            app.MapControllers();

            app.Run();
        }
    }
}