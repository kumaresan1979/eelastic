using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Http;

namespace appathon_component.Models
{
    public class Product
    {
        public int ProductId { get; set; } 
        public string ProductName { get; set; }
        public string ProductType { get; set; }
        public int Quantity { get; set; }
        public string ProductImage { get; set; }

        public static Product GetProduct()
        {
            Product product = new Product() ;
            return product;
        }
    }
}