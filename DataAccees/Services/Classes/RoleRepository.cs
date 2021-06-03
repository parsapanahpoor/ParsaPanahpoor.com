using DataAccees.GenericRepository;
using DataAccees.Services.Interfaces;
using DataContext;
using Microsoft.EntityFrameworkCore;
using Models.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccees.Services.Classes
{
   public  class RoleRepository : Repository<Models.Entities.User.Role> , IRoleRepository
    {
        private readonly DbContext db;

        public RoleRepository(DbContext dbContext):base(dbContext)
        {
            this.db = (this.db ?? (ParsaPanahpoorDBContext)db);
        }

        public List<Role> GetRoles()
        {
            return GetAll().ToList();
        }

    }
}
