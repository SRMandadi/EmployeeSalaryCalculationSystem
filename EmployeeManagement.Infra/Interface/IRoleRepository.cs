using EmployeeSalaryCalculationSystem.INFRA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeSalaryCalculationSystem.INFRA.Repositories
{
    public interface IRoleRepository : IGenericRepository<Roles>
    {
        Task<IEnumerable<Roles>> Find(string searchText);
    }
}
