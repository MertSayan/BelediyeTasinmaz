using Application.Constans;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

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
            if (value != null)
            {
                _dbSet.Remove(value);
                await _context.SaveChangesAsync();
            }
            throw new Exception();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
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
    }
}
