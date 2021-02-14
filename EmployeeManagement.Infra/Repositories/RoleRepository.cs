using EmployeeSalaryCalculationSystem.INFRA.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeSalaryCalculationSystem.INFRA.Repositories
{
    public class RoleRepository : GenericRepository<Roles>, IRoleRepository
    {
        public RoleRepository(DBContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Roles>> Find(string searchText)
        {
            return await this._context.Set<Roles>()
                .AsQueryable()
                .Where(e => e.RoleName == searchText)
                .AsNoTracking()
                .ToListAsync()
                .ConfigureAwait(false);
        }
    }
}
