using DataAccees.Interfaces;
using DataAccees.ViewModels;
using DataContext;
using Models.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilities.Convertors;
using Utilities.Genarator;

namespace DataAccees.Services
{
    public class UserService : IUserService
    {

        private ParsaPanahpoorDBContext _context;

        public UserService(ParsaPanahpoorDBContext context)
        {
            _context = context;
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

            _context.Users.Add(user);
            _context.SaveChanges();
            return user.UserId;
        }

        public void DeleteUser(int userId)
        {
            User user = GetUserById(userId);
            user.IsDelete = true;
            UpdateUser(user);
        }
        public User GetUserByActiveCode(string ActiveCode)
        {
            return _context.Users.FirstOrDefault(p => p.ActiveCode == ActiveCode);
        }

        public User GetUserByEmail(string email)
        {
            return _context.Users.SingleOrDefault(u => u.Email == email);
        }


        public User GetUserById(int Userid)
        {
            return _context.Users.Find(Userid);
        }

        public User GetUserByPhoneNumber(string PhoneNumber)
        {
            return _context.Users.FirstOrDefault(p => p.PhoneNumber == PhoneNumber.Trim().ToLower());
        }


        public User GetUserByUserName(string username)
        {
            return _context.Users.SingleOrDefault(u => u.UserName == username);
        }

        public int GetUserIdByUserName(string userName)
        {
            return _context.Users.Single(u => u.UserName == userName).UserId;
        }

        public List<int> GetUsersRoles(string username)
        {
            User user = GetUserByUserName(username);
            return _context.UsersRoles.Where(p => p.UserId == user.UserId).Select(p => p.RoleId).ToList();
        }

        public bool IsExistEmail(string email)
        {
            return _context.Users.Any(u => u.Email == email);
        }


        public bool IsExistPhoneNumber(string PhoneNumber)
        {
            return _context.Users.Any(p => p.PhoneNumber == PhoneNumber);
        }
        public bool IsExistUserName(string userName)
        {
            return _context.Users.Any(u => u.UserName == userName);
        }

        public User LoginUser(LoginViewModel login)
        {
            string PhoneNumber = FixedText.FixEmail(login.phoneNumber);
            return _context.Users.SingleOrDefault(u => u.PhoneNumber == PhoneNumber && u.Password == login.Password);
        }

        public void UpdateUser(User user)
        {
            _context.Update(user);
            _context.SaveChanges();
        }

    }
}

