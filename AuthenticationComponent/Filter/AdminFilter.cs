using AuthenticationComponent.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AuthenticationComponent.Filter
{
    //Identify whether user is admin 
    public class AdminFilter : ActionFilterAttribute, IAuthorizationFilter
    {
     
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            int userId = (int)filterContext.HttpContext.Session["userId"];
            using(var db = new UserDb())
            {
                var user = db.Users.Where(u => u.Id == userId).FirstOrDefault();
                if (user.Username != "admin")
                {
                    filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary
                        {
                            {"controller", "Auth" },
                            {"action", "Index" }
                        }
                    );
                } else
                {
                    filterContext.HttpContext.Session["isAdmin"] = true;
                }
            }

        }

    }    
}