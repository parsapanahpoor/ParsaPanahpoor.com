using DataAccees.UnitOfWork;
using DataAccees.ViewModels;
using DataContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Entities.User;
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

        public IActionResult Index(int? id, bool Create = false, bool Edit = false, bool Delete = false)
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
                var customer = _context.UserRepository.AddUserFromAdmin(user);

                _context.UserRepository.Insert(customer);
                _context.SaveChangesDB();

                _context.UserRolesRepository.AddRolesToUser(SelectedRoles, customer.UserId);
                _context.SaveChangesDB();

                return Redirect("/Admin/Users/Index?Create=true");
            }
            return View(user);
        }

        public IActionResult Edit(int id)
        {
          

            EditUserViewModel user = _context.UserRepository.GetUserForShowInEditMode(id);
            ViewData["Roles"] = _context.RoleRepository.GetRoles();

            if (user == null)
            {
                return NotFound();
            }


            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditUserViewModel user, List<int> SelectedRoles)
        {


            if (ModelState.IsValid)
            {
                try
                {
                   User userEdited =   _context.UserRepository.EditUserFromAdmin(user);
                    _context.UserRepository.Update(userEdited);
                    _context.UserRolesRepository.EditRolesUser(user.UserId, SelectedRoles);
                    _context.SaveChangesDB();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.UserRepository.IsExistUserName(user.UserName))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Redirect("/Admin/Users/Index?Edit=true");
            }
            ViewData["Roles"] = _context.RoleRepository.GetRoles();

            return View(user);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            EditUserViewModel user = _context.UserRepository.GetUserForShowInEditMode((int)id);
            ViewData["Roles"] = _context.RoleRepository.GetRoles();

            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

    }
}
