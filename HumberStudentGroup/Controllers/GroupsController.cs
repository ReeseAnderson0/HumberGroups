﻿using System;
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
        private HumberDBEntities db = new HumberDBEntities();

        public ActionResult Index()
        {
            return View(db.Groups.ToList());
        }

        public ActionResult Details(int id)
        {
            Group group = db.Groups.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            var author = db.Users.Find(group.AuthorId);
            Session["Author"] = author.Username;
            return View(group);
        }

        public ActionResult Create()
        {
            ViewBag.AuthorId = Session["UserId"];
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Desc,AuthorId")] Group group)
        {
            if (ModelState.IsValid)
            {
                var sessionUser = db.Users.Single(m => m.Id == group.AuthorId);
                group.Users.Add(sessionUser);
                db.Groups.Add(group);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(group);
        }

        public ActionResult Join(int id)
        {
            Group group = db.Groups.Find(id);
            int userId = int.Parse(Session["UserId"].ToString());
            User sessionUser = db.Users.Single(m => m.Id == userId);
            if (group == null)
            {
                return HttpNotFound();
            }
            group.Users.Add(sessionUser);
            db.SaveChanges();
            return RedirectToAction("Details", group);
        }

        public ActionResult Leave(int id)
        {
            Group group = db.Groups.Find(id);
            int userId = int.Parse(Session["UserId"].ToString());
            User sessionUser = db.Users.Single(m => m.Id == userId);
            if (group == null || userId == group.AuthorId)
            {
                return HttpNotFound();
            }
            group.Users.Remove(sessionUser);
            db.SaveChanges();
            return RedirectToAction("Details", group);
        }

        public ActionResult Edit(int id)
        {
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
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = db.Groups.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
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
