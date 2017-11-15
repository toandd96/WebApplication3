using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        pjt3hEntities db = new pjt3hEntities();
        // GET: Order
        public ActionResult Index()
        {

            return View(db.Orders.ToList());
        }

        //get delete order
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadGateway);
            }

            var order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {
            var order = db.Orders.Find(id);
            var orderdetails = db.OrderDetails.Where(od => od.orderid == order.id).ToList();

            foreach(var item in orderdetails)
            {
                db.OrderDetails.Remove(item);
            }
            
            
            db.Orders.Remove(order);


            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
        {
            
            var orderdetail = db.OrderDetails.Where(od=>od.orderid==id).ToList();
            var order = db.Orders.Find(id);
            ViewData["status"] = order.status.ToString();
            return View(orderdetail);
        }
        [HttpPost]
        public ActionResult Details(Order order)
        {
            return View(order);
        }

        [HttpPost]
        public ActionResult Giao_Hang(int id)
        {
            if (ModelState.IsValid)
            {
                var order = db.Orders.Find(id);
                order.status = "Đã giao hàng";
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}