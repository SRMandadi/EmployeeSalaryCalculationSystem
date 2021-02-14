using EmployeeSalaryCalculationSystem.INFRA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeSalaryCalculationSystem.INFRA.Repositories
{
    public interface IEmployeeTaskRepository : IGenericRepository<EmployeeTask>
    {
        Task<IEnumerable<EmployeeTask>> Find(string searchText);
    }
}
