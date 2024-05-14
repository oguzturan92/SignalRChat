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
    [Authorize(Roles = "Mentor")]
    public class StajController : Controller
    {
        private UserManager<ProjeUser> _userManager;
        private SignInManager<ProjeUser> _signInManager;
        private RoleManager<ProjeRole> _roleManager;
        public StajController(UserManager<ProjeUser> userManager, SignInManager<ProjeUser> signInManager, RoleManager<ProjeRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        StajManager _stajService = new StajManager(new StajDal());
        MentorManager _mentorService = new MentorManager(new MentorDal());

        public async Task<IActionResult> StajList()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var mentorEntity = _mentorService.GetMentorAndSocialAndStajByUserId(user.Id);
            return View(mentorEntity);
        }

        [HttpGet]
        public IActionResult StajCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> StajCreate(StajCreateModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                var mentorId = _mentorService.GetAll().Where(i => i.Id == user.Id).FirstOrDefault().Id;
                var staj = new Staj()
                {
                    StajTitle = model.StajTitle,
                    StajSubTitle = model.StajSubTitle,
                    StajRowNumber = model.StajRowNumber,
                    MentorId = mentorId
                };
                _stajService.Create(staj);
                TempData["icon"] = "success";
                TempData["title"] = "Staj eklendi.";
            }
            return RedirectToAction("StajList", "Staj");
        }

        [HttpGet]
        public IActionResult StajEdit(int id)
        {
            var staj = _stajService.GetById(id);
            if (staj != null)
            {
                var model = new StajEditModel()
                {
                    StajId = staj.StajId,
                    StajTitle = staj.StajTitle,
                    StajSubTitle = staj.StajSubTitle,
                    StajRowNumber = staj.StajRowNumber,
                    MentorId = staj.MentorId
                };
                return View(model);
            }
            TempData["iconOK"] = "error";
            TempData["iconText"] = "Bir sorun oluştu. Lütfen tekrar deneyin.";
            return RedirectToAction("StajList", "Staj");
        }

        [HttpPost]
        public async Task<IActionResult> StajEdit(StajEditModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                var mentorId = _mentorService.GetAll().Where(i => i.Id == user.Id).FirstOrDefault().Id;
                if (mentorId == model.MentorId)
                {
                    var staj = new Staj()
                    {
                        StajId = model.StajId,
                        StajTitle = model.StajTitle,
                        StajSubTitle = model.StajSubTitle,
                        StajRowNumber = model.StajRowNumber,
                        MentorId = model.MentorId
                    };
                    _stajService.Update(staj);
                    TempData["icon"] = "success";
                    TempData["title"] = "Sosyal Medya güncellendi.";
                    return RedirectToAction("StajList", "Staj");
                }
            }
            return View(model);
        }

        public async Task<IActionResult> StajDelete(int id)
        {
            var staj = _stajService.GetById(id);
            if (staj != null)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                var mentorId = _mentorService.GetAll().Where(i => i.Id == user.Id).FirstOrDefault().Id;
                if (mentorId == staj.MentorId)
                {
                    _stajService.Delete(staj);
                    TempData["icon"] = "success";
                    TempData["title"] = "Sosyal Medya silindi.";
                    return RedirectToAction("StajList", "Staj");
                }
            }
            TempData["iconOK"] = "error";
            TempData["iconText"] = "Bir sorun oluştu. Lütfen tekrar deneyin.";
            return RedirectToAction("StajList", "Staj");
        }
    
    }
}