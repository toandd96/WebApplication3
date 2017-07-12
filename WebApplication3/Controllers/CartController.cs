using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class CartController : Controller
    {
        pjt3hEntities db = new pjt3hEntities();
        // GET: Cart
        public ActionResult Index()

        {
            ViewBag.Tittle = "Giỏ Hàng";
            ShoppingCartModel model = new ShoppingCartModel();
            model.Cart = (ShopCart)Session["Cart"];
            return View(model);
        }

        public ActionResult ThemVaoGioHang(int id)
        {
            var P = db.Products.Single(p => p.id == id);
            if (P != null)
            {
                ShopCart objCart = (ShopCart)Session["Cart"];
                if (objCart == null)
                {
                    objCart = new ShopCart();
                }
                ShoppingCartItem item = new ShoppingCartItem()
                {
                    Name = P.name,
                    Id = P.id,
                    Quantity = 1,
                    Price = P.price,
                    Total = P.price,
                    Image = P.image
                };
                objCart.AddToCart(item);
                Session["Cart"] = objCart;
                return RedirectToAction("Index", "Cart");
            }
            return RedirectToAction("Index", "Cart");
        }

        public ActionResult UpdateQuantity(int proID, int quantity)
        {
            ShopCart objCart = (ShopCart)Session["Cart"];
            if (objCart != null)
            {
                objCart.UpdateQuantity(proID, quantity);
                Session["Cart"] = objCart;
            }
            return RedirectToAction("Index");
        }
        public ActionResult XoaSanPham(int id)
        {
            ShopCart objCart = new ShopCart();
            if (objCart != null)
            {
                objCart.RemoveFromCart(id);
                Session["Cart"] = objCart;
            }
            return RedirectToAction("Index");
        }

        public ActionResult ThanhToan()
        {
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == string.Empty)
            {
                return RedirectToAction("Login", "Home");
            }
            ShoppingCartModel model = new ShoppingCartModel();
            model.Cart = (ShopCart)Session["Cart"];
            int customId = int.Parse(Session["TaiKhoan"].ToString());
            var customer = db.customers.Find(customId);
            ViewBag.customer = customer;
            return View(model);
        }
        [HttpPost]
        public ActionResult ThanhToan(string ShipAdress, string ShipPhone)
        {
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == string.Empty)
            {
                return RedirectToAction("Login", "Home");
            }
            int customId = int.Parse(Session["TaiKhoan"].ToString());
            var customer = db.customers.Find(customId);
            Order order = new Order();
            OrderDetail orderDetails = new OrderDetail();
            order.customerid = customId;
            order.orderdate = DateTime.Now;
            order.status = "Chưa";
            if (ShipAdress == null || ShipAdress == string.Empty)
            {
                order.shipaddress = customer.address;
            }
            else
                order.shipaddress = ShipAdress;
            if (ShipPhone == null || ShipPhone == string.Empty)
            {
                order.shipphone = customer.phone;
            }
            else order.shipphone = ShipPhone;
            var maxitem = db.Orders.Count();
            ShoppingCartModel model = new ShoppingCartModel();
            
            model.Cart = (ShopCart)Session["Cart"];
            foreach (var item in model.Cart.ListItem)
            {

                orderDetails.orderid = maxitem + 1;
                orderDetails.price = item.Price;
                orderDetails.productid = item.Id;
                orderDetails.quantity = item.Quantity;
                order.total = item.Total;
            }
            db.Orders.Add(order);
            db.SaveChanges();
            db.OrderDetails.Add(orderDetails);
            db.SaveChanges();
            Session["Cart"] = null;
            TempData["msg"] = " Chúc mừng bạn đã đặt hàng thành công";
            return RedirectToAction("Index", "Cart");
        }

        public ActionResult ThanhToanThanhCong()
        {
            return View();
        }
    }
}