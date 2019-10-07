using AuthenticationComponent.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AuthenticationComponent.Data
{
    
    public class UserDb : DbContext
    {
        public UserDb() : base("Server=DESKTOP-43QPGUG;" +
                "Database=ShopDb; Integrated Security=true")
        {
            Database.SetInitializer(new UserDbInitializer<UserDb>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //To create inherited class
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ActivationCode> ActivationCodes { get; set; }

    }
    
}