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
    public class GroupsController : Controller

    {
        // Declaring the db to be used across the controller
        private HumberDBEntities db = new HumberDBEntities();

        public ActionResult Index(string searchTerm)
        {
            // get all the groups
            var Groups = from g in db.Groups select g;
            // if there is a search term change groups to search
            if (searchTerm != null)
            {
                Groups = Groups.Where(g => g.Title.Contains(searchTerm) || g.Desc.Contains(searchTerm));
            }
            // return view of groups
            return View(Groups.ToList().OrderByDescending(g => g.Users.Count));
        }

        public ActionResult Details(int id)
        {
            // find the group with the id
            Group group = db.Groups.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }

            // get the author
            var author = db.Users.Find(group.AuthorId);
            // set the session vars
            Session["GroupId"] = group.Id;
            Session["Author"] = author.Username;
            return View(group);
        }

        public ActionResult Create()
        {
            // create a session with the user id
            ViewBag.AuthorId = Session["UserId"];
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Desc,AuthorId")] Group group)
        {
            // check if the model is valid
            if (ModelState.IsValid)
            {
                // find the user and create the group
                var sessionUser = db.Users.Single(m => m.Id == group.AuthorId);
                group.Users.Add(sessionUser);
                group.Chat = new Chat();
                db.Groups.Add(group);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(group);
        }

        public ActionResult Join(int id)
        {
            // find the group to join
            Group group = db.Groups.Find(id);
            // find the user that wants to join
            int userId = int.Parse(Session["UserId"].ToString());
            User sessionUser = db.Users.Single(m => m.Id == userId);
            if (group == null)
            {
                return HttpNotFound();
            }
            // add the user to the group
            group.Users.Add(sessionUser);
            db.SaveChanges();
            return RedirectToAction("Details", group);
        }

        public ActionResult Leave(int id)
        {
            // find the group
            Group group = db.Groups.Find(id);
            int userId = int.Parse(Session["UserId"].ToString());
            User sessionUser = db.Users.Single(m => m.Id == userId);
            if (group == null || userId == group.AuthorId)
            {
                return HttpNotFound();
            }
            // remove the user from the group
            group.Users.Remove(sessionUser);
            db.SaveChanges();
            return RedirectToAction("Details", group);
        }

        public ActionResult Edit(int id)
        {
            // find the group to edit
            ViewBag.AuthorId = Session["UserId"];
            Group group = db.Groups.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Desc,AuthorId")] Group group)
        {
            // if the model is valid apply the updates
            if (ModelState.IsValid)
            {
                db.Entry(group).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(group);
        }

        public ActionResult Delete(int? id)
        {
            // check if the id is null
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // find the group
            Group group = db.Groups.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            // return the group
            return View(group);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            // check if the user wants to delete this group and save
            Group group = db.Groups.Find(id);
            group.Users.Clear();
            db.Groups.Remove(group);
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
