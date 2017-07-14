using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using PagedList;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class HomeController : Controller
    {
        pjt3hEntities db = new pjt3hEntities();
        public ActionResult Index()
        {
            
            return View(db.Products.Take(6).ToList());
        }

        //public PartialViewResult 

        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}
        public ActionResult Login()
        {
            if (Session["TaiKhoan"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Login(string email, string password)
        {

            var customer = db.customers.SingleOrDefault(c => c.email == email);
            if (customer != null)
            {
                if (customer.password == password)
                {
                    Session["TaiKhoan"] = customer.id;
                    return RedirectToAction("ThanhToan", "Cart");
                }
            }
            return View();
        }

        public static string EncodePassword(string originalPassword)
        {
            //Declarations
            Byte[] originalBytes;
            Byte[] encodedBytes;
            MD5 md5;

            //Instantiate MD5CryptoServiceProvider, get bytes for original password and compute hash (encoded password)
            md5 = new MD5CryptoServiceProvider();
            originalBytes = ASCIIEncoding.Default.GetBytes(originalPassword);
            encodedBytes = md5.ComputeHash(originalBytes);

            //Convert encoded bytes back to a 'readable' string
            return BitConverter.ToString(encodedBytes);
        }

        
        
        public  ActionResult SearchProduct(string namePro)
        {

            var products = from m in db.Products
                           select (m);
            if(!string.IsNullOrEmpty(namePro))
            {
                products = db.Products.Where(p => p.name.Contains(namePro));
                return View(products);
            }
            return View(products);
        }

        public PartialViewResult _PartialCate()
        {
            
            return PartialView(db.Products.ToList());
        }

        public PartialViewResult _PartialCateConHang()
        {
            var category = db.Categories.Where(ca => ca.status == "Còn hàng").ToList();
            List<Product> product = new List<Product>();
            foreach(var item in category)
            {
                var sp = db.Products.Where(p => p.categoryid == item.id);
            }
            return PartialView(category.ToList());
        }

        public PartialViewResult _PartialPage1()
        {
            var product=db.Products.SingleOrDefault(c=>c.id)
            return PartialView();
        }

    }
}
