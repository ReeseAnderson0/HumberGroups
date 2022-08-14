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
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AutherizeLogin(HumberStudentGroup.ADO.User user)
        {
            using (HumberDBEntities context = new HumberDBEntities())
            {
                // check if the user is in the system
                var UserLogin = context.Users.Any(m => m.Username == user.Username && m.Password == user.Password);
                if (!UserLogin)
                {
                    return View("Index", user);
                }
                else
                {
                    // create session variables to be used across the session
                    var sessionUser = context.Users.Single(m => m.Username == user.Username);
                    HttpContext.Session.Add("UserId", sessionUser.Id);
                    HttpContext.Session.Add("Username", sessionUser.Username);
                    if (sessionUser.Type.Equals("Admin"))
                    {
                        HttpContext.Session.Add("AdminView", sessionUser.Id);
                    }
                    return RedirectToAction("Index", "Home");
                }
            }
        }
    }
}
