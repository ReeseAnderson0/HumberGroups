using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HumberStudentGroup.ADO;

namespace HumberStudentGroup.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AutherizeLogin(HumberStudentGroup.ADO.User user)
        {
            using (HumberDBEntities context = new HumberDBEntities())
            {
                var UserLogin = context.Users.Any(m => m.Username == user.Username && m.Password == user.Password);
                if (!UserLogin)
                {
                    return View("Index", user);
                }
                else
                {
                    var sessionUser = context.Users.Single(m => m.Username == user.Username);
                    HttpContext.Session.Add("UserId", sessionUser.Id);
                    return RedirectToAction("Index", "Home");
                }
            }
        }
    }
}
