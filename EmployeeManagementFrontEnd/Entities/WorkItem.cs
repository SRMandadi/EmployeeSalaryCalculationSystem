using System;
using System.Collections.Generic;

namespace EmployeeSalaryCalculationSystem.MVC.Entities
{
    public class WorkItem
    {
        public int TaskId { get; set; }
        public string Name { get; set; }
        public int NoOfHours { get; set; }
        public bool? IsCompleted { get; set; }

        public virtual ICollection<EmployeeTask> EmployeeTask { get; set; }
    }
}
