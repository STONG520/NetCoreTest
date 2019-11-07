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
    [Authorize(Roles = "管理员")]
    public class UserController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;

        public UserController(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }
        public async Task<IActionResult> UserList() {
            var users = await userManager.Users.ToListAsync();
            return View(users);
        }
        public IActionResult AddUser() {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddUser(UserCreateViewModel userCreateView) {
            if (!ModelState.IsValid) {
                return View(userCreateView);
            }
            IdentityUser user = new IdentityUser {
                 Email=userCreateView.userEmail,
                  PasswordHash=userCreateView.userPassword,
                   UserName=userCreateView.userName
            };
            var result =await userManager.CreateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("UserList");
            }
            else {
                ModelState.AddModelError(string.Empty,"添加失败");
            }
            return View(userCreateView);
        }
        public async Task<IActionResult> DeleteUser(string id) {
            var user = await userManager.FindByIdAsync(id);
            if (user==null) {
                ModelState.AddModelError(string.Empty,"用户不存在");
            }
            var result =await userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("UserList");
            }
            else {
                ModelState.AddModelError(string.Empty, "删除失败");
            }
               return View("UserList",userManager.Users.ToListAsync());
        }
        public async Task<IActionResult> UpdateUser(string id) {
            var user = await userManager.FindByIdAsync(id);
            UserCreateViewModel userCreate = new UserCreateViewModel {
                 id=user.Id,
                  userEmail=user.Email,
                   userName=user.UserName,
                    userPassword=user.PasswordHash
            };
            return View(userCreate);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateUser(UserCreateViewModel userCreateView) {
            if (!ModelState.IsValid) {
                return View(userCreateView);
            }
            var user = await userManager.FindByIdAsync(userCreateView.id);
            user.Email = userCreateView.userEmail;
            user.PasswordHash = userCreateView.userPassword;
            user.UserName = userCreateView.userName;
            user.Id = userCreateView.id;
            var result =await userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("UserList");
            }
            else {
                return View(userCreateView);
            }
        }
    }
}