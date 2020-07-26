using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;

using Online_Cart.Data;
using Online_Cart.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Online_Cart.Areas.admin.Controllers
{
    [Area("admin")]
    public class ProductController : Controller
    {
        private ApplicationDbContext _db;
        [Obsolete]
        private IHostingEnvironment _he;

        [Obsolete]
        public ProductController(ApplicationDbContext db, IHostingEnvironment he)
        {
            _db = db;
            _he = he;
        }

        public IActionResult Index()
        {
            return View(_db.Products.Include(c => c.ProductType).Include(c => c.SpecialTag).ToList());
          
        }
        [HttpPost]
       public IActionResult Index(decimal? lowAmount, decimal? largeAmount)
      {
           var product = _db.Products.Include(c => c.ProductType).Include(c => c.SpecialTag)
                .Where(c => c.Price >= lowAmount && c.Price <= largeAmount).ToList();
            if (lowAmount == null || largeAmount == null)
            {
                product = _db.Products.Include(c => c.ProductType).Include(c => c.SpecialTag).ToList();
            }
            return View(product);
        }
        
        

        //get view
        public IActionResult Create()
        {
           
            ViewData["productId"] = new SelectList(_db.productTypes.ToList(), "id", "productType");
            ViewData["TagId"] = new SelectList(_db.SpecialTags.ToList(), "Id", "Name");

            return View();
        }
        [HttpPost]
        [Obsolete]
        public async Task<IActionResult> Create(Products product, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                var searchproduct = _db.Products.FirstOrDefault(c => c.Name == product.Name);
                if (searchproduct != null)
                {
                    ViewBag.msg = "This product already exist !!";
                    ViewData["productId"] = new SelectList(_db.productTypes.ToList(), "id", "productType");
                    ViewData["TagId"] = new SelectList(_db.SpecialTags.ToList(), "Id", "Name");
                    return View(product);
                }

                if (image != null)
                {
                    var name = Path.Combine(_he.WebRootPath + "/Images", Path.GetFileName(image.FileName));
                    await image.CopyToAsync(new FileStream(name, FileMode.Create));
                    product.Image = "Images/" + image.FileName;
                }

                if (image == null)
                {
                    product.Image = "Images/noimage.PNG";
                }
                _db.Products.Add(product);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));



            }
            return View(product);
        }
        public ActionResult Edit(int? id)
        {

            ViewData["productId"] = new SelectList(_db.productTypes.ToList(), "id", "productType");
            ViewData["TagId"] = new SelectList(_db.SpecialTags.ToList(), "Id", "Name");
            if (id == null)
            {
                return NotFound();
            }
            var product = _db.Products.Include(c => c.ProductType).Include(c => c.SpecialTag).FirstOrDefault(c => c.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);

        }
        [HttpPost]
        public async Task<IActionResult> Edit(Products product, IFormFile image)
        {
            if (ModelState.IsValid)
            {
               

                if (image != null)
                {
                    var name = Path.Combine(_he.WebRootPath + "/Images", Path.GetFileName(image.FileName));
                    await image.CopyToAsync(new FileStream(name, FileMode.Create));
                    product.Image = "Images/" + image.FileName;
                }

                if (image == null)
                {
                    product.Image = "Images/noimage.PNG";
                }
                _db.Products.Update(product);

                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));



            }
            return View(product);
        }
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = _db.Products.Include(c => c.ProductType).Include(c => c.SpecialTag).FirstOrDefault(c => c.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();

            }
            var product = _db.Products.Include(c => c.ProductType).Include(c => c.SpecialTag).Where(c => c.Id == id).FirstOrDefault();
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = _db.Products.FirstOrDefault(c => c.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            _db.Products.Remove(product);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



    }
        
}