using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class OrderDetailsController : Controller
    {
        pjt3hEntities db = new pjt3hEntities();
        // GET: OrderDetails
        public ActionResult Details(int? id)
        {
            //var details = db.OrderDetails.Where(od => od.).ToList();
            return View();
        }
    }
}