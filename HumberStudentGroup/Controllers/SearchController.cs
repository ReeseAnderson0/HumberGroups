using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HumberStudentGroup.ADO;
using HumberStudentGroup.Models;

namespace HumberStudentGroup.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        private HumberDBEntities db = new HumberDBEntities();

        public ActionResult Index(string searchTerm)
        {
            // get all items from these tables
            var Groups = from g in db.Groups select g;
            var Posts = from p in db.Posts select p;
            var Users = from u in db.Users select u;

            // check if there is a search term
            if (searchTerm != null)
            {
                Groups = Groups.Where(g => g.Title.Contains(searchTerm) || g.Desc.Contains(searchTerm));
                Posts = Posts.Where(p => p.Title.Contains(searchTerm) || p.Body.Contains(searchTerm));
                Users = Users.Where(u => u.Username.Contains(searchTerm));
            }
            // set the order of the models
            var GroupList = Groups.ToList().OrderByDescending(g => g.Users.Count);
            var PostList = Posts.ToList().OrderByDescending(g => g.Title);
            var UserList = Users.ToList().OrderByDescending(g => g.Username);

            // Create a searchModel and add the models to return
            SearchModelView models = new SearchModelView();
            models.Groups = GroupList;
            models.Posts = PostList;
            models.Users = UserList;

            return View(models);
        }
    }
}
