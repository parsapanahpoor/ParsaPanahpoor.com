using DataAccees.GenericRepository;
using DataAccees.UnitOfWork;
using Models.Entities.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccees.Services.Interfaces
{
   public  interface IRoleRepository : IRepository<Models.Entities.User.Role>
    {

        List<Role> GetRoles();

    }
}
