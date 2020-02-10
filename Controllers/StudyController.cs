using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YJournal.Models;

namespace YJournal.Controllers
{
    public class StudyController : Controller
    {
        DbJournal db = new DbJournal();
        // GET: Study
        [Authorize]
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated) return RedirectToAction("Index","Home");
            var user = db.ViewStudents.Where(p=>p.Email==User.Identity.Name);
            if(user.Count()==0) return RedirectToAction("Index", "Home");
            var student = user.First();

            var GroupLessons = db.Lessons.Where(p=>p.GroupId==student.GroupId&&p.State==true).OrderByDescending(p=>p.DateLess);

            return View(GroupLessons);
        }

        [Authorize]
        public ActionResult Marks()
        {
            if (!User.Identity.IsAuthenticated) return RedirectToAction("Index", "Home");
            var user = db.ViewStudents.Where(p => p.Email == User.Identity.Name);
            if (user.Count() == 0) return RedirectToAction("Index", "Home");
            var student = user.First();

            return View(student);
        }
    }
}