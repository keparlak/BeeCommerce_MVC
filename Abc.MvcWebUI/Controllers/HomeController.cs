using Abc.MvcWebUI.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Abc.MvcWebUI.Models;

namespace Abc.MvcWebUI.Controllers
{
    public class HomeController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Home
        // Ana sayfayı oluşturan metot.
        public ActionResult Index()
        {
            // IsHome ve IsApproved alanları true olan ürünlerden 80 karaktere kadar kısa bilgileri alarak 
            // ProductModel listesi oluşturur ve bu listeyi ana sayfa görünümüne gönderir.

            var urunler = db.Products.Where(i => i.IsHome && i.IsApproved).Select(i => new ProductModel()
            {
                Id = i.Id,
                Image = i.Image,
                Name = i.Name,
                Stock = i.Stock,
                Description = i.Description.Substring(0, 80) + "...",
                Price = i.Price,
                CategoryId = i.CategoryId
            }).ToList();

            return View(urunler);
        }

        // GET: Home/Details/{id}
        // Ürün detay sayfasını oluşturan metot.
        public ActionResult Details(int id)
        {
            // Verilen "id" parametresine sahip ürünü veritabanından çeker ve ürün detay sayfasına gönderir.
            // Aynı zamanda, ürünün ilişkili yorumlarını ViewBag.Comments'e ekler.

            var product = db.Products.Where(i => i.Id == id).FirstOrDefault();
            ViewBag.Comments = db.Comments.Where(i => i.ProductId == id).ToList();
            return View(product);
        }

        // GET: Home/List/{id?}
        // Ürünleri kategoriye göre listeleyen metot.
        public ActionResult List(int? id)
        {
            // IsApproved alanı true olan ürünlerden 80 karaktere kadar kısa bilgileri alarak 
            // ProductModel listesi oluşturur ve bu listeyi filtrelenmiş olarak görünüme gönderir.

            var urunler = db.Products.Where(a => a.IsApproved).Select(a => new ProductModel()
            {
                Id = a.Id,
                Image = a.Image,
                Name = a.Name,
                Stock = a.Stock,
                Description = a.Description.Substring(0, 80) + "...",
                Price = a.Price,
                CategoryId = a.CategoryId
            }).AsQueryable();

            if (id == null)
            {
                // Eğer "id" parametresi verilmemişse, tüm ürünleri listeleyerek görünüme gönderir.
                return View(urunler.ToList());
            }
            else
            {
                // Eğer "id" parametresi verilmişse, o kategoriye ait ürünleri filtreler ve görünüme gönderir.
                urunler = urunler.Where(i => i.CategoryId == id);
                return View(urunler.ToList());
            }
        }

        // [ChildActionOnly]
        // PartialViewResult _GetCategories()
        // Kategori listesini parça görünüm olarak döndüren metot.
        [ChildActionOnly]
        public PartialViewResult _GetCategories()
        {
            // Tüm kategorileri çeker ve CategoryModel listesi olarak parça görünüme gönderir.
            var kategoriler = db.Categories.Select(i => new CategoryModel()
            {
                Name = i.Name,
                CategoryCount = i.Products.Count(),
                Id = i.Id
            }).ToList();
            return PartialView("_GetCategories", kategoriler);
        }
    }
}
