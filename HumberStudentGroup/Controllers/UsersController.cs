using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HumberStudentGroup.ADO;

namespace HumberStudentGroup.Controllers
{
    public class UsersController : Controller
    {
        private HumberDBEntities db = new HumberDBEntities();

        public ActionResult Signout() 
        {
            // if the user wants to sign out clear the session
            Session.Clear();
            return RedirectToAction("Index");
        }

        public ActionResult Index()
        {
            // get the users from the database
            return View(db.Users.ToList());
        }

        public ActionResult Details(int? id)
        {
            // check if the id is null
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // find the user from the id
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            // return the user view
            return View(user);
        }

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Username,Password")] User user)
        {
            // if the model is valid add it to the db
            if (ModelState.IsValid)
            {
                user.Type = "Student";
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        public ActionResult Edit(int? id)
        {
            // check if the id is null
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // find the user through its id
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            // return the user view
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Username,Password")] User user)
        {
            // check if the user model is valid
            if (ModelState.IsValid)
            {
                // apply chanes and save
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // find the uesr throught its id
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            // return the user view
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            // find the user that is going to be deleted
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
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
