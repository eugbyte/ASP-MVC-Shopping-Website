using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AuthenticationComponent.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public string Description { get; set; }
        public string Image { get; set; }

        //Many Products to 1 Order Detail
        public ICollection<OrderDetails> OrderDetails { get; set; }


        public Product()
        {
            //
        }

        public Product(string name, double price, string description, string image)
        {
            Name = name;
            Price = price;
            Description = description;
            Image = image;

        }
    }

}