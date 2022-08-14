using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HumberStudentGroup.ADO;

namespace HumberStudentGroup.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (HumberDBEntities context = new HumberDBEntities())
            {
                // get the groups as index
                return View(context.Groups.ToList());
            }
        }
        public ActionResult About()
        {
            return View();
        }
    }
}
