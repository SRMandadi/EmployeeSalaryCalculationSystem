using EmployeeTaskManagement.Infra.Repositories;
using System;
using System.Threading.Tasks;

namespace EmployeeSalaryCalculationSystem.INFRA.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DBContext _context;
        private IEmployeeRepository employeeRepository;
        private IWorkItemRepository taskRepository;
        private IEmployeeTaskRepository employeeTaskRepository;
        private IRoleRepository roleRepository;


        public UnitOfWork(DBContext bookStoreDbContext)
        {
            this._context = bookStoreDbContext;
        }

        public IEmployeeRepository EmployeeRepository
        {
            get
            {
                if (this.employeeRepository == null)
                {
                    this.employeeRepository = new EmployeeRepository(this._context);
                }

                return this.employeeRepository;
            }
        }

        public IWorkItemRepository TaskRepository
        {
            get
            {
                if (this.taskRepository == null)
                {
                    this.taskRepository = new WorkItemRepository(this._context);
                }

                return this.taskRepository;
            }
        }

        public IEmployeeTaskRepository EmployeeTaskRepository
        {
            get
            {
                if (this.employeeTaskRepository == null)
                {
                    this.employeeTaskRepository = new EmployeeTaskRepository(this._context);
                }

                return this.employeeTaskRepository;
            }
        }


        public IRoleRepository RoleRepository
        {
            get
            {
                if (this.roleRepository == null)
                {
                    this.roleRepository = new RoleRepository(this._context);
                }

                return this.roleRepository;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task SaveChangesAsync()
        {
            await this._context.SaveChangesAsync().ConfigureAwait(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }
}
