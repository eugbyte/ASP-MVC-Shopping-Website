using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Helpers;
using AuthenticationComponent.Data;
using AuthenticationComponent.Models;
using System.Web.Routing;
using System.Diagnostics;

namespace AuthenticationComponent.Filter
{
    
    public class AuthenticationFilter : ActionFilterAttribute, IAuthenticationFilter
    {
        //Identify whether user has logged in
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            bool isAuth = false;
            try
            {
                isAuth = (bool)filterContext.HttpContext.Session["isAuth"];
            }
            catch (NullReferenceException error)
            {
                Debug.WriteLine(error);
            }
                
            if (isAuth == false)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary
                        {
                            {"controller", "Auth" },
                            {"action", "Index" }
                        }
                    );
            }

        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            Debug.WriteLine("Direct route access attempted");
        }
    }
}