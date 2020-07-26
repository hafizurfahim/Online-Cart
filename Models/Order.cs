using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Online_Cart.Models
{
    public class Order
    {
        public Order()
        {
            orderDetails = new List<OrderDetails>();
        }
        public int id { get; set; }
     [Required]
     [Display(Name ="Order No : ")]
        public string OrderNo { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Phone Number : ")]
        public int PhoneNo { get; set; }
        [Required]
        [EmailAddress]
        public string Email{ get; set; }
        [Required]
        public string Address { get; set; }
        public DateTime OrderDate { get; set; }
        public virtual List<OrderDetails> orderDetails { get; set; }

      
    }
}
