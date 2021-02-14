using EmployeeSalaryCalculationSystem.INFRA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeSalaryCalculationSystem.INFRA.Repositories
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        Task<IEnumerable<Employee>> Find(string searchText);

        Task<Employee> GetEmployeeDetailsByaccessCode(string accessCode);
    }
}
