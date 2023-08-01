using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Abc.MvcWebUI.Entity;

namespace Abc.MvcWebUI.Controllers
{
    [Authorize(Roles = "admin")]
    public class ProductController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Product
        public ActionResult Index()
        {
            // Tüm ürünleri veritabanından alır ve Category (Kategori) bilgilerini de dahil ederek "Index" view'ine gönderir.
            var products = db.Products.Include(p => p.Category);
            return View(products.ToList());
        }

        // GET: Product/Details/5
        public ActionResult Details(int? id)
        {
            // Belirli bir ürünün detaylarını görüntülemek için kullanılan metot.
            // "id" değeri verilen ürünün veritabanından detaylarını alır ve "Details" view'ine gönderir.
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

        // GET: Product/Create
        public ActionResult Create()
        {
            // Yeni bir ürün eklemek için kullanılan metot.
            // Kategori bilgilerini "ViewBag" ile "Create" view'ine gönderir ve ürünün eklenmesi için sayfa oluşturulur.
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name");
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,Price,Image,Stock,IsHome,IsApproved,CategoryId")] Product product, HttpPostedFileBase file)
        {
            // Yeni ürünün veritabanına eklenmesi için kullanılan metot.
            // Eğer ürün verileri geçerliyse, ürün resmi kontrolü yapılır ve uygunsa resim de eklenir.
            // Daha sonra ürün veritabanına eklenir ve "Index" sayfasına yönlendirilir.

            if (ModelState.IsValid)
            {
                if (file != null && file.ContentLength > 0)
                {
                    var extension = Path.GetExtension(file.FileName);
                    if (extension == ".jpg" || extension == ".png")
                    {
                        var folder = Server.MapPath("~/Theme/img");
                        var randomFilename = Path.GetRandomFileName();
                        var filename = Path.ChangeExtension(randomFilename, ".jpg");
                        var path = Path.Combine(folder, filename);
                        file.SaveAs(path);

                        product.Image = filename;
                    }
                    else
                    {
                        ModelState.AddModelError("", "Sadece .jpg veya .png formatındaki resimler yüklenebilir.");
                        ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", product.CategoryId);
                        return View(product);
                    }
                }

                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // GET: Home
        public ActionResult Upload()
        {
            // Ürün resimlerini yüklemek için kullanılan metot.
            // "Upload" view'ini döndürür ve resim yükleme formunu gösterir.
            return View();
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file, int id)
        {
            // Ürün resminin yüklendiği metot.
            // Eğer resim geçerliyse, resmi belirli bir klasöre kaydederek veritabanına ekler.

            if (file != null && file.ContentLength > 0)
            {
                var extension = Path.GetExtension(file.FileName);
                if (extension == ".jpg" || extension == ".png")
                {
                    var folder = Server.MapPath("~/Theme/img");
                    var randomFilename = Path.GetRandomFileName();
                    var filename = Path.ChangeExtension(randomFilename, ".jpg");
                    var path = Path.Combine(folder, filename);
                    file.SaveAs(path);

                    var product = db.Products.FirstOrDefault(i => i.Id == id);
                    product.Image = filename;
                    db.SaveChanges();
                }
                else
                {
                    ViewData["message"] = "Sadece .jpg veya .png formatındaki resimler yüklenebilir.";
                    return View();
                }
            }
            else
            {
                ViewData["message"] = "Bir dosya seçiniz.";
                return View();
            }

            return RedirectToAction("Index");
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int? id)
        {
            // Ürün düzenleme sayfasını oluşturmak için kullanılan metot.
            // Eğer "id" değeri verilmişse, ilgili ürünün veritabanından bilgilerini alır ve "Edit" view'ine gönderir.
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,Image,Price,Stock,IsHome,IsApproved,CategoryId")] Product product)
        {
            // Ürün düzenleme işlemleri için kullanılan metot.
            // Eğer ürün verileri geçerliyse, ürün veritabanında güncellenir ve "Index" sayfasına yönlendirilir.

            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int? id)
        {
            // Ürünün silme sayfasını oluşturmak için kullanılan metot.
            // Eğer "id" değeri verilmişse, ilgili ürünün veritabanından bilgilerini alır ve "Delete" view'ine gönderir.
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

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            // Ürünün silindiği metot.
            // Eğer "id" değeri verilmişse, ilgili ürünü veritabanından siler ve "Index" sayfasına yönlendirir.
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            // IDisposable arayüzünü uygulayan sınıflar için nesne kaynaklarını serbest bırakmak için kullanılır.
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
