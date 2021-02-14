using System.Collections.Generic;

namespace EmployeeSalaryCalculationSystem.MVC.Entities
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string EmployeeCode { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Surname { get; set; }
        public string MobileNo { get; set; }
        public string EmailAddress { get; set; }
        public string PhysicalAddress { get; set; }
        public string AccessCode { get; set; }
        public int RoleId { get; set; }
        public virtual Roles Role { get; set; }
        public virtual ICollection<EmployeeTask> EmployeeTask { get; set; }
    }
}
