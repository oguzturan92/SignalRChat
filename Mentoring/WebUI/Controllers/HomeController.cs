using System.Diagnostics;
using Entity.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers;

[Authorize]
public class HomeController : Controller
{
    private UserManager<ProjeUser> _userManager;
    private SignInManager<ProjeUser> _signInManager;
    private RoleManager<ProjeRole> _roleManager;
    public HomeController(UserManager<ProjeUser> userManager, SignInManager<ProjeUser> signInManager, RoleManager<ProjeRole> roleManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
    }

    public async Task<IActionResult> Index()
    {
        var user = await _userManager.FindByNameAsync(User.Identity.Name);
        if (user != null)
        {
            var role = await _userManager.GetRolesAsync(user);
            if (role.FirstOrDefault() == "Student")
            {
                return RedirectToAction("Index", "Student");
            } else
            {
                return RedirectToAction("Index", "Mentor");
            }
        }
        return RedirectToAction("UserLogin", "User");
    }
}
