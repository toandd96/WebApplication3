using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;
using System.IO;

namespace WebApplication3.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private pjt3hEntities db = new pjt3hEntities();

        public JsonResult GetAllProduct()
        {
            var product = db.Products.OrderByDescending(p => p.datecreate);
            return Json(from pro in product
                        select new
                        {
                            id=pro.id,
                            name=pro.name,
                            image=pro.image,
                            price=pro.price,
                            quantity=pro.quantity,
                            information=pro.information,
                            datecreate=pro.datecreate,
                            description=pro.description
                        },JsonRequestBehavior.AllowGet);
        }
        // GET: Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Category).Include(p => p.Supplier);
            return View(products.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            //ViewBag.authorid = new SelectList(db.Authors, "id", "name");
            ViewBag.categoryid = new SelectList(db.Categories, "id", "name");
            ViewBag.supplierid = new SelectList(db.Suppliers, "id", "name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "id,name,categoryid,supplierid,image,price,quantity,information,datecreate,description")] Product product,HttpPostedFileBase upload)
        {
            Category category = db.Categories.SingleOrDefault(c=>c.id==product.categoryid);
            if (ModelState.IsValid)
            {
                if(upload != null && upload.ContentLength>0)
                {
                    string filename = Path.GetFileName(upload.FileName);
                    string path = Path.Combine(Server.MapPath("~/image"),filename);
                    upload.SaveAs(path);
                    product.image = "/image/" + filename;
                }
                else
                {
                    product.image = "/image/noproduct.PNG";
                }
                product.datecreate = DateTime.Now;
                category.status = "Còn hàng";
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.authorid = new SelectList(db.Authors, "id", "name", product.authorid);
            ViewBag.categoryid = new SelectList(db.Categories, "id", "name", product.categoryid);
            ViewBag.supplierid = new SelectList(db.Suppliers, "id", "name", product.supplierid);
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            //ViewBag.authorid = new SelectList(db.Authors, "id", "name", product.authorid);
            ViewBag.categoryid = new SelectList(db.Categories, "id", "name", product.categoryid);
            ViewBag.supplierid = new SelectList(db.Suppliers, "id", "name", product.supplierid);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        
        public ActionResult Edit([Bind(Include = "id,name,categoryid,supplierid,image,price,quantity,information,datecreate,description")] Product product,HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                //var currentProduct = db.Products.Find(product.id);

                if (upload != null && upload.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(upload.FileName);
                    string path = Path.Combine(Server.MapPath("~/image"), fileName);
                    upload.SaveAs(path);
                    product.image = "/image/" + fileName;
                }


                //currentProduct.name = product.name;
                //currentProduct.supplierid = product.supplierid;
                //currentProduct.categoryid = product.categoryid;
                //// currentProduct.ProductImage = product.ProductImage;
                //currentProduct.price = product.price;
                //currentProduct.quantity = product.quantity;
                //currentProduct.information = product.information;
                //currentProduct.description = product.description;
                //currentProduct.datecreate = DateTime.Now;
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.authorid = new SelectList(db.Authors, "id", "name", product.authorid);
            ViewBag.categoryid = new SelectList(db.Categories, "id", "name", product.categoryid);
            ViewBag.supplierid = new SelectList(db.Suppliers, "id", "name", product.supplierid);
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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
    }
}
