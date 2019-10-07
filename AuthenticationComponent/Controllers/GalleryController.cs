using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AuthenticationComponent.Models;
using AuthenticationComponent.Data;
using AuthenticationComponent.Filter;

namespace AuthenticationComponent.Controllers
{
    public class GalleryController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            using(var db = new UserDb())
            {
                List<Product> products = db.Products.ToList();
                ViewData["products"] = products;

                //Display number of items in cart
                var count = 0;
                if (Session["Cart"] != null)
                {
                    List<Product> sessionProducts = Session["Cart"] as List<Product>;
                    count = sessionProducts.Count();
                }

                ViewBag.count = count;
            }
            return View("Index");
        }

        // GET: Purchase
        [AuthenticationFilter]
        public ActionResult AddToCart(int? productId)
        {
            
            if (productId == null)
            {
                return RedirectToAction("Index", "Gallery");
            }

            using (var db = new UserDb())
            {
                Product product = db.Products.Where(p => p.Id == productId).FirstOrDefault();
                if (product == null)
                    return RedirectToAction("Index", "Gallery");

                if (Session["Cart"] == null)
                {
                    Session["Cart"] = new List<Product>() { product };
                } else
                {
                    List<Product> sessionProducts = Session["Cart"] as List<Product>;
                    sessionProducts.Add(product);
                    Session["Cart"] = sessionProducts;
                }
            }
                       
            return RedirectToAction("Index", "Gallery");
        }

        

        
    }
}