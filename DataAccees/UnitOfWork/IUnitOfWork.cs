using DataAccees.Services.Classes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccees.UnitOfWork
{
    public interface IUnitOfWork<TContext> : IDisposable where TContext : DbContext
    {
        #region Repositories

        UserRepository UserRepository{ get; }
        RoleRepository   RoleRepository{ get; }
        PermissionRepository PermissionRepository{ get; }
        UserRolesRepository UserRolesRepository{ get; }

        #endregion

        void SaveChangesDB();
        Task<int> SaveChangesDBAsync();
    }


}
