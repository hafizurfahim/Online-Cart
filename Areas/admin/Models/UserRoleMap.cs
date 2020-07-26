using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Online_Cart.Areas.admin.Models
{
    public class UserRoleMap
    {
        public string RoleId { set; get; }
       
        public string UserId { set; get; }
        public string RoleName { set; get; }

        public string UserName { set; get; }
    }
}
