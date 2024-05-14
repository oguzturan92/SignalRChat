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
    [Authorize(Roles = "Student")]
    public class StudentController : Controller
    {
        private UserManager<ProjeUser> _userManager;
        private SignInManager<ProjeUser> _signInManager;
        private RoleManager<ProjeRole> _roleManager;
        public StudentController(UserManager<ProjeUser> userManager, SignInManager<ProjeUser> signInManager, RoleManager<ProjeRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        StudentManager _studentService = new StudentManager(new StudentDal());
        DepartmentManager _departmentService = new DepartmentManager(new DepartmentDal());
        MentorManager _mentorService = new MentorManager(new MentorDal());
        PointManager _pointService = new PointManager(new PointDal());
        public async Task<IActionResult> Index(string department)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var student = _studentService.GetAll().Where(i => i.StudentId == user.StudentId).FirstOrDefault();
            if (student.Id == 0)
            {
                student.Id = user.Id;
                _studentService.Update(student);
            }

            var mentors = _mentorService.GetMentorsAndUsersAndSocial();  
            
            if (!string.IsNullOrEmpty(department))
            {
                mentors = _mentorService.GetMentorsAndUsersAndSocial().Where(i => i.Department.ToLower() == department.ToLower()).ToList();
            }

            var model = new StudentIndexModel()
            {
                Mentors = mentors,
                Departments = _departmentService.GetAll().OrderBy(i => i.DepartmentRowNumber).ToList()
            };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> MentorDetail(int userId)
        {
            var mentorEntity = _mentorService.GetMentorAndSocialAndStajByUserId(userId);
            var points = _pointService.GetAll().Where(i => i.MentorId == mentorEntity.MentorId).ToList();
            var point = 0;
            if (points.Count() > 0)
            {
                point = (int)points.Average(i => i.PointNo);
            }
            var model = new MentorDetailModel()
            {
                User = await _userManager.FindByIdAsync(userId.ToString()),
                Mentor = mentorEntity,
                Point = (decimal)point
            };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> StudentEdit()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var student = _studentService.GetAll().Where(i => i.Id == user.Id).FirstOrDefault();
            var model = new StudentEditModel()
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                StudentImage = student.StudentImage,
                Gender = user.Gender,
                StartingYear = student.StartingYear
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> StudentEdit(StudentEditModel model, IFormFile file)
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
                            var dosyaYoluSil = Path.Combine("wwwroot/img/" + model.StudentImage);
                            if (System.IO.File.Exists(dosyaYoluSil))
                            {
                                System.IO.File.Delete(dosyaYoluSil);
                            }

                            model.StudentImage = file.FileName;
                            var dosyaYolu = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", model.StudentImage);

                            var sayi = 1;
                            while (System.IO.File.Exists(dosyaYolu))
                            {
                                model.StudentImage = sayi + "-" + file.FileName;
                                dosyaYolu = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", model.StudentImage);
                                sayi++;
                            }

                            var stream = new FileStream(dosyaYolu, FileMode.Create);
                            file.CopyTo(stream);
                            stream.Flush();
                            stream.Close();
                        }

                        var student = _studentService.GetAll().Where(i => i.Id == user.Id).FirstOrDefault();

                        student.StartingYear = model.StartingYear;
                        student.StudentImage = model.StudentImage;

                        _studentService.Update(student);

                        TempData["icon"] = "success";
                        TempData["title"] = "Öğrenci güncellendi.";
                        return RedirectToAction("StudentEdit", "Student");
                    }
                }
            }
            return View(model);
        }

    }
}