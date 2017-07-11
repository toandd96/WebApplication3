using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class ShoppingCartItem
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? Quantity { get; set; }

        public double? Price { get; set; }

        public string Image { get; set; }

        public double? Total { get; set; }
    }
}