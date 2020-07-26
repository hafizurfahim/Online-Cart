using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Online_Cart.Data;
using Online_Cart.Models;

namespace Online_Cart.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class UserController : Controller
    {
        UserManager<IdentityUser> _userManager;
        RoleManager<IdentityRole> _roleManager;
        ApplicationDbContext _db;
        public UserController(UserManager<IdentityUser> userManager, ApplicationDbContext db,RoleManager<IdentityRole>roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _db = db;
        }
        public IActionResult Index()
        {
            return View(_db.ApplicationUsers.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ApplicationUser applicationUser)
        {
            var result = await _userManager.CreateAsync(applicationUser, applicationUser.PasswordHash);
            if (result.Succeeded)
            {
                


                    var isSaved = await _userManager.AddToRoleAsync(applicationUser, "User");
                
                TempData["save"] = "Save data succesfully !!!!!";
                return RedirectToAction(nameof(Index));
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View();
        }

        public async Task<IActionResult> Edit(string id)
        {
            var info = _db.ApplicationUsers.FirstOrDefault(c => c.Id == id);
            if (info == null)
            {
                return NotFound();
            }
            return View(info);

        }
        [HttpPost]
        public async Task<IActionResult> Edit(ApplicationUser user)
        {
            var info = _db.ApplicationUsers.FirstOrDefault(c => c.Id == user.Id);
            if (info == null)
            {
                return NotFound();
            }
            info.Firstname = user.Firstname;
            info.Lastname = user.Lastname;
            var result = await _userManager.UpdateAsync(info);
            if (result.Succeeded)
            {
                TempData["save"] = "Updated data succesfully";
                return RedirectToAction(nameof(Index));
            }

            return View(info);

        }


        public async Task<IActionResult> Details(string id)
        {
            var info = _db.ApplicationUsers.FirstOrDefault(c => c.Id == id);
            if (info == null)
            {
                return NotFound();
            }
            return View(info);

        }

        public async Task<IActionResult> Lockout(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = _db.ApplicationUsers.FirstOrDefault(c => c.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);

        }
        [HttpPost]
        public async Task<IActionResult> Lockout(ApplicationUser applicationUser)
        {
            var UserInfo = _db.ApplicationUsers.FirstOrDefault(c => c.Id == applicationUser.Id);
            if (UserInfo == null)
            {
                return NotFound();
            }
            UserInfo.LockoutEnd = DateTime.Now.AddYears(10);
           var roweffect= _db.SaveChanges();
           
           if(roweffect>0)
            {
                TempData["save"] = "User locked Successfully !!! ";
                return RedirectToAction(nameof(Index));
            }
            return View(UserInfo);

        }
        public async Task<IActionResult> Active(string id)
        {
            var info = _db.ApplicationUsers.FirstOrDefault(c => c.Id == id);
            if (info == null)
            {
                return NotFound();
            }
            return View(info);
        }

        [HttpPost]
        public async Task<IActionResult> Active(ApplicationUser applicationUser)
        {
            var UserInfo = _db.ApplicationUsers.FirstOrDefault(c => c.Id == applicationUser.Id);
            if (UserInfo == null)
            {
                return NotFound();
            }
            UserInfo.LockoutEnd = DateTime.Now.AddDays(-1);
            var roweffect = _db.SaveChanges();

            if (roweffect > 0)
            {
                TempData["save"] = "Active user Successfully !!! ";
                return RedirectToAction(nameof(Index));
            }
            return View(UserInfo);

        }

        public async Task<IActionResult> Delete(string id)
        {
            var info = _db.ApplicationUsers.FirstOrDefault(c => c.Id == id);
            if (info == null)
            {
                return NotFound();
            }
            return View(info);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(ApplicationUser applicationUser)
        {
            var UserInfo = _db.ApplicationUsers.FirstOrDefault(c => c.Id == applicationUser.Id);
            if (UserInfo == null)
            {
                return NotFound();
            }
            _db.ApplicationUsers.Remove(UserInfo);
            var roweffect = _db.SaveChanges();

            if (roweffect > 0)
            {
                TempData["save"] = "Delete user Successfully !!! ";
                return RedirectToAction(nameof(Index));
            }
            return View(UserInfo);

        }
    }
}