using DataAccees.GenericRepository;
using Models.Entities.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccees.Services.Interfaces
{
   public interface IUserRolesRepository : IRepository<UserRole>
    {
        void AddRolesToUser(List<int> SelectedRoles, int userid);
        void EditRolesUser(int userId, List<int> rolesId);

    }
}
