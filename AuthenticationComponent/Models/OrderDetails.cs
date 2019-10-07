using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AuthenticationComponent.Models
{
    public class OrderDetails
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }

        //Many OrderDetail to One Order
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }

        //1 OrderDetails to Many Products
        public int ProductId { get; set; }
        public Product Product { get; set; }

        //1 OrderDetails to Many ActivationCodes        
        public string ActivationCodes { get; set; }
    }
}