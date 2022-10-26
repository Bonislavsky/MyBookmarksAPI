using Microsoft.EntityFrameworkCore;
using MyBookmarksAPI.DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBookmarksAPI.DAL.Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected MyBookmarksDbContext _dbContext { get; set; }

        public RepositoryBase(MyBookmarksDbContext dbContext)
        {
            _dbContext = dbContext;
        }
            
        public abstract Task<T> GetById(long id);

        public virtual async Task<T> Create(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public async void Update(T entity)
        {
            _dbContext.Attach(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
