using DataAccees.Services.Classes;
using DataAccees.Services.Interfaces;
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

        #region UserRepository
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

        #region RoleRepository
        private RoleRepository roleRepository;
        public RoleRepository RoleRepository
        {
            get
            {
                if (roleRepository == null)
                {
                    roleRepository = new RoleRepository(db);
                }

                return roleRepository;
            }
        }

        #endregion

        #region PermissionRepository

        private PermissionRepository permissionRepository;
        public PermissionRepository PermissionRepository
        {
            get
            {
                if (permissionRepository == null)
                {
                    permissionRepository = new PermissionRepository(db);
                }

                return permissionRepository;
            }
        }


        #endregion

        #region
     private UserRolesRepository userRolesRepository;
        public UserRolesRepository UserRolesRepository
        {
            get
            {
                if (userRolesRepository == null)
                {
                    userRolesRepository = new UserRolesRepository(db);
                }

                return userRolesRepository;
            }
        }


        #endregion

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
