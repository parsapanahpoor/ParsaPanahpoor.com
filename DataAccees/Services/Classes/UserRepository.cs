using DataAccees.GenericRepository;
using DataAccees.Services.Interfaces;
using DataAccees.UnitOfWork;
using DataAccees.ViewModels;
using DataContext;
using Etecsho.Utilities.Genarator;
using Microsoft.EntityFrameworkCore;
using Models.Entities.User;
using System;
using System.Collections.Generic;
using System.IO;
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

        public User AddUserFromAdmin(CreateUserViewModel user)
        {
            User addUser = new User();
            addUser.Password = user.Password;
            addUser.PhoneNumber = user.PhoneNumber;
            addUser.ActiveCode = Utilities.Genarator.RandomNumberGenerator.GetNumber();
            addUser.Email = user.Email;
            addUser.IsActive = true;
            addUser.RegisterDate = DateTime.Now;
            addUser.UserName = user.UserName;
            addUser.IsDelete = false;



            #region Save Avatar

            if (user.UserAvatar != null)
            {
                string imagePath = "";
                addUser.UserAvatar = NameGenerator.GenerateUniqCode() + Path.GetExtension(user.UserAvatar.FileName);
                imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/UserAvatar", addUser.UserAvatar);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    user.UserAvatar.CopyTo(stream);
                }
            }
            else
            {
                addUser.UserAvatar = "Defult.jpg";
            }

            #endregion



            return addUser;
        }

        public User EditUserFromAdmin(EditUserViewModel editUser)
        {
            User user = GetById(editUser.UserId);
            user.Email = editUser.Email;
            user.PhoneNumber = editUser.PhoneNumber;
            if (!string.IsNullOrEmpty(editUser.Password))
            {
                user.Password = editUser.Password;
            }

            if (editUser.UserAvatar != null)
            {
                //Delete old Image
                if (editUser.AvatarName != "Defult.jpg")
                {
                    string deletePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/UserAvatar", editUser.AvatarName);
                    if (File.Exists(deletePath))
                    {
                        File.Delete(deletePath);
                    }
                }

                //Save New Image
                user.UserAvatar = NameGenerator.GenerateUniqCode() + Path.GetExtension(editUser.UserAvatar.FileName);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/UserAvatar", user.UserAvatar);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    editUser.UserAvatar.CopyTo(stream);
                }
            }
            return user;
        }

        public SideBarUserPanelViewModel GetSideBarUserPanelData(string username)
        {
            return GetAll().Where(p => p.UserName == username)
                        .Select(p => new SideBarUserPanelViewModel()
                        {

                            UserName = p.UserName,
                            ImageName = p.UserAvatar



                        }).Single();
        }

        public EditUserViewModel GetUserForShowInEditMode(int id)
        {

            var user = GetAll(includeProperties: "UserRoles").FirstOrDefault(p=>p.UserId == id);

            EditUserViewModel customer = new EditUserViewModel()
            {
                UserId = user.UserId,
                PhoneNumber = user.PhoneNumber,
                AvatarName = user.UserAvatar,
                Password = user.Password,
                Email = user.Email,
                UserName = user.UserName,
                UserRoles = user.UserRoles.Select(r => r.RoleId).ToList()
            };

            return customer;
        }

        public List<User> GetUsers()
        {
            return GetAll().ToList();
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

        public User Login(LoginViewModel login)
        {
            string PhoneNumber = FixedText.FixEmail(login.phoneNumber);
            return Get(p => p.PhoneNumber == PhoneNumber && p.Password == login.Password);
        }
    }
}
