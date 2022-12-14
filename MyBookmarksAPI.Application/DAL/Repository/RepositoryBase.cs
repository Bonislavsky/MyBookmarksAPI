using Microsoft.EntityFrameworkCore;
using MyBookmarksAPI.DAL.Interface;
using MyBookmarksAPI.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyBookmarksAPI.DAL.Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : EntityBase
    {
        protected readonly MyBookmarksDbContext _dbContext;
        protected readonly DbSet<T> _dbSet;

        public RepositoryBase(MyBookmarksDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        public Task<T> GetByCondition(Expression<Func<T, bool>> expression) => _dbSet.AsNoTracking().SingleOrDefaultAsync(expression);

        public IQueryable<T> GetListByCondition(Expression<Func<T, bool>> expression) => _dbSet.Where(expression).AsNoTracking();

        public virtual async Task Create(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public virtual IQueryable<T> GetAll() => _dbSet.AsNoTracking();

        public void Delete(T entity) => _dbSet.Remove(entity);

        public void DeleteRange(IEnumerable<T> listEntity) => _dbSet.RemoveRange(listEntity);

        public void Update(T entity) => _dbContext.Attach(entity).State = EntityState.Modified;

        public async Task<bool> EntityExists(Expression<Func<T, bool>> expression) => await _dbSet.AnyAsync(expression);
    }
}
