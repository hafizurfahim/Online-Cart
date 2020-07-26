using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Online_Cart.Data;
using Online_Cart.Models;
using Online_Cart.Utility;

namespace Online_Cart.Areas.admin.Controllers
{
    [Area("admin")]
    [Authorize]
    

    public class ProductTypesController : Controller
    {
      private ApplicationDbContext _db;
        public ProductTypesController(ApplicationDbContext db)
        {
            _db = db;
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View(_db.productTypes.ToList());
        }

        //Create get action 
       
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Create(ProductType Product)
        {
            if (ModelState.IsValid)
            {
                _db.productTypes.Add(Product);
                await _db.SaveChangesAsync();
                TempData["save"] = ("Save data succesfully");
                return RedirectToAction(nameof(Index));

                
            }
            return View (Product);
        }

        //Create get action 
        public IActionResult Edit(int?id)
        {
            if(id==null)
            {
                return NotFound();
            }
            var product = _db.productTypes.Find(id);
            if(product==null)
            {
                return NotFound();
            }

            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductType Product)
        {
            if (ModelState.IsValid)
            {
                _db.Update(Product);
                await _db.SaveChangesAsync();
                TempData["Edit"] = ("Editted data succesfully");
                return RedirectToAction(nameof(Index));


            }
            return View(Product);
        }
        //Create get action 
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = _db.productTypes.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(ProductType Product)
        {
            
                return RedirectToAction(nameof(Index));
           
        }
        //Create get action 
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = _db.productTypes.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int?id, ProductType Product)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (id != Product.id)
            {
                return NotFound();
            }
            var product = _db.productTypes.Find(id);
            if(product==null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _db.Remove(product);
                await _db.SaveChangesAsync();
                TempData["Delete"] = ("Deleted data succesfully");
                return RedirectToAction(nameof(Index));


            }
            return View(Product);

        }
       
    }
}