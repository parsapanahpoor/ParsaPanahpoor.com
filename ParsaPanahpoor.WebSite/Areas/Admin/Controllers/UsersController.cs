using DataAccees.UnitOfWork;
using DataAccees.ViewModels;
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

        public IActionResult Create()
        {

            ViewData["Roles"] = _context.RoleRepository.GetRoles();


            return View();
        
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateUserViewModel user, List<int> SelectedRoles)
        {
            if (ModelState.IsValid)
            {
                var  customer = _context.UserRepository.AddUserFromAdmin(user);

                _context.UserRepository.Insert(customer);
                _context.SaveChangesDB();

                _context.UserRolesRepository.AddRolesToUser(SelectedRoles , customer.UserId);
                _context.SaveChangesDB();

                return Redirect("/Admin/Users/Index?Create=true");
            }
            return View(user);
        }
    }
}
