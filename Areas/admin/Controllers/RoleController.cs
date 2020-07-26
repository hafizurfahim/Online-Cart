using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Online_Cart.Areas.admin.Models;
using Online_Cart.Data;

namespace Online_Cart.Areas.admin.Controllers
{
    [Area("admin")]
    public class RoleController : Controller
    {
        RoleManager<IdentityRole> _roleManager;
       
        UserManager<IdentityUser> _userManager;
        ApplicationDbContext _db;
        public RoleController(RoleManager<IdentityRole> roleManager,ApplicationDbContext db, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        [Authorize]
        public IActionResult Index()
        {
            var roles = _roleManager.Roles.ToList();
            ViewBag.role = roles;
            return View();
        }
        public async Task<IActionResult> Create()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create( string name)
        {
            IdentityRole role =new IdentityRole();
            role.Name= name;
            var info = await _roleManager.CreateAsync(role);
            var exist = await _roleManager.RoleExistsAsync(role.Name);
            ViewBag.name = name;
            if (exist)
            {
                ViewBag.message = "This role already exist";
            }
            if (info.Succeeded)
            {
                TempData["save"] = "Save data succesfully !!!!!";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public async Task<IActionResult> Edit(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
           // ViewBag.id = role.Id;
            ViewBag.name = role.Name;

            return View(role);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(string name,string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            role.Name = name;
           
           
            var exist = await _roleManager.RoleExistsAsync(role.Name);
           
            if (exist)
            {
                ViewBag.message = "This role already exist"; 
                ViewBag.name = name;
                return View();
            }
            var info = await _roleManager.UpdateAsync(role);
            if (info.Succeeded)
            {
                TempData["save"] = "data updated succesfully !!!!!";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        
        public async Task<IActionResult> Delete(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            // ViewBag.id = role.Id;
            ViewBag.name = role.Name;

            return View(role);
        }


        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> Deleteconfirm( string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }

           
            var info = await _roleManager.DeleteAsync(role);
            if (info.Succeeded)
            {
                TempData["save"] = "data deleted succesfully !!!!!";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public async Task<IActionResult> Assign()
        {

            ViewData ["Userid"]=new SelectList(_db.ApplicationUsers.Where(c=>c.LockoutEnd<DateTime.Now || c.LockoutEnd==null ).ToList(),"Id","UserName");
            ViewData["roleid"] = new SelectList(_roleManager.Roles.ToList(), "Name","Name");
            return View();        
                
                
                }
        [HttpPost]
        public async Task<IActionResult> Assign(RoleUservm roleUser)
        {
           

            var user = _db.ApplicationUsers.FirstOrDefault(c => c.Id == roleUser.UserId);
            var isassigned = await _userManager.IsInRoleAsync(user, roleUser.RoleId);
            if (isassigned)
            {
                ViewBag.msg = "This user's role assigned already.";
                ViewData["Userid"] = new SelectList(_db.ApplicationUsers.Where(c => c.LockoutEnd < DateTime.Now || c.LockoutEnd == null).ToList(), "Id", "UserName");
                ViewData["roleid"] = new SelectList(_roleManager.Roles.ToList(), "Name", "Name");
                return View();
            }
            var role = await _userManager.AddToRoleAsync(user, roleUser.RoleId);
            if (role.Succeeded)
            {
                TempData["save"] = "User Role assigned.";
                return RedirectToAction(nameof(Index));
            }
            

            return View();

        }
        public IActionResult RoleAssignUser()
        {
            var result = from ur in _db.UserRoles
                         join r in _db.Roles on ur.RoleId equals r.Id
                         join a in _db.ApplicationUsers on ur.UserId equals a.Id
                         select new UserRoleMap()
                         {
                             UserId = ur.UserId, 
                             UserName=a.UserName,
                             RoleId=ur.RoleId,
                             RoleName=r.Name


                         };
            ViewBag.info = result;
            return View();
        }


      
    }
}














