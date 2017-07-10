using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class SuppliersUserController : Controller
    {
        pjt3hEntities db = new pjt3hEntities();
        // GET: SuppliersUser
        public ActionResult Index(int? id)
        {
            if (id.HasValue)
            {
                return View(db.Products.Where(s => s.supplierid == id).ToList());
            }
            else return RedirectToAction("Index","Home");
        }
        public PartialViewResult MenuSupplier()
        {
            return PartialView(db.Suppliers.ToList());
        }
    }
}