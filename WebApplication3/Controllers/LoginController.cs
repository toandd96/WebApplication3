using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;
using Microsoft.Owin.Security;

namespace WebApplication3.Controllers
{
    public class LoginController : Controller
    {
        pjt3hEntities db = new pjt3hEntities();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string username, string password, string returnUrl)
        {
            var user = db.customers.SingleOrDefault(c => c.email == username);
            if (user == null)
            {
                ViewBag.Error = "Tên đăng nhập có thể chưa được nhập";
            }
            else
            {
                if (password == user.password)
                {
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
            return RedirectToAction("Index", "Login");
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if(Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index","Products");
        }

}
}