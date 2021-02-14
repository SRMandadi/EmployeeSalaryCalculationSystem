using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeSalaryCalculationSystem.MVC.Entities; 

namespace EmployeeSalaryCalculationSystem.MVC.Services
{
    public interface IEmployeeTaskService
    {
        Task<IEnumerable<EmployeeTask>> GetEmployeeTasks();
        Task<EmployeeTask> CreateEmployeeTask(EmployeeTask empTask);
        Task<EmployeeTask> UpdateEmployeeTask(EmployeeTask empTask);
        Task<bool> DeleteEmployeeTask(int id);
        Task<EmployeeTask> GetEmployeeTaskById(int id);

        Task<IEnumerable<EmployeeTask>> GetEmpHourCapacityOfTheDate(int id, DateTime startDate, DateTime? endDate);

        Task<IEnumerable<EmployeeAndTaskList>> GetEmployeesAndWorkItems(String searchText);
    }
}
