using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Heavy.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Heavy.Web.Controllers
{
    [Authorize(Roles ="管理员")]
    public class RoleController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public RoleController(UserManager<IdentityUser> userManager,RoleManager<IdentityRole> roleManager) {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<IActionResult> RoleList() {
            var roles = await roleManager.Roles.ToListAsync();
            return View(roles);
            
        }
        public IActionResult AddRole() {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddRole(RoleViewModel roleView) {
            if (!ModelState.IsValid) {
                return View(roleView);
            }
            var identityRole = new IdentityRole
            {
                Name = roleView.roleName
            };
           var result=await roleManager.CreateAsync(identityRole);
            if (result.Succeeded)
            {
                return RedirectToAction("RoleList");
            }
            else {
                return View(roleView);
            }
        }
        public async Task<IActionResult> UpdateRole(string id) {
            var role =await roleManager.FindByIdAsync(id);
            if (role==null) {
                return RedirectToAction("RoleList");
            }
            UserToRoleViewModel userToRoleView = new UserToRoleViewModel {
                 roleId=role.Id,
                  roleName=role.Name
            };
            var users =await userManager.Users.ToListAsync();
            foreach (IdentityUser user in users) {
                if (await userManager.IsInRoleAsync(user,role.Name)) {
                    userToRoleView.userNames.Add(user.UserName);
                }
            }
            return View(userToRoleView);
        }
        public async Task<IActionResult> AddUserToRole(string roleId) {
            var role = await roleManager.FindByIdAsync(roleId);
            if (role!=null) {
                AddUserToRoleViewModel addUserToRole = new AddUserToRoleViewModel {
                     roleId=role.Id
                };
                var users = await userManager.Users.ToListAsync();
                foreach (IdentityUser user in users)
                {
                    if (!await userManager.IsInRoleAsync(user, role.Name))
                    {
                        addUserToRole.identityUsers.Add(user);
                    }
                }
                return View(addUserToRole);
            }
            return RedirectToAction("RoleList");
        }
        [HttpPost]
        public async Task<IActionResult> AddUserToRole(AddUserToRoleViewModel addUserTo)
        {
            var role = await roleManager.FindByIdAsync(addUserTo.roleId);
            var user = await userManager.FindByIdAsync(addUserTo.userId);
            if (role != null && user != null)
            {
                if (!await userManager.IsInRoleAsync(user, role.Name))
                {
                    var result = await userManager.AddToRoleAsync(user, role.Name);
                    if (result.Succeeded)
                    {

                    }
                }
            }
            return RedirectToAction("RoleList");
        }
    }
}