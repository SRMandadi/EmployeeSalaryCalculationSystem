using System;

namespace EmployeeSalaryCalculationSystem.INFRA.Models
{
    public partial class EmployeeTask
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
}
