using EmployeeSalaryCalculationSystem.INFRA.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeSalaryCalculationSystem.INFRA.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(DBContext _context)
            : base(_context)
        {
        }

        public override async Task<IEnumerable<Employee>> GetAll()
        {
            return await this._context.Set<Employee>()
                .Include(x => x.Role)
                .Include(x => x.EmployeeTask).ThenInclude(y => y.Task)
                .AsQueryable()
                .AsNoTracking()
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<Employee>> Find(string searchText)
        {
            return await this._context.Set<Employee>()
                .Include(x => x.Role)
                .AsQueryable()
                .Where(e => e.FirstName == searchText || e.Surname == searchText)
                .AsNoTracking()
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public async Task<Employee> GetEmployeeDetailsByaccessCode(string accessCode)
        {
            var employees = await this._context.Set<Employee>()
                .Include(x => x.Role)
                .AsQueryable()
                .Where(e => e.AccessCode == accessCode)
                .AsNoTracking()
                .ToListAsync()
                .ConfigureAwait(false);

            return employees.FirstOrDefault();
        }
    }
}
