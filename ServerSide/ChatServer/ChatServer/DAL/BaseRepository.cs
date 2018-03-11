using System;
using System.Linq;
using ChatServer.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace ChatServer.DAL
{
    public class BaseRepository: IGenericRepository
    {
        private readonly DataContext _dbContext = null;

        public BaseRepository(DataContext Context)
        {
            _dbContext = Context;
        }
        public virtual void Delete<TEntity>(TEntity entity) where TEntity : BaseClass
        {
            _dbContext.Set<TEntity>().Remove(entity);
            _dbContext.SaveChanges();
        }

        public virtual TEntity GetByID<TEntity>(object id) where TEntity : BaseClass
        {
            return _dbContext.Set<TEntity>().FirstOrDefault(t => t.Id == (Guid)id);
        }

        public virtual IQueryable<TEntity> GetQuery<TEntity>() where TEntity : BaseClass
        {
            DbSet<TEntity> tempSet = _dbContext.Set<TEntity>();
            return tempSet.AsQueryable<TEntity>();
        }

        public virtual void Insert<TEntity>(TEntity entity) where TEntity : BaseClass
        {
            _dbContext.Set<TEntity>().Add(entity);
            _dbContext.SaveChanges();
        }

        public virtual void Update<TEntity>(TEntity entity) where TEntity : BaseClass
        {
            _dbContext.Set<TEntity>().Update(entity);
            _dbContext.SaveChanges();
        }
    }
}
