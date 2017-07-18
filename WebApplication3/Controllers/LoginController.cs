using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;
using Microsoft.Owin.Security;
using System.Security.Cryptography;
using System.Text;

namespace WebApplication3.Controllers
{
    public class LoginController : Controller
    {
        pjt3hEntities db = new pjt3hEntities();
        // GET: Login
        public ActionResult Index()
        {
            if (Session["Taikhoan"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
                return View();
        }

        [HttpPost]
        public ActionResult Index(string username, string password, string returnUrl)
        {
            
            
            var admin = db.Admins.SingleOrDefault(c => c.username == username);
            var cutadmpw = admin.password.ToString();
            //lấy khoảng trắng trong chuỗi
            string getspace = cutadmpw.TrimEnd();
            //xóa khoảng trắng
            
            if (admin == null)
            {
                ViewBag.Error = "Tên đăng nhập có thể chưa được nhập";
            }
            else
            {
                if (password == getspace.ToString())
                {
                    Session["admin"] = username;
                    var ident = new ClaimsIdentity(new[] {
                        new Claim(ClaimTypes.NameIdentifier,username),
                    new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider",
                    "ASP.NET Identity", "http://www.w3.org/2001/XMLSchema#string"),
                        new Claim(ClaimTypes.Name, username),}, DefaultAuthenticationTypes.ApplicationCookie);
                    HttpContext.GetOwinContext().Authentication.SignIn(
                        new AuthenticationProperties { IsPersistent = true }, ident
                        );
                    return RedirectToLocal(returnUrl);
                }
                else
                    ViewBag.Error = "Sai mật khẩu nhập lại";
               }
        
            return View();
    }
        public ActionResult SessionLogOff()
        {
            HttpContext.GetOwinContext().Authentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if(Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index","Products");
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

        public ActionResult GetUserName()
        {
            var identity = (ClaimsIdentity)User.Identity;
            string claims = identity.Name;
            return PartialView(claims);
        }
    }
}