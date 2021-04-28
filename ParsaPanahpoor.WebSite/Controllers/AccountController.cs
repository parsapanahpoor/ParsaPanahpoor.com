using DataAccees.Interfaces;
using DataAccees.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utilities.Convertors;
using Utilities.Genarator;

namespace ParsaPanahpoor.WebSite.Controllers
{
    public class AccountController : Controller
    {

        private IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }



        #region Register


        [Route("Register")]
        public ActionResult Register()
        {

            return View();
        }
        [HttpPost]
        [Route("Register")]
        public ActionResult Register(RegisterViewModel register)
        {

            if (!ModelState.IsValid)
            {
                return View(register);
            }


            if (_userService.IsExistUserName(register.UserName))
            {
                ModelState.AddModelError("UserName", "نام کاربری معتبر نمی باشد");
                return View(register);
            }

            if (_userService.IsExistEmail(FixedText.FixEmail(register.Email)))
            {
                ModelState.AddModelError("Email", "ایمیل معتبر نمی باشد");
                return View(register);
            }
            if (_userService.IsExistPhoneNumber(FixedText.FixEmail(register.PhoneNumber)))
            {
                ModelState.AddModelError("PhoneNumber", "شماره ی وارد شده معتبر نمی باشد ");
                return View(register);
            }




            _userService.AddUser(register);

            //#region Send Activation Email

            //string body = _viewRender.RenderToStringAsync("_ActiveEmail", user);
            //SendEmail.Send(user.Email, "فعالسازی", body);

            //#endregion

            return Redirect("/");
        }

        #endregion

    }
}
