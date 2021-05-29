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
   public class UserRepository : Repository<User>, IUserRepository
    {

        private readonly DbContext db;
        public UserRepository(DbContext dbContext) : base(dbContext)
        {
            this.db = (this.db ?? (ParsaPanahpoorDBContext)db);
        }

    }
}
