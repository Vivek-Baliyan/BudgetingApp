using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace API.Repository.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter, string includeroperties = null);
        Task<bool> AnyAsync(Expression<Func<T, bool>> filter);
        Task<IEnumerable<T>> GetAllAsync(string includeroperties = null);
        Task<IEnumerable<T>> GetFilteredAsync(Expression<Func<T, bool>> filter, string includeroperties = null);
        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);
    }
}