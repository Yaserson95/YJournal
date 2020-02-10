using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using YJournal.Models;

namespace YJournal.Controllers
{
    public class UsersController : Controller
    {
        private DbJournal db = new DbJournal();
        //Users I;
        public UsersController() {
            /*if (User.Identity.IsAuthenticated) {
                var users = db.Users.Where(p => p.Email == User.Identity.Name);
                if (users.Count() > 0) I = users.First();
            }*/

        }
        // GET: Users
        [Authorize]
        public async Task<ActionResult> Index()
        {

            if (User.Identity.IsAuthenticated) {
                var users = db.Users.Where(p => p.Email != User.Identity.Name);
                return View(await users.ToListAsync());
            } else return RedirectToAction("Index", "Index");

        }
        public async Task<ActionResult> Teachers() {
            if (User.Identity.IsAuthenticated)
            {
                var teachers = db.ViewTeachers.Where(p => p.Email != User.Identity.Name);
                return View(await teachers.ToListAsync());
            }
            else return RedirectToAction("Index", "Index");
        }
        public async Task<ActionResult> Students()
        {
            if (User.Identity.IsAuthenticated)
            {
                var students = db.ViewStudents.Where(p => p.Email != User.Identity.Name);
                return View(await students.ToListAsync());
            }
            else return RedirectToAction("Index", "Index");
        }


        // GET: Users/Details/5
        [Authorize]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var users = await db.Users.FindAsync(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        //==============================================Добавление пользователя========================================================={
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Users users)
        {

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Некорректные данные");
                return View(users);
            }
            else {
                db.Users.Add(users);
                db.SaveChanges();
                if (users.NPost == 3 || users.NPost == 2) {
                    var UsId = db.Users.Where(p => p.Email == users.Email).First();
                    if (users.NPost == 2) return RedirectToAction("CreateTeacher", new { id = UsId.UserID });
                    if (users.NPost == 3) return RedirectToAction("CreateStudent", new { id = UsId.UserID });
                }
                return RedirectToAction("index");
            }
        }

        //}
        //=============================Данные для учителя========================={
        [Authorize]
        public ActionResult CreateTeacher(int? id) {
            Teachers teacher = new Teachers { TeacherId = id.Value };

            return View(teacher);
        }


        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult CreateTeacher(Teachers Teacher) {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Некорректные данные");
                return View(Teacher);
            }
            var infoTh = db.UserToTeacher(Teacher.TeacherId, Teacher.NamePost, Teacher.GroupOwn).ToList();
            if (infoTh != null && Convert.ToInt32(infoTh[0]) != -1)
            {
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Некорректно введены данные");
                return View(Teacher);
            }

        }

        //}
        //========================================Данные для студента=============================={
        [Authorize]
        public ActionResult CreateStudent(int? id)
        {
            Students student = new Students { StudentId = id.Value };
            return View(student);
        }


        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult CreateStudent(Students student)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Некорректные данные");
                return View(student);
            }
            var infoTh = db.UserToStudent(student.StudentId, student.GroupId, student.ReceiptDate).ToList();
            if (infoTh != null && Convert.ToInt32(infoTh[0]) != -1)
            {
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Некорректно введены данные");
                return View(student);
            }

        }


        // }





        // GET: Users/Edit/5
        [Authorize]
        public async Task<ActionResult> Edit(int? id)
        {
             
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var users = await db.Users.FindAsync(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.Teachers, "TeacherId", "NamePost", users.UserID);
            return View(users);
        }

        // POST: Users/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.




        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "UserID,Email,Surname,UserName,Birth,Phone,NPost,Password")] Users users)
        {
            if (ModelState.IsValid)
            {
                db.Entry(users).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.Teachers, "TeacherId", "NamePost", users.UserID);
            return View(users);
        }

        // GET: Users/Delete/5
        [Authorize]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var users = await db.Users.FindAsync(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // POST: Users/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            db.DeleteUser(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
