using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Concrete;
using Data.Concrete.EfCore;
using Entity.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class PointController : Controller
    {
        private UserManager<ProjeUser> _userManager;
        public PointController(UserManager<ProjeUser> userManager)
        {
            _userManager = userManager;
        }
        PointManager _pointService = new PointManager(new PointDal());
        MentorManager _mentorService = new MentorManager(new MentorDal());
        
        public async Task<IActionResult> Index(int point, int mentorId)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var mentor = _mentorService.GetById(mentorId);
            var sorgula = _pointService.GetAll().Where(i => i.PointScorerUserId == user.Id && i.MentorId == mentorId).FirstOrDefault();
            if (sorgula == null)
            {
                var entity = new Point()
                {
                    PointNo = point,
                    PointScorerUserId = user.Id,
                    MentorId = mentorId,
                };
                _pointService.Create(entity);
                return RedirectToAction("MentorDetail", "Student", new { userId = mentor.Id});
            }
            TempData["icon"] = "warning";
            TempData["title"] = "Daha önce puan verilmiş.";
            return RedirectToAction("MentorDetail", "Student", new { userId = mentor.Id});
        }
    }
}