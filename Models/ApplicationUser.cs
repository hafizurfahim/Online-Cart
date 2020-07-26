using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Online_Cart.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string Firstname { set; get; }
        public string Lastname { set; get; }
    }
}
