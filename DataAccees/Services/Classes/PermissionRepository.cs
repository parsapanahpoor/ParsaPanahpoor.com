using DataAccees.Services.Interfaces;
using DataContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security;
using System.Text;

namespace DataAccees.Services.Classes
{
   public  class PermissionRepository : GenericRepository.Repository<Models.Entities.Permissions.Permission> , IPermissionRepository
    {
        private readonly DbContext db;
        public PermissionRepository(DbContext dbContext) : base(dbContext)
        {
            this.db = (this.db ?? (ParsaPanahpoorDBContext)db);
        }

       
    }
}
