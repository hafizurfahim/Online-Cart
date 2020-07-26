using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Online_Cart.Data;
using Online_Cart.Models;
using Online_Cart.Utility;

namespace Online_Cart.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class OrderController : Controller
    {
        private ApplicationDbContext _db;
        public OrderController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Checkout()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(Order ordercheckout)
        {
            List<Products> products = HttpContext.Session.Get<List<Products>>("products");
            if (products != null)
            {
                foreach (var product in products)
                {
                    OrderDetails orderDetails = new OrderDetails();
                    orderDetails.ProductId = product.Id;
                    
                    ordercheckout.orderDetails.Add(orderDetails);

                }
            }
            ordercheckout.OrderNo = GetOrderNo();

            _db.order.Add(ordercheckout);
            await _db.SaveChangesAsync();
            HttpContext.Session.Set("products", new List<Products>());
            return View();

        }
        public string GetOrderNo()
        {
            int rowCount = _db.order.ToList().Count() + 1;
            return rowCount.ToString("#20200malu");


        }
    }
}