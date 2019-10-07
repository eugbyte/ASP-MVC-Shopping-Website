using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuthenticationComponent.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string PurchaseDate { get; set; }

        //1 Order to Many Order Detail
        public ICollection<OrderDetails> OrderDetails { get; set; }

        //1 Customer to many Orders
        public int UserId { get; set; }
        public User User { get; set; }

    }
}