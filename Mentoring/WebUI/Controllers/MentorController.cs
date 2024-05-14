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
    public class MentorController : Controller
    {
        private UserManager<ProjeUser> _userManager;
        private SignInManager<ProjeUser> _signInManager;
        private RoleManager<ProjeRole> _roleManager;
        public MentorController(UserManager<ProjeUser> userManager, SignInManager<ProjeUser> signInManager, RoleManager<ProjeRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        MentorManager _mentorService = new MentorManager(new MentorDal());
        DepartmentManager _departmentService = new DepartmentManager(new DepartmentDal());
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var mentor = _mentorService.GetAll().Where(i => i.MentorId == user.MentorId).FirstOrDefault();
            if (mentor.Id == 0)
            {
                mentor.Id = user.Id;
                _mentorService.Update(mentor);
            }

            var mentorEntity = _mentorService.GetMentorAndSocialAndStajByUserId(user.Id);
            if (!mentorEntity.IsOnline)
            {
                mentorEntity.IsOnline = true;
                _mentorService.Update(mentorEntity);
            }
            
            var model = new MentorIndexModel()
            {
                User = user,
                Mentor = mentorEntity
            };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> MentorEdit()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var mentorEntity = _mentorService.GetAll().Where(i => i.Id == user.Id).FirstOrDefault();
            var model = new MentorEditModel()
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Gender = user.Gender,
                MentorId = mentorEntity.MentorId,
                GraduationYear = mentorEntity.GraduationYear,
                Description = mentorEntity.Description,
                Department = mentorEntity.Department,
                Image = mentorEntity.Image,
                Email = mentorEntity.Email,
                Phone = mentorEntity.Phone,
                Address = mentorEntity.Address,
                Departments = _departmentService.GetAll().OrderBy(i => i.DepartmentRowNumber).ToList()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> MentorEdit(MentorEditModel model, IFormFile file)
        {   
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                if (user != null && user.Id == model.Id)
                {
                    user.Name = model.Name;
                    user.Surname = model.Surname;
                    user.Gender = model.Gender;

                    var sonuc = await _userManager.UpdateAsync(user);
                    if (sonuc.Succeeded)
                    {
                        if (file != null)
                        {
                            var dosyaYoluSil = Path.Combine("wwwroot/img/" + model.Image);
                            if (System.IO.File.Exists(dosyaYoluSil))
                            {
                                System.IO.File.Delete(dosyaYoluSil);
                            }

                            model.Image = file.FileName;
                            var dosyaYolu = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", model.Image);

                            var sayi = 1;
                            while (System.IO.File.Exists(dosyaYolu))
                            {
                                model.Image = sayi + "-" + file.FileName;
                                dosyaYolu = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", model.Image);
                                sayi++;
                            }

                            var stream = new FileStream(dosyaYolu, FileMode.Create);
                            file.CopyTo(stream);
                            stream.Flush();
                            stream.Close();
                        }

                        var mentor = _mentorService.GetAll().Where(i => i.Id == user.Id).FirstOrDefault();

                        mentor.Image = model.Image;
                        mentor.Email = model.Email;
                        mentor.Phone = model.Phone;
                        mentor.Address = model.Address;
                        mentor.GraduationYear = model.GraduationYear;
                        mentor.Description = model.Description;
                        mentor.Department = model.Department;

                        _mentorService.Update(mentor);

                        TempData["icon"] = "success";
                        TempData["title"] = "Mentor güncellendi.";
                        return RedirectToAction("Index", "Mentor");
                    }
                }
            }
            model.Departments = _departmentService.GetAll().OrderBy(i => i.DepartmentRowNumber).ToList();
            return View(model);
        }

    }
}