using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class CartController : Controller
    {
        pjt3hEntities db = new pjt3hEntities();
        List<OrderDetail> listOrderDetails = new List<OrderDetail>();
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
            if (P != null && P.quantity != 0)
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
            else

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
        public ActionResult ThanhToan(string ShipAddress, string ShipPhone)
        {
            try
            {
                if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == string.Empty)
                {
                    return RedirectToAction("Login", "Home");
                }
                int customId = int.Parse(Session["TaiKhoan"].ToString());
                var customer = db.customers.Find(customId);
                Order order = new Order();
               

                order.customerid = customId;
                order.orderdate = DateTime.Now;
                order.status = "Chưa";
                if (string.IsNullOrWhiteSpace(ShipAddress))
                {
                    order.shipaddress = customer.address;
                }
                else
                    order.shipaddress = ShipAddress;
                if (string.IsNullOrWhiteSpace(ShipPhone))
                {
                    order.shipphone = customer.phone;


                }
                else order.shipphone = ShipPhone;
                //ShipAddress = customer.address;
                //ShipPhone = customer.phone;
                int maxitem;
                var item1 = db.Orders.Count();
                int a = 0;
                if (item1 == a)
                {
                    maxitem = 1;
                }
                else
                {
                    var max = db.Orders.Max(o => o.id).ToString();
                    maxitem = Convert.ToInt32(max) + 1;
                }

                
                ShoppingCartModel model = new ShoppingCartModel();
                model.Cart = (ShopCart)Session["Cart"];
                order.total = model.Cart.total();
                db.Orders.Add(order);
                db.SaveChanges();
                foreach (var item in model.Cart.ListItem)
                {
                    OrderDetail orderDetails = new OrderDetail();
                    var product = db.Products.Single(p => p.id == item.Id);
                    var category = db.Categories.Single(c => c.id == product.categoryid);
                    orderDetails.orderid = maxitem;
                    orderDetails.price = item.Price;
                    orderDetails.image = item.Image;
                    orderDetails.productid = item.Id;
                    orderDetails.quantity = item.Quantity;
                    
                    product.quantity -= item.Quantity;
                    if (product.quantity == 0)
                    {
                        category.status = "Hết hàng";
                    }

                    db.OrderDetails.Add(orderDetails);
                   
                }

                db.SaveChanges();


                Session["Cart"] = null;
                TempData["msg"] = " Chúc mừng bạn đã đặt hàng thành công";
                return RedirectToAction("Index", "Cart");
            }
            catch (Exception ex)
            {
                //foreach (var validationErrors in dbEx.EntityValidationErrors)
                //{
                //    foreach (var validationError in validationErrors.ValidationErrors)
                //    {
                //        Trace.TraceWarning("Property: {0} Error: {1}",
                //                                validationError.PropertyName,
                //                                validationError.ErrorMessage);
                //    }
                //}
                return RedirectToAction("Index", "Cart");
            }
            //return View();
        }

        public ActionResult ThanhToanThanhCong()
        {
            return View();
        }

        public ActionResult XoaGioHang()
        {
            ShopCart objCart = (ShopCart)Session["Cart"];
            objCart.DeleteAllItems();
            Session["Cart"] = objCart;
            return View();
        }
    }
}