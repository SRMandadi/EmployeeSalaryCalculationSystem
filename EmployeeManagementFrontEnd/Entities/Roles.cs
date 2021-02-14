using System.Collections.Generic;

namespace EmployeeSalaryCalculationSystem.MVC.Entities
{
    public class Roles
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public decimal? RatePerhour { get; set; }
        public virtual ICollection<Employee> Employee { get; set; }
    }
}