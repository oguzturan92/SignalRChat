using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entity.Concrete;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebUI.Models;

namespace WebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private UserManager<ProjeUser> _userManager;
        private SignInManager<ProjeUser> _signInManager;
        public AdminController(UserManager<ProjeUser> userManager, SignInManager<ProjeUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            var model = new AdminModel()
            {
                Users = _userManager.Users.OrderByDescending(i => i.RegisterDate).ToList()
            };
            return View(model);
        }

        public async Task<IActionResult> UserBlock(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user.IsBlock)
            {
                user.IsBlock = false;
            } else
            {
                user.IsBlock = true;
            }
            await _userManager.UpdateAsync(user);
            return RedirectToAction("Index", "Admin");
        }
        public async Task<IActionResult> UserDelete(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user != null)
            {
                var userName1 = User.Identity.Name;
                if (userName1.ToLower() == user.UserName.ToLower())
                {
                    TempData["iconOK"] = "error";
                    TempData["iconText"] = "Kendinizi silemezsiniz.";
                    return RedirectToAction("Index", "Admin");
                }

                var sonuc = await _userManager.DeleteAsync(user);
                if (sonuc.Succeeded)
                {
                    TempData["icon"] = "success";
                    TempData["title"] = "Kullanıcı başarıyla silindi.";
                    return RedirectToAction("Index", "Admin");
                }
            }
            TempData["iconOK"] = "warning";
            TempData["iconText"] = "Kullanıcı silme başarısız. Lütfen tekrar deneyiniz.";
            return RedirectToAction("Index", "Admin");
        }
    }
}