using System.Collections.Generic;

namespace EmployeeSalaryCalculationSystem.INFRA.Models
{
    public partial class WorkItem
    {
        public WorkItem()
        {
            EmployeeTask = new HashSet<EmployeeTask>();
        }

        public int TaskId { get; set; }
        public string Name { get; set; }
        public int NoOfHours { get; set; }
        public bool? IsCompleted { get; set; }

        public virtual ICollection<EmployeeTask> EmployeeTask { get; set; }
    }
}
