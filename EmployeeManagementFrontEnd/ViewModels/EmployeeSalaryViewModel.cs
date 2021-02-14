using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EmployeeSalaryCalculationSystem.MVC.Entities;

namespace EmployeeSalaryCalculationSystem.MVC.ViewModels
{
    public class EmployeeSalaryViewModel
    {

        [Display(Name = "Employee Id")]
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Surname { get; set; }

        [Required(ErrorMessage = "Please Select Start Date.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Please enter your secured Access Code.")]
        [Display(Name = "Access Code")]
        public string AccessCode { get; set; }
        public string EmployeeRoleName { get; set; }

        [Required(ErrorMessage = "Please Select Start Date.")]
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }

        [Required(ErrorMessage = "Please Select End Date.")]
        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }
        [Display(Name = "Total No Of Hours Worked")]
        public int TotalNoOfHoursWorked { get; set; }

        public decimal Salary { get; set; }

        public int RoleId { get; set; }

        public bool DispalyGrid { get; set; }

        public IEnumerable<Employee> Employees { get; set; }
    }
}
