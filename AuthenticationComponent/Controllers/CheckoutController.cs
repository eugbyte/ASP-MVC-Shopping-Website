using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AuthenticationComponent.Data;
using AuthenticationComponent.Filter;
using AuthenticationComponent.Models;

namespace AuthenticationComponent.Controllers
{
    [AuthenticationFilter]
    public class CheckoutController : Controller
    {
        [HttpPost]
        public ActionResult ProcessOrder(List<int>productIds, List<int>quantities)
        {

            if(productIds == null && quantities == null)
            {
                return RedirectToAction("ViewCart", "Cart");
            }

            using (var db = new UserDb())
            {
                //Generate a new Order for each cart purchase
                Order order = new Order();

                List<string> currentDate = DateTime.Now
                                 .ToString()
                                 .Split(' ')
                                 .ToList();
                order.PurchaseDate = currentDate[0] + " " + currentDate[1];
                order.UserId = (int)Session["userId"];
                db.Orders.Add(order);
                //To let EF auto generate the order id for us
                db.SaveChanges();

                List<OrderDetails> orderDetailLst = new List<OrderDetails>() { };

                //For every product in the cart...            
                for (int i = 0; i < productIds.Count; i++)
                {
                    int prodId = productIds[i];
                    int quantity = quantities[i];
                
                    Product product = db.Products.Where(p => p.Id == prodId).FirstOrDefault();
                    if (product == null)
                    {
                        continue;
                    }
                   
                    OrderDetails orderDetail = new OrderDetails();
                    orderDetail.Price = product.Price;
                    orderDetail.Quantity = quantity;
                    orderDetail.ProductId = product.Id;


                    //For each quantity, generate an ActivationCode
                    for (int j = 0; j < quantity; j++)
                    {
                        //Retrieve unused ActivationCode - unused ActivationCodes have not been assigned a UserID
                        ActivationCode code = db.ActivationCodes
                                                .Where(ac => ac.UserId == null || ac.UserId == 0)
                                                .FirstOrDefault();
                        code.UserId = (int)Session["userId"];
                        db.SaveChanges();

                        //resolve off-by-one error
                        if (j == 0)
                        {
                            orderDetail.ActivationCodes = code.Id.ToString();
                        } else if (j >= 1)
                        {
                            orderDetail.ActivationCodes += "," + code.Id; 
                            //use string.split(',') later to retrieve individual ActivationCodes
                        }
                        
                    }
                     
                    orderDetailLst.Add(orderDetail);
                }

                //Add to the OrderDetails table using the navigation property of Order
                order.OrderDetails = orderDetailLst;
                db.SaveChanges();

                //Remove the Cart from Session
                Session.Contents.Remove("Cart");
            }

            return RedirectToAction("OrderHistory");
        }

        public ActionResult OrderHistory()
        {
            using(var db = new UserDb())
            {
                int userId = (int)Session["userId"];

                //For each order, select date, name, price, quantity, image, activationCodes
                var dnpqia = from c in db.Users
                             where c.Id == userId
                             join o in db.Orders
                             on c.Id equals o.UserId
                             join od in db.OrderDetails
                             on o.Id equals od.OrderId
                             join p in db.Products
                             on od.ProductId equals p.Id
                             orderby o.PurchaseDate
                             select new
                             {
                                 date = o.PurchaseDate,
                                 price = p.Price,
                                 quantity = od.Quantity,
                                 name = p.Name,
                                 image = p.Image,
                                 activationCode = od.ActivationCodes
                           };

                //Destructure query result to pass into ViewBag
                List<string> names = new List<string>();
                List<double> prices = new List<double>();
                List<string> dates = new List<string>();
                List<int> quantities = new List<int>();
                List<string> images = new List<string>();
                List<string> activationCodes = new List<string>();

                foreach(var row in dnpqia)
                {
                    names.Add(row.name);
                    dates.Add(row.date);
                    quantities.Add(row.quantity);
                    prices.Add(row.price);
                    images.Add(row.image);
                    activationCodes.Add(row.activationCode);
                }

                ViewBag.dates = dates;
                ViewBag.names = names;
                ViewBag.prices = prices;
                ViewBag.quantities = quantities;
                ViewBag.images = images;
                ViewBag.activationCodes = activationCodes;
             
            }

            return View();
        }
    }
}