using System.Collections.Generic;
using EmployeeSalaryCalculationSystem.INFRA.Models;

namespace EmployeeSalaryCalculationSystem.WEBAPI.Models
{
    public class EmployeeAndTaskList
    {
        public IEnumerable<Employee> Employees;
        public IEnumerable<WorkItem> WorkItems;
    }
}
