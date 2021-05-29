using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccees.GenericRepository
{
    public abstract class Repository<TEntity> : IRepository<TEntity>, IDisposable where TEntity : class
    {

        private readonly DbContext db;

        #region Private Field dbSet

        private DbSet<TEntity> dbSet

        {

            get
            {

                return db.Set<TEntity>();

            }

        }

        #endregion


        #region Ctor

        public Repository(DbContext dbContext)
        {
            this.db = dbContext;

        }

        #endregion


        #region Implement Of GenericRepository

        public void Delete(object Id)
        {
            var entity = GetById(Id);

            if (entity == null)
                throw new ArgumentException("on entity");

            dbSet.Remove(entity);
        }

        public void Delete(TEntity entity)
        {
            dbSet.Remove(entity);
        }

        public void Delete(Expression<Func<TEntity, bool>> where)
        {
            IEnumerable<TEntity> objects = dbSet.Where(where).AsEnumerable();

            foreach (var item in objects)
            {
                dbSet.Remove(item);
            }

        }


        public TEntity Get(Expression<Func<TEntity, bool>> where)
        {
            return dbSet.FirstOrDefault(where);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return dbSet.AsEnumerable();
        }

        public TEntity GetById(object Id)
        {
            return dbSet.Find(Id);
        }

        public IEnumerable<TEntity> GetMany(Expression<Func<TEntity, bool>> where)
        {
            return dbSet.Where(where).AsEnumerable();
        }

        public void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public void Update(TEntity entity)
        {
            dbSet.Update(entity);
        }




        #endregion

    
        #region Dispose

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion


    }
}
