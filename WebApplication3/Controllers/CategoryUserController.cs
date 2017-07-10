using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;
using System.Data.Entity;
using System.Net;

namespace WebApplication3.Controllers
{
    public class CategoryUserController : Controller
    {
        pjt3hEntities db = new pjt3hEntities();
        [HttpGet]
        // GET: CategoryUser
        public ActionResult Index(int? id)
        {
            if(id.HasValue)
            {
                return View(db.Products.Where(c=>c.categoryid==id).ToList());
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            
        }
        public PartialViewResult MenuCategories()
        {
            
            return PartialView(db.Categories.ToList());
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }

            return View(category);
        }
    }
}