using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YJournal.Models;

namespace YJournal.Controllers
{
    public class LessonsController : Controller
    {
        private DbJournal db = new DbJournal();
        private Users CurrentUser(string Email) {
            return db.Users.Where(p => p.Email == User.Identity.Name).First();
        }
        // GET: Lessons
        [Authorize]
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var id = CurrentUser(User.Identity.Name).UserID;
                var lessons = db.Lessons.Where(d=>d.TeacherId ==id).OrderByDescending(p=>p.DateLess).ToList();
                return View(lessons);
                //var lessons = db.Lessons.
            }
            else { 
                return RedirectToAction("index","index");
            }
        }

       

        public ActionResult New()
        {
            if (User.Identity.IsAuthenticated)
            {
                var id = CurrentUser(User.Identity.Name).UserID;
                return View(new Lessons {TeacherId = id});
            }
            else return RedirectToAction("index","index");
            
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult New(Lessons lesson) {
            lesson.State = false;
            lesson.DateLess = DateTime.Now;
            var LstStudents = db.Students.Where(p => p.GroupId == lesson.GroupId).ToList();
            if (LstStudents.Count() == 0) {
                ModelState.AddModelError(string.Empty, "В выбранной группе нет студентов!");
                return View(lesson);
            }
            db.Lessons.Add(lesson);
            db.SaveChanges();
            foreach (var item in LstStudents)
            {
                var mark = new Marks
                {
                    LessId = lesson.LessId,
                    StudentId = item.StudentId,
                    Mark = null,
                    Comments = "",
                    Activity = false,
                    Presence = false
                };
                db.Marks.Add(mark);
            }
            db.SaveChanges();
                return RedirectToAction("Marks", "Lessons", new {lessId = lesson.LessId});
        }
        [Authorize]
        public ActionResult Marks(int? lessId) {
            if (lessId == null) return RedirectToAction("index","Home");
            var lesson = db.Lessons.Where(p => p.LessId == lessId.Value).First();
            if (lesson.State.Value) return RedirectToAction("index");

            return View(new LessonMarks{LessonId = lesson.LessId });

        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Marks(LessonMarks lesm)
        {
            if (lesm.Action == 3) {
                return RedirectToAction("Delete",new { lessId = lesm.LessonId });
            }
            var jsonMarks = JsonConvert.DeserializeObject<List<Marks>>(lesm.Marks);
            foreach (var mark in jsonMarks) {
                db.Entry(mark).State = EntityState.Modified;
            }
            db.SaveChanges();
            switch (lesm.Action)
            {
                case 1: return RedirectToAction("Finish", new { lessId = lesm.LessonId });
                case 2: return RedirectToAction("Edit",new { lessId = lesm.LessonId});
                default: break;
            }
            return View(lesm);

        }
        [Authorize]
        public ActionResult Delete(int? lessId) {
            if (lessId == null) return RedirectToAction("index");
            var lesson = db.Lessons.Where(p=>p.LessId ==lessId);
            if (lesson==null) return RedirectToAction("index");
            return View(lesson.First());
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int lessid)
        {
            //Удаление всех оценок
            var marks = db.Marks.Where(p=>p.LessId==lessid);
            foreach (var curm in marks)
            {
                db.Marks.Remove(curm);
            }
            db.SaveChanges();
            var lesson = db.Lessons.Where(p => p.LessId == lessid);
            if (lesson == null) {
                ModelState.AddModelError(String.Empty,"Не удалось удалить занятие");
                return View(lessid); 
            }
            db.Lessons.Remove(lesson.First());
            db.SaveChanges();
            return RedirectToAction("index");
        }

        /*public ActionResult Marks(int? lessId) {
   if (lessId == null) return RedirectToAction("Index", "Home");
   else {
       Lessons lesson = db.Lessons.Where(p=>p.LessId == lessId).First();
       if(lesson==null) return RedirectToAction("Index", "Home");
       var LstStudents = db.Students.Where(p=>p.GroupId == lesson.GroupId).ToList();
       List<Marks> listMarks = new List<Marks>();
       foreach (var item in LstStudents) {
           var mark = new Marks{
               LessId = lesson.LessId,
               StudentId = item.StudentId,
               Mark=null,Comments="",
               Activity=false,
               Presence=false 
           };
           db.Marks.Add(mark);
           listMarks.Add(mark);
       }
       db.SaveChanges();
       return View(listMarks);
   }

}*/

        /*[HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Marks(List<Marks> marks) {
             return RedirectToAction("Index", "Home");

        }*/
        [Authorize]
        public ActionResult Finish(int? lessId) {
            if (lessId == null)return RedirectToAction("index");
            var lessons = db.Lessons.Where(p=>p.LessId== lessId);
            if(lessons.Count()==0)return RedirectToAction("index");
            return View(lessons.First());
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Finish(Lessons lesson)
        {
            lesson.State = true;
            db.Entry(lesson).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("index");
        }


        [Authorize]
        public ActionResult Edit(int? lessId) {
            if (lessId == null) return RedirectToAction("index");
            var lesson = db.Lessons.Where(p=>p.LessId== lessId);
            if(lesson.Count()==0)return RedirectToAction("index");
            var less = lesson.First();
            if(less.State.Value) RedirectToAction("index");
            return View(less);
        }

        [Authorize]
        public ActionResult Show(int? lessId)
        {
            if (lessId == null) return RedirectToAction("index");
            var lesson = db.Lessons.Where(p => p.LessId == lessId);
            if (lesson.Count() == 0) return RedirectToAction("index");
            var less = lesson.First();
            if (less.State.Value) RedirectToAction("index");
            return View(less);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Lessons lesson)
        {
            if (ModelState.IsValid)
            {
                if (lesson.State.Value) RedirectToAction("index");
                db.Entry(lesson).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Marks", new { lessId = lesson.LessId });
            }
            return View(lesson);
        }

        [Authorize]
        public ActionResult ShowMarks() {

            return PartialView("_ShowMarks");
        }

    }
}