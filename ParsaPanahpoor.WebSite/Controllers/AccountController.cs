using DataAccees.Interfaces;
using DataAccees.UnitOfWork;
using DataAccees.ViewModels;
using DataContext;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Utilities.Convertors;
using Utilities.Genarator;

namespace ParsaPanahpoor.WebSite.Controllers
{
    public class AccountController : Controller
    {

        private readonly UnitOfWork<ParsaPanahpoorDBContext> _context;

        public AccountController( UnitOfWork<ParsaPanahpoorDBContext> context)
        {
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

            return Redirect("/Home");
        }

        #endregion


        #region Login
        [Route("Login")]
        public IActionResult Login()
        {

            return View();
        }

        [Route("Login")]
        [HttpPost]
        public IActionResult Login(LoginViewModel login)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }


            var user =_context.UserRepository.Login(login);
            if (user != null)
            {
                if (user.IsActive)
                {
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
                        new Claim(ClaimTypes.Name,user.UserName)
                    };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    var properties = new AuthenticationProperties
                    {
                        IsPersistent = login.RememberMe
                    };
                    HttpContext.SignInAsync(principal, properties);


                    ViewBag.IsSuccess = true;
                    return Redirect("/");
                }
                else
                {
                    ModelState.AddModelError("phoneNumber", "حساب کاربری شما فعال نمی باشد");
                }
            }
            ModelState.AddModelError("phoneNumber", "کاربری با مشخصات وارد شده یافت نشد");
            return View(login);
        }





        #endregion

        #region Logout
        [Route("Logout")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/Login");
        }

        #endregion


    }
}
