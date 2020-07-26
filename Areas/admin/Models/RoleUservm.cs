using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Online_Cart.Areas.admin.Models
{
    public class RoleUservm
   {
        [Required]
        [DisplayName("Role")]
        public string RoleId { set; get; }
        [DisplayName("User")]
        public string UserId { set; get; }
    }
}
