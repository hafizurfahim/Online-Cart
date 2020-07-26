using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Online_Cart.Models
{
    public class ProductType
    {
        public int id { get; set; }
        [Required]
        [Display(Name = " product Type")]
        public string productType { set; get; }
    }
}
