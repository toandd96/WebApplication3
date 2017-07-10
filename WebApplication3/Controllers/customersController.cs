using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;
using System.Security.Cryptography;
using System.Text;


namespace WebApplication3.Controllers
{
    [Authorize]
    public class customersController : Controller
    {
        private pjt3hEntities db = new pjt3hEntities();

        // GET: customers
        public ActionResult Index()
        {
            return View(db.customers.ToList());
        }

        // GET: customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            customer customer = db.customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: customers/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: customers/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "id,name,gender,email,address,phone,password,status,datevreate")] customer customer)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.customers.Add(customer);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(customer);
        //}

        // GET: customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            customer customer = db.customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,gender,email,address,phone,password,status,datevreate")] customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            customer customer = db.customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            customer customer = db.customers.Find(id);
            db.customers.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[ValidateInput(false)]
        //public ActionResult Register([Bind(Include = "name,phone,email,password,confirmpassword,datecreate,gender,status,address")] customer customer)
        //{
        //    if (ModelState.IsValid)
        //    {
                
        //        var a = db.customers.ToList();
        //        var mahoapass = EncodePassword(customer.password);
        //        customer.password = mahoapass;
        //        customer.confirmpassword = mahoapass;
        //        customer.status = "NULL";
        //        customer.address = "NULL";
        //        customer.datecreate = DateTime.Now;
        //        customer.gender = "NULL";
        //        db.customers.Add(customer);
        //        db.SaveChanges();
        //        return View("Login");
                
                
        //    }
        //    return View(customer);
        //}
        
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[ValidateInput(false)]
        //public ActionResult Login(FormCollection f)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        string taikhoan = f["txttaikhoan"].ToString();
        //        string matkhau = f["txtmatkhau"].ToString();
        //        string mahoapass = EncodePassword(matkhau);
        //        customer customer = db.customers.SingleOrDefault(c => c.email == taikhoan && c.password == mahoapass);
        //        if (customer != null)
        //        {
        //            ViewBag.message = ("Đăng nhập thành công");
        //            Session["Email"] = taikhoan;
        //            Session["password"] = matkhau;
        //            return RedirectToAction("/");
        //        }
        //        ViewBag.message = "Tài khoản hoặc mật khẩu của bạn không chính xác";
        //        return View();
        //    }
        //    return RedirectToAction("/");
        //}
    }
}
