using DataAccees.GenericRepository;
using DataAccees.Services.Interfaces;
using DataAccees.ViewModels;
using DataContext;
using Microsoft.EntityFrameworkCore;
using Models.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilities.Convertors;
using Utilities.Genarator;

namespace DataAccees.Services.Classes
{
   public class UserRepository : Repository<User>, IUserRepository
    {

        private readonly DbContext db;
        public UserRepository(DbContext dbContext) : base(dbContext)
        {
            this.db = (this.db ?? (ParsaPanahpoorDBContext)db);
        }

        public int AddUser(RegisterViewModel register)
        {
            Models.Entities.User.User user = new Models.Entities.User.User()
            {
                ActiveCode = RandomNumberGenerator.GetNumber(),
                Email = FixedText.FixEmail(register.Email),
                IsActive = true,
                PhoneNumber = register.PhoneNumber,
                Password = register.Password,
                RegisterDate = DateTime.Now,
                UserAvatar = "Defult.jpg",
                UserName = register.UserName
            };

            Insert(user);
            return user.UserId;
        }

        public bool IsExistPhonenumber(string Phonenumber)
        {
            return GetAll().Any(p => p.PhoneNumber == Phonenumber);
        }

        public bool IsExistUserName(string userName)
        {
            var users = GetAll().Any(p => p.UserName == userName);
            return users;
        }

        public bool IsExitEmail(string Email)
        {
            return GetAll().Any(p => p.Email == Email);
        }
    }
}
