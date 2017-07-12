using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication3.Models;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace WebApplication3.Controllers
{
    
    public class RegisterController : Controller
    {
        pjt3hEntities db = new pjt3hEntities();
        // GET: Register
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(customer customer)
        {
            try
            {
                if (ModelState.IsValid)
            {
                    
                    
                    customer.datecreate = DateTime.Now;
                    customer.status = "Bình thường";
                    db.customers.Add(customer);
                    db.SaveChanges();
                    ModelState.Clear();
                    ViewBag.Message = customer.name + "đã đăng ký thành công";
                    return RedirectToAction("Index", "Login");
               
            }
            }
            catch (Exception ex)
            {
                ViewBag.msg = "Email đã tồn tại mời bạn nhập email khác";
            }
            return View();

        }
        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            // Convert the byte array to hexadecimal string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
            }
            return sb.ToString();
        }
    }
}