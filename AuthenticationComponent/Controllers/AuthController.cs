using AuthenticationComponent.Data;
using AuthenticationComponent.Filter;
using AuthenticationComponent.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace AuthenticationComponent.Controllers
{
    public class AuthController : Controller
    {
        //Get the login page
        [HttpGet]
        public ActionResult Index()
        {
            Session["isAuth"] = false;
            return View("Login");
        }

        //Process login information posted to the server
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            bool isAuth = false;
            User currentUser = null;


            using (var db = new UserDb())
            {
                List<User> dbUsers = db.Users.ToList();
                foreach (var user in dbUsers)
                {
                    bool passwordIsVerified = Crypto.VerifyHashedPassword(user.Password, password);
                    if (user.Username == username && passwordIsVerified)
                    {
                        isAuth = true;
                        currentUser = user;
                    }
                }
            }

            //If authenticated, set session state to be so
            if (isAuth)
            {
                Session["userId"] = currentUser.Id;
                Session["username"] = currentUser.Username;
                Session["isAuth"] = isAuth;
                Session["isAdmin"] = username == "admin" ? true : false;

                return RedirectToAction("Index", "Gallery");
            }
            else
            {
                ViewBag.loginErrorMessage = "Username or password is incorrect";
                return View("Login");
            }
        }

        [HttpGet]
        public ActionResult Logout()
        {
            Session.Contents.Remove("isAuth");
            Session.Contents.Remove("isAdmin");
            return RedirectToAction("Index", "Auth");
        }

    }
}