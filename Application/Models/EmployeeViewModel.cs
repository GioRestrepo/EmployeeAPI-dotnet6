﻿namespace EmployeesAPI.Application.Models
{
    public class EmployeeViewModel
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Identification { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public decimal Salary { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public float YearsInCompany { get; set; }
        public bool Active { get; set; }
    }
}
