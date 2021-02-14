using System;
using System.ComponentModel.DataAnnotations;

namespace EmployeeSalaryCalculationSystem.MVC.ViewModels
{
    public class EmployeesTotalHoursPerDayViewModel
    {
        [Display(Name = "Employee Id")]
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Surname { get; set; }

        public string FullName { get; set; }


        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        public DateTime? Date { get; set; }

        [Display(Name = "Total Hours")]
        public int TotalHours { get; set; }
    }
}
