using DataAccees.UnitOfWork;
using DataContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParsaPanahpoor.WebSite.Areas.Admin.Controllers
{   
        [Area("Admin")]
        [Authorize]
    public class UsersController : Controller
    {
        private readonly UnitOfWork<ParsaPanahpoorDBContext> _context;

        public UsersController(UnitOfWork<ParsaPanahpoorDBContext> context)
        {
            _context = context;
        }

        public IActionResult Index(int?id , bool Create =  false , bool Edit = false , bool Delete= false)
        {
            ViewBag.Create = Create;
            ViewBag.Edit = Edit;
            ViewBag.Delete = Delete;

            if (id != null)
            {

                return View(User);
            }

            var user = _context.UserRepository.GetUsers();
            
            return View(user);
        }
    }
}
