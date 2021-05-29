using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataAccees.GenericRepository
{
    public interface IRepository<TEntity> where TEntity : class
    {

        #region Public Definition Functions

        //-------Definition Public Functions Models---------//

        void Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(object Id);
        void Delete(TEntity entity);
        void Delete(Expression<Func<TEntity, bool>> where);

        TEntity GetById(object Id);
        IEnumerable<TEntity> GetAll();
        TEntity Get(Expression<Func<TEntity, bool>> where);
        IEnumerable<TEntity> GetMany(Expression<Func<TEntity, bool>> where);


        #endregion


    }
}
