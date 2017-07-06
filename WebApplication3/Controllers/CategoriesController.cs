using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WebApplication3.Models;
using PagedList;
namespace WebApplication3.Controllers
{
    public class CategoriesController : Controller
    {
        private pjt3hEntities db = new pjt3hEntities();

        // GET: Categories
        public ActionResult Index(string searchString,string sortorder,int? page,string currenFilter,Product product)
        {
            
            ViewBag.CurrentSort = sortorder;
            ViewBag.NameSort = string.IsNullOrEmpty(sortorder)?"name_desc":string.Empty;
            var category = from m in db.Categories
                           select (m);
            if(searchString!=null)
            {
                page = 1;
            }
            else
            {
                searchString = currenFilter;
            }
            switch (sortorder)
            {
                case "name_desc":
                    category = category.OrderByDescending(m => m.name);
                        break;
                default:
                    category = category.OrderBy(m => m.name);
                    break;
            }

            ViewBag.currentFilter = searchString;
            if (!string.IsNullOrEmpty(searchString))
            {
                category = category.Where(c => c.name.Contains(searchString));
            }
            var a = new SelectList(db.Categories, "id", "name",category.ToList());
            
            
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(category.ToPagedList(pageNumber,pageSize));
            // return View(db.Categories.ToList());
        }

        // GET: Categories/Details/5
        public ActionResult Details(int? id,Product product)
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

        // GET: Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,status")] Category category)
        {
            if (ModelState.IsValid)
            {
                string a = "Hết hàng";
                category.status = a.ToString();
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,status")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: Categories/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
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
