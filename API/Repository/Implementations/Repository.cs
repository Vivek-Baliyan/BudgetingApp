using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using API.Data;
using API.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repository.Implementations
{
    public class Repository<T>(DataContext db) : IRepository<T> where T : class
    {
        internal DbSet<T> _dbSet = db.Set<T>();

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync(string includeroperties = null)
        {
            IQueryable<T> query = _dbSet;
            if (includeroperties != null)
            {
                foreach (var includeProp in includeroperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return await query.ToListAsync();
        }

        public async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter, string includeroperties = null)
        {
            IQueryable<T> query = _dbSet.Where(filter);
            if (includeroperties != null)
            {
                foreach (var includeProp in includeroperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetFilteredAsync(Expression<Func<T, bool>> filter, string includeroperties = null)
        {
            IQueryable<T> query = _dbSet.Where(filter);
            if (includeroperties != null)
            {
                foreach (var includeProp in includeroperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return await query.ToListAsync();
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> filter)
        {
            return await _dbSet.AnyAsync(filter);
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            _dbSet.RemoveRange(entity);
        }
    }
}