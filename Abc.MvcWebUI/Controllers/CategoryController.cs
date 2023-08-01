using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Abc.MvcWebUI.Entity;

namespace Abc.MvcWebUI.Controllers
{
    // Controller sınıfına sadece "admin" rolündeki kullanıcılar erişebilir.
    [Authorize(Roles = "admin")]
    public class CategoryController : Controller
    {
        // Entity Framework ile veritabanı işlemleri yapmak için 
        private DataContext db = new DataContext();

        // GET: Category
        // Tüm kategorilerin listesini görüntüler.
        public ActionResult Index()
        {
            // Tüm kategorileri veritabanından çeker ve Index view'ine gönderir.
            return View(db.Categories.ToList());
        }

        // GET: Category/Details/5
        // Belirli bir kategorinin detaylarını görüntüler.
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // İlgili id'ye sahip kategoriyi veritabanından bulur.
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }

            // Kategoriyi Details view'ine gönderir.
            return View(category);
        }

        // GET: Category/Create
        // Yeni bir kategori oluşturmak için kullanılan sayfayı görüntüler.
        public ActionResult Create()
        {
            return View();
        }

        // POST: Category/Create
        // Yeni bir kategori oluşturmak için kullanılan HTTP POST işlemi.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description")] Category category)
        {
            if (ModelState.IsValid)
            {
                // Model doğrulaması geçerliyse yeni kategoriyi veritabanına ekler.
                db.Categories.Add(category);
                db.SaveChanges();

                // Kategoriler sayfasına yönlendirir.
                return RedirectToAction("Index");
            }

            // Model doğrulaması geçersizse Create view'ine geri döner.
            return View(category);
        }

        // GET: Category/Edit/5
        // Varolan bir kategoriyi düzenlemek için kullanılan sayfayı görüntüler.
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // İlgili id'ye sahip kategoriyi veritabanından bulur.
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }

            // Kategoriyi Edit view'ine gönderir.
            return View(category);
        }

        // POST: Category/Edit/5
        // Varolan bir kategoriyi düzenlemek için kullanılan HTTP POST işlemi.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description")] Category category)
        {
            if (ModelState.IsValid)
            {
                // Model doğrulaması geçerliyse kategoriyi veritabanında günceller.
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();

                // Kategoriler sayfasına yönlendirir.
                return RedirectToAction("Index");
            }

            // Model doğrulaması geçersizse Edit view'ine geri döner.
            return View(category);
        }

        // GET: Category/Delete/5
        // Varolan bir kategoriyi silmek için kullanılan sayfayı görüntüler.
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // İlgili id'ye sahip kategoriyi veritabanından bulur.
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }

            // Kategoriyi Delete view'ine gönderir.
            return View(category);
        }

        // POST: Category/Delete/5
        // Varolan bir kategoriyi silmek için kullanılan HTTP POST işlemi.
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            // İlgili id'ye sahip kategoriyi veritabanından bulur ve siler.
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
            db.SaveChanges();

            // Kategoriler sayfasına yönlendirir.
            return RedirectToAction("Index");
        }

        // Controller sınıfı kapatıldığında, DataContext'i serbest bırakır.
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
