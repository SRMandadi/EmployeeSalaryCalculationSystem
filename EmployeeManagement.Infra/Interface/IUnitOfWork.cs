using System;
using System.Threading.Tasks;

namespace EmployeeSalaryCalculationSystem.INFRA.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IEmployeeRepository EmployeeRepository { get; }
        IEmployeeTaskRepository EmployeeTaskRepository { get; }
        IWorkItemRepository TaskRepository { get; }
        IRoleRepository RoleRepository { get; }
        Task SaveChangesAsync();
    }
}
