using DataAccees.GenericRepository;
using DataAccees.Services.Interfaces;
using DataContext;
using Microsoft.EntityFrameworkCore;
using Models.Entities.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccees.Services.Classes
{
    public  class UserRolesRepository : Repository<UserRole>, IUserRolesRepository
    {
        private DbContext db;
        public UserRolesRepository(DbContext dbContext) : base(dbContext)
        {
            this.db = (this.db ?? (ParsaPanahpoorDBContext)db);
        }

        public void AddRolesToUser(List<int> SelectedRoles, int userid)
        {
            foreach (var item in SelectedRoles)
            {
                UserRole userRole = new UserRole()
                {

                    RoleId = item,
                    UserId = userid

                };
                Insert(userRole);
            }

        }
    }
}
