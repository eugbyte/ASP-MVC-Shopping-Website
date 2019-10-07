using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace AuthenticationComponent.Controllers
{
    public class ContactUsController : Controller
    {
        [HttpGet]
        public ActionResult ContactForm()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SubmitFeedBack(string comment, string email)
        {
            System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();

            message.From = new System.Net.Mail.MailAddress(email);
            message.To.Add(new System.Net.Mail.MailAddress("company@email.com"));

            message.IsBodyHtml = true;
            message.BodyEncoding = Encoding.UTF8;
            message.Subject = "Feedback";
            message.Body = comment;

            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
            //just testing, don't actually send
            //client.Send(message); 
            return RedirectToAction("Index", "Gallery");
        }
    }
}