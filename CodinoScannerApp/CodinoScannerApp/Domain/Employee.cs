using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;

namespace CodinoScannerApp.Domain
{
    [BsonIgnoreExtraElements]
    public class Employee
    {
        public string EmployeeId { get; set; }
        public string EmployeeFirstName { get; set; }
        public string EmployeeLastName { get; set; }
        public string Password { get; set; }
        public string EmployeeType { get; set; }

        [BsonConstructor]
        public Employee(string employeeId, string employeeFirstName, string employeeLastName, string password, string employeeType)
        {
            //Changed it to lowercase so that people can login with different cases nd stuff
            EmployeeId = employeeId.ToLower();
            EmployeeFirstName = employeeFirstName;
            EmployeeLastName = employeeLastName;
            Password = password;
            EmployeeType = employeeType;
        }

        public bool ValidatePassword(string inputPassword)
        {
            return BCrypt.Net.BCrypt.Verify(inputPassword, Password);
        }

        public void UpdatePassword(string inputPassword)
        {
            Password = inputPassword;
        }

    }
}
