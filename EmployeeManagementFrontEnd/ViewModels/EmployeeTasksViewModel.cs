using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EmployeeSalaryCalculationSystem.MVC.Entities;

namespace EmployeeSalaryCalculationSystem.MVC.ViewModels
{
    public class EmployeeTasksViewModel
    {
        public int EmployeeTaskId { get; set; }
        public int EmployeeId { get; set; }
        public int TaskId { get; set; }

        [Required(ErrorMessage = "Please Enter No Of Hours.")]
        [Display(Name = "No Of Hours")]
        public int TotalNoOfHours { get; set; }
        [Display(Name = "Date")]
        public DateTime? CurrentDate { get; set; }
        public string Priority { get; set; }

        [Display(Name = "Pay Per Task")]
        public decimal? PayPerTask { get; set; }

        [Display(Name = "Employee Name")]
        public string EmployeeName { get; set; }

        [Display(Name = "Task Name")]
        public string TaskName { get; set; }

        public int EmployeeCapacity { get; set; }

        public virtual EmployeeViewModel Employee { get; set; }

        public virtual WorkItemViewModel Task { get; set; }

        public IEnumerable<EmployeeViewModel> EmployeeList { get; set; }

        public IEnumerable<WorkItemViewModel> WorkItemList { get; set; }

        public IEnumerable<Employee> Employees { get; set; }

        public IEnumerable<WorkItem> WorkItems { get; set; }




    }
}
