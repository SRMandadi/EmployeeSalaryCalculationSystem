using EmployeeSalaryCalculationSystem.MVC.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeSalaryCalculationSystem.MVC.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetEmployees();
        Task<Employee> CreateEmployee(Employee emp);
        Task<Employee> UpdateEmployee(Employee emp);
        Task<bool> DeleteEmployee(int id); 
        Task<Employee> GetEmployeeById(int id);
        Task<Employee> GetEmployeeDetailsByaccessCode(string accessCode); 

    }
}