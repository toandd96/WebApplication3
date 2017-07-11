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
            if(ListItem.Where(s=>s.id==item.id).Any())
            {
                var myItem = ListItem.Single(s => s.id == item.id);
                myItem.quantity += myItem.quantity;
                myItem.total = myItem.quantity * myItem.price;
            }
            else
            {
                ListItem.Add(item);
            }
        }

        public bool RemoveFromCart(int lngProductSellId)
        {
            ShoppingCartItem exitsitem = ListItem.Where(i=>i.id==lngProductSellId).SingleOrDefault();
            if(exitsitem!=null)
            {
                ListItem.Remove(exitsitem);
            }
            return true;
        }

        public bool UpdateQuantity(int lngProductSellId,int intQuantity)
        {
            ShoppingCartItem exitsitem = ListItem.Where(i => i.id == lngProductSellId).SingleOrDefault();
            if(exitsitem!=null)
            {
                exitsitem.quantity = intQuantity;
                exitsitem.total = exitsitem.quantity * exitsitem.price;
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