using EmployeeSalaryCalculationSystem.INFRA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeSalaryCalculationSystem.INFRA.Repositories
{
    public interface IWorkItemRepository : IGenericRepository<WorkItem>
    {
        Task<IEnumerable<WorkItem>> Find(string searchText);
    }
}
