using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
                    BirthDate = u.BirthDate
                }).ToList();

            return View(list);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            ShortUserInfoViewModel userInfo = new ShortUserInfoViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                UserName = user.UserName,
                BirthDate = user.BirthDate
            };

            return View(userInfo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ShortUserInfoViewModel userInfo)
        {
            if (!ModelState.IsValid)
            {
                return View(userInfo);
            }

            var user = await _userManager.GetUserAsync(User);

            user.FirstName = userInfo.FirstName;
            user.LastName = userInfo.LastName;
            user.Email = userInfo.Email;
            user.BirthDate = userInfo.BirthDate;

            var result = await _userManager.UpdateAsync(user);
            
            if (result.Succeeded)
            {
                await _userManager.RemoveClaimAsync(user, User.FindFirst(ClaimTypes.DateOfBirth));
                await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.DateOfBirth, user.BirthDate.Year.ToString()));

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

        [HttpDelete()]
        public async Task<bool> Delete(string id)
        {
            ApplicationUser user = await _userManager.FindByNameAsync(id);

            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return true;
                }
            }

            return false;
        }
    }
}