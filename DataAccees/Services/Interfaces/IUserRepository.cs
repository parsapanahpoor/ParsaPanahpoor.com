using DataAccees.GenericRepository;
using DataAccees.ViewModels;
using Models.Entities.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccees.Services.Interfaces
{
   public interface IUserRepository : IRepository<User>
    {

        //------Definition Private Functions Model -------------//

        bool IsExistUserName(string userName);
        bool IsExitEmail(string Email);
        bool IsExistPhonenumber(string Phonenumber);
        int AddUser(RegisterViewModel register);
        User Login(LoginViewModel login);


        #region AdminPanel


        User AddUserFromAdmin(CreateUserViewModel user);
        SideBarUserPanelViewModel GetSideBarUserPanelData(string username);
        EditUserViewModel GetUserForShowInEditMode(int id );
        List<User> GetUsers();

        User EditUserFromAdmin(EditUserViewModel editUser);

        #endregion

    }
}
