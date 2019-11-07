using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Heavy.Web.ViewModels
{
    public class AddUserToRoleViewModel
    {
        public AddUserToRoleViewModel()
        {
            identityUsers = new List<IdentityUser>();
        }

        public string roleId { get; set; }
        public string userId { get; set; }
        public List<IdentityUser> identityUsers { get; set; }
    }
}
