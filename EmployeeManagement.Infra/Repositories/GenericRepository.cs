using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EmployeeSalaryCalculationSystem.INFRA.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly DBContext _context;
        public GenericRepository(DBContext context)
        {
            _context = context;
        }

        public virtual async Task<T> Get(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public virtual async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }


        public virtual T Add(T entity)
        {
            return this._context
                .Add(entity)
                .Entity;
        }

        public virtual T Update(T entity)
        {
            return this._context
                .Update(entity)
                .Entity;
        }

        public virtual T Delete(T entity)
        {
            this._context.Attach(entity);
            return this._context.Remove(entity).Entity;
        }

        public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await this._context.Set<T>()
                .AsQueryable()
                .Where(predicate)
                .ToListAsync()
                .ConfigureAwait(false);
        }
    }
}
