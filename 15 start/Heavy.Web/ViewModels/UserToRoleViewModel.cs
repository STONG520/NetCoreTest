using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Heavy.Web.ViewModels
{
    public class UserToRoleViewModel
    {
        public UserToRoleViewModel()
        {
            userNames = new List<string>();
        }

        public string roleId { get; set; }
        [Required]
        [Display(Name = "角色名")]
        public string roleName { get; set; }
        public List<string> userNames { get; set; }
    }
}
