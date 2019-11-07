using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Heavy.Web.ViewModels
{
    public class UserCreateViewModel
    {
        [Required]
        [Display(Name ="用户名")]
        public string userName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage ="邮箱格式错误")]
        [Display(Name = "邮箱")]
        public string userEmail { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string userPassword { get; set; }
        public string id { get; set; }
    }
}
