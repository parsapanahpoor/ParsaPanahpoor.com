using DataAccees.Services.Classes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccees.UnitOfWork
{
    public class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : DbContext, new()
    {
     


        #region Fileds
        protected readonly DbContext db;

        #endregion



        #region Ctor
        public UnitOfWork()
        {
            db = new TContext();
        }

        #endregion


        #region Implement

        public void SaveChangesDB()
        {
            db.SaveChanges();
        }

        public Task<int> SaveChangesDBAsync()
        {
            return db.SaveChangesAsync();
        }

        #endregion

        #region Repositories
        private UserRepository userRepository;

        public UserRepository UserRepository
        {
            get
            {
                if (userRepository == null)
                {
                    userRepository = new UserRepository(db);
                }

                return userRepository;
            }
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
