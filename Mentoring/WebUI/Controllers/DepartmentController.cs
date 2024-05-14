using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Concrete;
using Data.Concrete.EfCore;
using Entity.Concrete;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class DepartmentController : Controller
    {
        // DEPENCENCY INJECTION ------------------------------------------------
        DepartmentManager _departmentDepartment = new DepartmentManager(new DepartmentDal());

        // ADMIN METOTLARI ------------------------------------------------
        [HttpGet]
        public IActionResult DepartmentList()
        {
            var model = new DepartmentModel() {
                Departments = _departmentDepartment.GetAll().OrderBy(i => i.DepartmentRowNumber).ToList()
            };
            return View(model);
        }

        [HttpGet]
        public IActionResult DepartmentCreate()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DepartmentCreate(DepartmentModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = new Department() {
                    DepartmentId = model.DepartmentId,
                    DepartmentName = model.DepartmentName,
                    DepartmentRowNumber = model.DepartmentRowNumber
                };

                _departmentDepartment.Create(entity);

                TempData["icon"] = "success";
                TempData["title"] = "Sosyal Medya oluşturuldu.";
                return RedirectToAction("DepartmentList", "Department");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult DepartmentEdit(int id)
        {
            var department = _departmentDepartment.GetById(id);
            if (department != null)
            {
                var model = new DepartmentModel()
                {
                    DepartmentId = department.DepartmentId,
                    DepartmentName = department.DepartmentName,
                    DepartmentRowNumber = department.DepartmentRowNumber
                };
                return View(model);
            }
            TempData["iconOK"] = "error";
            TempData["iconText"] = "Bir sorun oluştu. Lütfen tekrar deneyin.";
            return RedirectToAction("DepartmentList", "Department");
        }

        [HttpPost]
        public IActionResult DepartmentEdit(DepartmentModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = new Department() {
                    DepartmentId = model.DepartmentId,
                    DepartmentName = model.DepartmentName,
                    DepartmentRowNumber = model.DepartmentRowNumber
                };

                _departmentDepartment.Update(entity);

                TempData["icon"] = "success";
                TempData["title"] = "Sosyal Medya güncellendi.";
                return RedirectToAction("DepartmentList", "Department");
            }
            return View(model);
        }

        public IActionResult DepartmentDelete(int id)
        {
            var entity = _departmentDepartment.GetById(id);
            if (entity != null)
            {
                _departmentDepartment.Delete(entity);
                
                TempData["icon"] = "success";
                TempData["title"] = "Sosyal Medya silindi.";
                return RedirectToAction("DepartmentList", "Department");
            }

            TempData["iconOK"] = "error";
            TempData["iconText"] = "Bir sorun oluştu. Lütfen tekrar deneyin.";
            return RedirectToAction("DepartmentList", "Department");
        }

    }
}