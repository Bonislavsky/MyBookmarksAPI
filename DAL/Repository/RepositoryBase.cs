using Microsoft.EntityFrameworkCore;
using MyBookmarksAPI.DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyBookmarksAPI.DAL.Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected MyBookmarksDbContext _dbContext { get; set; }

        public RepositoryBase(MyBookmarksDbContext dbContext) => _dbContext = dbContext;

        public Task<T> GetByCondition(Expression<Func<T, bool>> expression) => _dbContext.Set<T>().FirstOrDefaultAsync(expression);

        public virtual async Task<T> Create(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            return entity;
        }

        public async Task<IEnumerable<T>> GetAll() => await _dbContext.Set<T>().ToListAsync();

        public void Delete(T entity) => _dbContext.Set<T>().Remove(entity);

        public void Update(T entity) => _dbContext.Attach(entity).State = EntityState.Modified;

        public async void Save() => await _dbContext.SaveChangesAsync();
    }
}
