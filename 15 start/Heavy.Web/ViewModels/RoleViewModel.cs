using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Heavy.Web.ViewModels
{
    public class RoleViewModel
    {
        [Required]
        [Display(Name = "角色名")]
        public string roleName { get; set; }
        public string id { get; set; }
    }
}
