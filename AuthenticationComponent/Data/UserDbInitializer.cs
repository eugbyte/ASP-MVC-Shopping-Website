using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using AuthenticationComponent.Models;

namespace AuthenticationComponent.Data
{
    public class UserDbInitializer<T> : DropCreateDatabaseAlways<UserDb>
    {
        protected override void Seed(UserDb context)
        {
            User admin = new User("admin", "admin");
            User customer = new User("customer", "customer");

            context.Users.Add(admin);
            context.Users.Add(customer);
            
            List<Product> products = new List<Product>()
            {
                new Product(".NET Charts", 99, "Brings powerful charting applications", "/Content/Images/Charts.png"),
                new Product(".NET PayPal", 69, "Integrate your .NET apps with Paypal the easy way", "/Content/Images/Paypal.png"),
                new Product(".NET ML", 299, "Supercharged .NET machine learning libraries", "/Content/Images/ML.png"),
                new Product(".NET Analytics", 299, "Performs data mining and analytics easily in .NET", "/Content/Images/Analytics.png"),
                new Product(".NET Logger", 49, "Logs and aggregates events easily", "/Content/Images/Logger.jpg")
            };

            context.Products.AddRange(products);

            //Generate 20 unused ActivationCodes
            for(int i = 0; i < 20; i++)
            {
                context.ActivationCodes.Add(new ActivationCode());
            }

            base.Seed(context);
        }
    }
}