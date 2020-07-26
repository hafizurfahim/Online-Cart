using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Online_Cart.Models
{
    public class OrderDetails
    {
        public int id { get; set; }
       [Display(Name ="Order")]
        public int OrderId { get; set; }
        [Display(Name = "Product")]
        public int ProductId { get; set; }
        [ForeignKey("OrderId")]
        public Order order { get; set; }
        [ForeignKey("ProductId")]
        public Products product { get; set; }
    }
}
