using System;
using System.Collections.Generic;

namespace EmployeeSalaryCalculationSystem.MVC.Entities
{
    public class EmployeeTask
    {
        public int EmployeeTaskId { get; set; }
        public int EmployeeId { get; set; }
        public int TaskId { get; set; }
        public int TotalNoOfHours { get; set; }
        public DateTime? CurrentDate { get; set; }
        public string Priority { get; set; }
        public decimal? PayPerTask { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual WorkItem Task { get; set; }
    }

    public class EmployeeAndTaskList
    {
        public IEnumerable<Employee> Employees;
        public IEnumerable<WorkItem> WorkItems;
    }
}
