using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class ShoppingCartItem
    {
        public int id { get; set; }

        public string name { get; set; }

        public decimal? quantity { get; set; }

        public int price { get; set; }

        public decimal? total { get; set; }
    }
}