using AuthenticationComponent.Data;
using AuthenticationComponent.Filter;
using AuthenticationComponent.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;

namespace AuthenticationComponent.Controllers
{
    //Only admin can access this route
    [AdminFilter]
    public class AdminController : Controller
    {
        
        
        [HttpGet]
        public ActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddProduct(Product product, HttpPostedFileBase Image)
        {
            
            using (var db = new UserDb())
            {
                try
                {
                    if (Image != null)
                    {
                        string fileName = Path.GetFileName(Image.FileName);
                        string path = Path.Combine(Server.MapPath("/Content/Images"), fileName);
                        Debug.WriteLine(path);
                        //file is uploaded
                        Image.SaveAs(path);
                        product.Image = "/Content/Images" + "/" + fileName;
                    }
                }
                catch (Exception error)
                {
                    throw new IOException(error.Message);
                }
                db.Products.Add(product);
                db.SaveChanges();
            }

            return RedirectToAction("Index", "Gallery");
        }
    }
}