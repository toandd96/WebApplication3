using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class ShopCart
    {
        public List<ShoppingCartItem> ListItem { get; set; }

        public ShopCart()
        {
            ListItem = new List<ShoppingCartItem>();
        }

        public void AddToCart(ShoppingCartItem item)
        {
            if(ListItem.Where(s=>s.Id==item.Id).Any())
            {
                var myItem = ListItem.Single(s => s.Id == item.Id);
                myItem.Quantity += myItem.Quantity;
                myItem.Total = myItem.Quantity * myItem.Price;
            }
            else
            {
                ListItem.Add(item);
            }
        }

        public bool RemoveFromCart(int lngProductSellId)
        {
            ShoppingCartItem exitsitem = ListItem.Where(i=>i.Id==lngProductSellId).SingleOrDefault();
            if(exitsitem!=null)
            {
                ListItem.Remove(exitsitem);
            }
            return true;
        }

        public bool UpdateQuantity(int lngProductSellId,int intQuantity)
        {
            ShoppingCartItem exitsitem = ListItem.Where(i => i.Id == lngProductSellId).SingleOrDefault();
            if(exitsitem!=null)
            {
                exitsitem.Quantity = intQuantity;
                exitsitem.Total = exitsitem.Quantity * exitsitem.Price;
            }
            return true;
        }

        public bool DeleteAllItems()
        {
            ListItem.Clear();
            return true;
        }
    }
}