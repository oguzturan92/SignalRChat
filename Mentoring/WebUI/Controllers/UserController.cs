using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Concrete;
using Data.Concrete.EfCore;
using Entity.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class UserController : Controller
    {
        private UserManager<ProjeUser> _userManager;
        private SignInManager<ProjeUser> _signInManager;
        public UserController(UserManager<ProjeUser> userManager, SignInManager<ProjeUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        MentorManager _mentorService = new MentorManager(new MentorDal());
        
        [HttpGet]
        public IActionResult UserRegister()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> UserRegister(UserRegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var emailSorgula = await _userManager.FindByEmailAsync(model.Email.ToLower());
                if (emailSorgula != null)
                {
                    ModelState.AddModelError("Email", "Bu mail adresi başka kullanıcıya kayıtlı.");
                    return View(model);
                }

                var user = new ProjeUser()
                {
                    Name = model.Name,
                    Surname = model.Surname,
                    UserName = model.Email.Trim().ToLower(),
                    Email = model.Email.Trim().ToLower(),
                    EmailConfirmed = true,
                    RegisterDate = DateTime.Now
                };
                if (model.RoleName == "Mentor")
                {
                    user.Mentor = new Mentor(){GraduationYear=new DateTime(2000,01,01)};
                }
                if (model.RoleName == "Student")
                {
                    user.Student = new Student(){StartingYear=new DateTime(2000,01,01)};
                }
                
                var userSonuc = await _userManager.CreateAsync(user, model.Password);

                var roleSonuc = await _userManager.AddToRoleAsync(user, model.RoleName);

                if (userSonuc.Succeeded && roleSonuc.Succeeded)
                {
                    TempData["icon"] = "success";
                    TempData["title"] = "Kullanıcı oluşturuldu. Giriş yapabilirsiniz.";
                    return RedirectToAction("UserLogin", "User");
                } else
                {
                    TempData["iconOK"] = "warning";
                    TempData["iconText"] = "Bir sorun oluştu. Lütfen tekrar deneyin";
                    return View(model);
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult UserLogin(string ReturnUrl = null)
        {
            var model = new UserLoginModel(){
                ReturnUrl = ReturnUrl
            };
            return View(model);
        }
        
        [HttpPost]
        public async Task<IActionResult> UserLogin(UserLoginModel model)
        {
            if (ModelState.IsValid)
            {
                var userSorgusu = await _userManager.FindByEmailAsync(model.Email);
                if (userSorgusu != null)
                {
                    var passwordSorgusu = await _signInManager.PasswordSignInAsync(
                        userSorgusu,
                        model.Password,
                        true,
                        false
                    );
                    
                    if (passwordSorgusu.Succeeded)
                    {
                        return Redirect(model.ReturnUrl ?? "~/Home/Index");
                    } else
                    {
                        ModelState.AddModelError("Email", "Email veya şifre hatalı.");
                    }
                } else
                {
                    ModelState.AddModelError("Email", "Email veya şifre hatalı.");
                }
            }
            return View(model);
        }

        public async Task<IActionResult> UserLogout()
        {   
            if (!string.IsNullOrEmpty(User.Identity.Name))
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                var mentorEntity = _mentorService.GetAll().Where(i => i.Id == user.Id).FirstOrDefault();
                if (mentorEntity != null)
                {
                    mentorEntity.IsOnline = false;
                    _mentorService.Update(mentorEntity);
                }
            }
            await _signInManager.SignOutAsync();

            return RedirectToAction("UserLogin", "User");
        }
    }
}