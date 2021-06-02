using DataAccees.GenericRepository;
using DataAccees.Services.Interfaces;
using DataContext;
using Microsoft.EntityFrameworkCore;
using Models.Entities.Permissions;
using Models.Entities.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccees.Services.Classes
{
    public class PermissionService : Repository<RolePermission>, IPermissionService
    {

        private readonly DbContext db;
        public PermissionService(DbContext dbContext) : base(dbContext)
        {
            this.db = (this.db ?? (ParsaPanahpoorDBContext)db);
        }


        public void AddPermissionsToRole(int roleId, List<int> permission)
        {
            foreach (var p in permission)
            {
                Insert(new RolePermission()
                {

                    PermissionId = p,
                    RoleId = roleId

                });
            }
        }

        public int AddRole(Role role)
        {
            throw new NotImplementedException();
        }

        public void AddRolesToUser(List<int> roleIds, int userId)
        {
            throw new NotImplementedException();
        }

        public bool CheckPermission(int permissionId, string userName)
        {
            throw new NotImplementedException();
        }

        public void DeleteRole(Role role)
        {
            throw new NotImplementedException();
        }

        public void EditRolesUser(int userId, List<int> rolesId)
        {
            throw new NotImplementedException();
        }

        public List<Permission> GetAllPermission()
        {
            throw new NotImplementedException();
        }

        public Role GetRoleById(int roleId)
        {
            throw new NotImplementedException();
        }

        public List<Role> GetRoles()
        {
            throw new NotImplementedException();
        }

        public List<int> PermissionsRole(int roleId)
        {
            throw new NotImplementedException();
        }

        public void UpdatePermissionsRole(int roleId, List<int> permissions)
        {
            throw new NotImplementedException();
        }

        public void UpdateRole(Role role)
        {
            throw new NotImplementedException();
        }
    }
}
