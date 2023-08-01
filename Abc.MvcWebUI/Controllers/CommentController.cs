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
    // [Authorize(Roles ="admin")]
    // Bu Controller'ın sadece "admin" rolüne sahip kullanıcılar tarafından erişilebilmesini sağlayan bir attribute (özellik).
    // Ancak, bu kısım şu anlık yorum satırı olarak işaretlenmiş, yani kimse tarafından erişim engeli yok.

    public class CommentController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Comment
        public ActionResult Index(int? id)
        {
            // Bu metot, yorumları listeleyen bir sayfaya yönlendirir. Eğer "id" değeri verilmişse (bir ürünün id'si),
            // o ürüne ait yorumları listeleyen bir sayfa döner. Eğer "id" değeri yoksa, tüm yorumları listeleyen bir sayfa döner.

            if (id != null)
            {
                // "id" değeri verilmişse, o "id" değerine sahip ürüne ait yorumları veritabanından alır ve sayfaya gönderir.
                var comments = db.Comments.Where(i => i.ProductId == id);
                return View(comments.ToList());
            }
            else
            {
                // "id" değeri yoksa, tüm yorumları veritabanından alır ve sayfaya gönderir.
                var comments = db.Comments.Include(c => c.Product);
                return View(comments.ToList());
            }
        }

        // [AllowAnonymous]
        // Bu metotun yetkilendirme işlemlerini atlaması ve tüm kullanıcıların erişmesine izin verilmesini sağlayan bir attribute (özellik).
        // Şu anlık yorum satırı olarak işaretlenmiş, yani kimse tarafından erişim engeli yok.

        // GET: Comment/Create
        public ActionResult Create(int id)
        {
            // Yeni bir yorum oluşturulacak ürünün "id" değerini alır ve bu ürünü bir "ViewBag" ile sayfaya gönderir.
            // Kullanıcılar, bu sayfada yorumlarını oluşturabilir.

            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name");
            return View();
        }

        // POST: Comment/Create
        // Bu metot, kullanıcıların yorumları oluşturmak için verileri gönderdiği HTTP POST taleplerini işler.
        // Bu metot "ValidateAntiForgeryToken" attribute'ı ile güvenliğini sağlar.

        // [AllowAnonymous]
        // Bu metotun yetkilendirme işlemlerini atlaması ve tüm kullanıcıların erişmesine izin verilmesini sağlayan bir attribute (özellik).
        // Şu anlık yorum satırı olarak işaretlenmiş, yani kimse tarafından erişim engeli yok.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Surname,Description,ProductId")] Comment comment)
        {
            // Bu metot, oluşturulan yorumun veritabanına kaydedilmesini sağlar.
            // Eğer yorum geçerli ise (ModelState.IsValid == true), yorum veritabanına eklenir ve ilgili ürünün detay sayfasına yönlendirilir.
            // Eğer yorum geçerli değilse, kullanıcının tekrar yorum oluşturma sayfasına yönlendirilir ve hata mesajları gösterilir.

            if (ModelState.IsValid)
            {
                db.Comments.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Details", "", new { id = comment.ProductId });
            }

            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", comment.ProductId);
            return View(comment);
        }

        // GET: Comment/Delete/5
        public ActionResult Delete(int? id)
        {
            // Bu metot, bir yorumun silinmesini sağlayan sayfayı oluşturur.
            // Eğer "id" değeri verilmişse (bir yorumun id'si), o yorumun bilgilerini içeren bir sayfa döner.
            // Eğer "id" değeri yoksa, "BadRequest" (HTTP 400) hatası döner.

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Comment/Delete/5
        // Bu metot, kullanıcıların yorumları silmek için verileri gönderdiği HTTP POST taleplerini işler.
        // Bu metot "ValidateAntiForgeryToken" attribute'ı ile güvenliğini sağlar.

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            // Bu metot, silinecek yorumun veritabanından kaldırılmasını sağlar.
            // Yorum veritabanından silindikten sonra, yorumun bulunduğu ürünün detay sayfasına yönlendirilir.

            Comment comment = db.Comments.Find(id);
            db.Comments.Remove(comment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // Bu metot, Controller sınıfının tüm kaynaklarının temizlenmesini sağlar.
        // Özellikle DbContext gibi kaynaklar kapatılır ve sistem kaynakları geri verilir.
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
