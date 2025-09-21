using Microsoft.EntityFrameworkCore;
using ParkingSystem.Application.Interfaces;
using ParkingSystem.Infrastructure.Data;
using System.Linq.Expressions;

namespace ParkingSystem.Infrastructure.Repositories
{
    public class GenericRepository<T>(ApplicationDbContext context) : IGenericRepository<T> where T : class
    {
        public async Task<T?> GetByIdAsync(int id) => await context.Set<T>().FindAsync(id);
        public async Task<IEnumerable<T>> GetAllAsync() => await context.Set<T>().ToListAsync();
        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression) => await context.Set<T>().Where(expression).ToListAsync();
        public async Task AddAsync(T entity) => await context.Set<T>().AddAsync(entity);
        public void Update(T entity) => context.Set<T>().Update(entity);
        public void Delete(T entity) => context.Set<T>().Remove(entity);

        public async Task<IEnumerable<T>> GetAllIncludingAsync(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = context.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return await query.ToListAsync();
        }
    }
}
