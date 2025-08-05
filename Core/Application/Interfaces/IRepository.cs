using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IRepository<T> where T: class 
    {
        Task<T> GetByIdAsync(int id);
        Task<List<T>> GetAllAsync();
        Task AddAsync(T entity);
        Task UpdateAsync (T entity);
        Task DeleteAsync (int id);
        Task<T> GetByFilterAsync(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes);
        Task<List<T>> ListByFilterAsync<TKey>(
        Expression<Func<T, bool>> filter,
        Expression<Func<T, TKey>>? orderByDescending = null,
        params Expression<Func<T, object>>[] includes);

        Task UpdateRange(IEnumerable<T> entities); //çoklu güncelleme

    }
}
