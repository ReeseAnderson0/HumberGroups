using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HumberStudentGroup.ADO;

namespace HumberStudentGroup.Controllers
{
    public class ChatController : Controller
    {
        // GET: Chat
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CreateMessage(string ChatMessage)
        {
            using (HumberDBEntities context = new HumberDBEntities())
            {
                // FIND THE GROUP/CHAT
                int GroupId = int.Parse(Session["GroupId"].ToString());

                var group = context.Groups.Single(g => g.Id == GroupId);
                // CREATE THE MESSAGE
                string Date = DateTime.Now.ToString("MM/dd HH:mm");
                var message = new Message
                {
                    Text = ChatMessage,
                    SentDate = Date,
                    UserId = Int32.Parse(Session["UserId"].ToString()),
                    ChatId = GroupId,
                };
                group.Chat.Messages.Add(message);
                context.SaveChanges();
                return RedirectToAction("Details", "Groups", group);
            }
        }

    }
}