using EmployeeSalaryCalculationSystem.INFRA.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeSalaryCalculationSystem.INFRA.Repositories
{
    public class WorkItemRepository : GenericRepository<WorkItem>, IWorkItemRepository
    {
        public WorkItemRepository(DBContext context) : base(context)
        {
        }

        public async Task<IEnumerable<WorkItem>> Find(string searchText)
        {
            return await this._context.Set<WorkItem>()
                .AsQueryable()
                .Where(e => e.Name == searchText)
                .AsNoTracking()
                .ToListAsync()
                .ConfigureAwait(false);
        }
    }
}
