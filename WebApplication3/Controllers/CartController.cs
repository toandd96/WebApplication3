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
        // GET: Cart
        public ActionResult Index()
        {
            ViewBag.Tittle = "Giỏ Hàng";
            ShoppingCartModel model = new ShoppingCartModel();
            model.Cart = (ShopCart)Session["Cart"];
            return View(model);
        }

        public ActionResult ThemVaoGioHang
    }
}