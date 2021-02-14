using EmployeeSalaryCalculationSystem.INFRA.Models;
using EmployeeSalaryCalculationSystem.INFRA.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeTaskManagement.Infra.Repositories
{
    public class EmployeeTaskRepository : GenericRepository<EmployeeTask>, IEmployeeTaskRepository
    {
        public EmployeeTaskRepository(DBContext context) : base(context)
        {
        }

        public async Task<IEnumerable<EmployeeTask>> Find(string searchText)
        {
            return await this._context.Set<EmployeeTask>()
                .AsQueryable()
                .AsNoTracking()
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public override async Task<IEnumerable<EmployeeTask>> GetAll()
        {
            return await this._context.Set<EmployeeTask>()
                .Include(x => x.Employee).ThenInclude(x => x.Role)
                .Include(x => x.Task)
                .AsQueryable()
                .AsNoTracking()
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public override async Task<EmployeeTask> Get(int id)
        {
            var e = await this._context.Set<EmployeeTask>()
                .Include(x => x.Employee).ThenInclude(x => x.Role)
                .Include(x => x.Task)
                .AsQueryable()
                .Where(e => e.EmployeeTaskId == id)
                .AsNoTracking()
                .ToListAsync()
                .ConfigureAwait(false); 

            return e.FirstOrDefault();
        }
    }
}
