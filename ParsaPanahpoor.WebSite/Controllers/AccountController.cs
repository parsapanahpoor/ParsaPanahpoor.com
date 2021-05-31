using DataAccees.Interfaces;
using DataAccees.UnitOfWork;
using DataAccees.ViewModels;
using DataContext;
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

        //private IUserService _userService;
        private readonly UnitOfWork<ParsaPanahpoorDBContext> _context;

        public AccountController(/*IUserService userService ,*/ UnitOfWork<ParsaPanahpoorDBContext> context)
        {
            //_userService = userService;
            _context = context;
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


            if (_context.UserRepository.IsExistUserName(register.UserName))
            {
                ModelState.AddModelError("UserName", "نام کاربری معتبر نمی باشد");
                return View(register);
            }

            if (_context.UserRepository.IsExitEmail(FixedText.FixEmail(register.Email)))
            {
                ModelState.AddModelError("Email", "ایمیل معتبر نمی باشد");
                return View(register);
            }
            if (_context.UserRepository.IsExistPhonenumber(FixedText.FixEmail(register.PhoneNumber)))
            {
                ModelState.AddModelError("PhoneNumber", "شماره ی وارد شده معتبر نمی باشد ");
                return View(register);
            }




           int userid =  _context.UserRepository.AddUser(register);
           _context.SaveChangesDB();
            //#region Send Activation Email

            //string body = _viewRender.RenderToStringAsync("_ActiveEmail", user);
            //SendEmail.Send(user.Email, "فعالسازی", body);

            //#endregion

            return Redirect("/");
        }

        #endregion

    }
}
