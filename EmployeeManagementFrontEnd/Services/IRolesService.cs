using EmployeeSalaryCalculationSystem.MVC.Entities;
using System.Collections.Generic;
using System.Threading.Tasks; 

namespace EmployeeSalaryCalculationSystem.MVC.Services
{
    public interface IRolesService
    {
        Task<IEnumerable<Roles>> GetRoles();
        Task<Roles> CreateRole(Roles emp);
        Task<Roles> UpdateRole(Roles emp);
        Task<bool> DeleteRole(int id);
        Task<Roles> GetRoleById(int id);
    }
}
