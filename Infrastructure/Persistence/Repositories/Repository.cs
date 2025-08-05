using Application.Constans;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System.Linq.Expressions;

namespace Persistence.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly HobiContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(HobiContext context)
        {
            _context = context;
            
            _dbSet= context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var value = await _dbSet.FindAsync(id);
            if (value == null)
                throw new Exception(Messages<T>.EntityNotFound); // istersen kendi NotFound mesajını kullan

            _dbSet.Remove(value);
            await _context.SaveChangesAsync();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByFilterAsync(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>();

            foreach (var include in includes)
                query = query.Include(include);

            return await query.FirstOrDefaultAsync(filter);
        }

        public async Task<List<T>> ListByFilterAsync<TKey>(
             Expression<Func<T, bool>> filter,
             Expression<Func<T, TKey>>? orderByDescending = null,
             params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>();

            foreach (var include in includes)
                query = query.Include(include);

            query = query.Where(filter);

            // Eğer sıralama verilmişse onu uygula
            if (orderByDescending != null)
            {
                query = query.OrderByDescending(orderByDescending);
            }
            else
            {
                // Sıralama verilmemişse varsayılan olarak "CreatedAt" varsa ona göre sırala
                var property = typeof(T).GetProperty("CreatedAt");
                if (property != null)
                {
                    var parameter = Expression.Parameter(typeof(T), "x");
                    var body = Expression.Property(parameter, property);
                    var converted = Expression.Convert(body, typeof(object));
                    var lambda = Expression.Lambda<Func<T, object>>(converted, parameter);
                    query = query.OrderByDescending(lambda);
                }
                else
                {
                    // "CreatedAt" yoksa "Id" alanını dener
                    property = typeof(T).GetProperty("Id");
                    if (property != null)
                    {
                        var parameter = Expression.Parameter(typeof(T), "x");
                        var body = Expression.Property(parameter, property);
                        var converted = Expression.Convert(body, typeof(object));
                        var lambda = Expression.Lambda<Func<T, object>>(converted, parameter);
                        query = query.OrderByDescending(lambda);
                    }
                }
            }

            return await query.ToListAsync();
        }




        public async Task<T> GetByIdAsync(int id)
        {
            var entity= await _dbSet.FindAsync(id);
            if(entity != null)
            {
                return entity;
            }
            throw new Exception(Messages<T>.EntityNotFound);
        }

        

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRange(IEnumerable<T> entities)
        {
            _dbSet.UpdateRange(entities);
            await _context.SaveChangesAsync();
        }
    }
}
