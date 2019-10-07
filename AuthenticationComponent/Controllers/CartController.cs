using AuthenticationComponent.Data;
using AuthenticationComponent.Filter;
using AuthenticationComponent.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AuthenticationComponent.Controllers
{
    [AuthenticationFilter]
    public class CartController : Controller
    {
        [HttpGet]
        public ActionResult ViewCart(int? productId, int? quantity)
        {

            //Dictionary<productName, ListofSaidProducts> to be passed to ViewBag
            var checkoutDict = new Dictionary<string, List<Product>>();

            if (Session["Cart"] == null)
            {
                ViewBag.checkoutDict = checkoutDict;
                return View();
            }

            List<Product> sessionProducts = Session["Cart"] as List<Product>;

            //should user add product while in the cart, this actionMethod will receive productId, quantity as query parameters
            //recalculate the amount of the type of product in the session object
            if (productId != null && quantity != null)
            {
                using (var db = new UserDb())
                {
                    sessionProducts.RemoveAll(prod => prod.Id == productId);

                    Product product = db.Products.Where(prod => prod.Id == productId).FirstOrDefault();
                    for (var i = 0; i < quantity; i++)
                    {
                        sessionProducts.Add(product);
                    }                    
                    Session["Cart"] = sessionProducts;
                }
            }

            //Calculate the total price of products in Cart
            double total = sessionProducts.Sum(product => product.Price);

            //convert list of products into dictionary, with the key being the productName
            var groupedProducts = from product in sessionProducts
                                  group product by product.Name into newGroup
                                  orderby newGroup.Key
                                  select newGroup;

            foreach (var group in groupedProducts)
            {
                string key = group.Key;
                List<Product> values = new List<Product>();
                
                foreach(var product in group)
                {
                    values.Add(product);
                }

                checkoutDict.Add(key, values);
            }

            ViewBag.checkoutDict = checkoutDict;
            ViewBag.total = total;
            return View();
        }

        
    }
}

