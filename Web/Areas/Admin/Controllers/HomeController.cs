using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Areas.Admin.Models;
using Web.Models;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin, God")]
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            List<ShortUserInfoViewModel> list = _userManager.Users
                .Select(u => new ShortUserInfoViewModel
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    UserName = u.UserName,
                    Age = u.Age
                }).ToList();

            return View(list);
        }

        public async Task<IActionResult> Edit(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            ShortUserInfoViewModel userInfo = new ShortUserInfoViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                UserName = user.UserName,
                Age = user.Age
            };

            return View(userInfo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ShortUserInfoViewModel userInfo)
        {
            var user = await _userManager.FindByNameAsync(userInfo.UserName);

            user.FirstName = userInfo.FirstName;
            user.LastName = userInfo.LastName;
            user.Email = userInfo.Email;
            user.UserName = userInfo.UserName;
            user.Age = userInfo.Age;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(userInfo);
        }

        public IActionResult Delete(string userName)
        {
            return View();
        }
    }
}