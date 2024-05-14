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
    public class SocialMediaController : Controller
    {
        private UserManager<ProjeUser> _userManager;
        private SignInManager<ProjeUser> _signInManager;
        private RoleManager<ProjeRole> _roleManager;
        public SocialMediaController(UserManager<ProjeUser> userManager, SignInManager<ProjeUser> signInManager, RoleManager<ProjeRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        SocialMediaManager _socialMediaService = new SocialMediaManager(new SocialMediaDal());
        MentorManager _mentorService = new MentorManager(new MentorDal());

        public async Task<IActionResult> SocialMediaList()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var mentorEntity = _mentorService.GetMentorAndSocialAndStajByUserId(user.Id);
            return View(mentorEntity);
        }

        [HttpGet]
        public IActionResult SocialMediaCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SocialMediaCreate(SocialMediaCreateModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                var mentorId = _mentorService.GetAll().Where(i => i.Id == user.Id).FirstOrDefault().Id;
                var socialMedia = new SocialMedia()
                {
                    SocialMediaTitle = model.SocialMediaTitle,
                    SocialMediaRowNumber = model.SocialMediaRowNumber,
                    SocialMediaIcon = model.SocialMediaIcon,
                    SocialMediaLink = model.SocialMediaLink,
                    MentorId = mentorId
                };
                _socialMediaService.Create(socialMedia);
                TempData["icon"] = "success";
                TempData["title"] = "Sosyal Medya güncellendi.";
            }
            return RedirectToAction("SocialMediaList", "SocialMedia");
        }

        [HttpGet]
        public IActionResult SocialMediaEdit(int id)
        {
            var socialMedia = _socialMediaService.GetById(id);
            if (socialMedia != null)
            {
                var model = new SocialMediaEditModel()
                {
                    SocialMediaId = socialMedia.SocialMediaId,
                    SocialMediaTitle = socialMedia.SocialMediaTitle,
                    SocialMediaRowNumber = socialMedia.SocialMediaRowNumber,
                    SocialMediaIcon = socialMedia.SocialMediaIcon,
                    SocialMediaLink = socialMedia.SocialMediaLink,
                    MentorId = socialMedia.MentorId
                };
                return View(model);
            }
            TempData["iconOK"] = "error";
            TempData["iconText"] = "Bir sorun oluştu. Lütfen tekrar deneyin.";
            return RedirectToAction("SocialMediaList", "SocialMedia");
        }

        [HttpPost]
        public async Task<IActionResult> SocialMediaEdit(SocialMediaEditModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                var mentorId = _mentorService.GetAll().Where(i => i.Id == user.Id).FirstOrDefault().Id;
                if (mentorId == model.MentorId)
                {
                    var socialMedia = new SocialMedia()
                    {
                        SocialMediaId = model.SocialMediaId,
                        SocialMediaTitle = model.SocialMediaTitle,
                        SocialMediaRowNumber = model.SocialMediaRowNumber,
                        SocialMediaIcon = model.SocialMediaIcon,
                        SocialMediaLink = model.SocialMediaLink,
                        MentorId = model.MentorId
                    };
                    _socialMediaService.Update(socialMedia);
                    TempData["icon"] = "success";
                    TempData["title"] = "Sosyal Medya güncellendi.";
                    return RedirectToAction("SocialMediaList", "SocialMedia");
                }
            }
            return View(model);
        }

        public async Task<IActionResult> SocialMediaDelete(int id)
        {
            var socialMedia = _socialMediaService.GetById(id);
            if (socialMedia != null)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                var mentorId = _mentorService.GetAll().Where(i => i.Id == user.Id).FirstOrDefault().Id;
                if (mentorId == socialMedia.MentorId)
                {
                    _socialMediaService.Delete(socialMedia);
                    TempData["icon"] = "success";
                    TempData["title"] = "Sosyal Medya silindi.";
                    return RedirectToAction("SocialMediaList", "SocialMedia");
                }
            }
            TempData["iconOK"] = "error";
            TempData["iconText"] = "Bir sorun oluştu. Lütfen tekrar deneyin.";
            return RedirectToAction("SocialMediaList", "SocialMedia");
        }
    
    }
}