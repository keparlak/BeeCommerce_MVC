using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Abc.MvcWebUI.Entity;
using Abc.MvcWebUI.Models;

namespace Abc.MvcWebUI.Controllers
{
    public class CartController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Cart
        public ActionResult Index()
        {
            // Sepeti görüntülemek için kullanılan metot. GetCart() ile sepet verisini alır ve bu veriyi "Index" view'ine gönderir.
            return View(GetCart());
        }

        public ActionResult AddToCart(int Id)
        {
            // Ürünü sepete eklemek için kullanılan metot. Verilen "Id" değerine sahip ürünü veritabanından bulur ve sepete ekler.
            var product = db.Products.Where(i => i.Id == Id).FirstOrDefault();
            if (product != null)
            {
                GetCart().AddProduckt(product, 1);
            }
            return RedirectToAction("Index");
        }

        public ActionResult RemoveFromCart(int Id)
        {
            // Ürünü sepetten çıkarmak için kullanılan metot. Verilen "Id" değerine sahip ürünü sepetten çıkarır.
            var product = db.Products.Where(i => i.Id == Id).FirstOrDefault();
            if (product != null)
            {
                GetCart().DeleteProduckt(product);
            }
            return RedirectToAction("Index");
        }

        public Cart GetCart()
        {
            // Session'da tutulan "cart" değişkenini alır. Eğer "cart" yoksa, yeni bir "Cart" nesnesi oluşturur ve Session'a ekler.
            var cart = (Cart)Session["cart"];
            if (cart == null)
            {
                cart = new Cart();
                Session["cart"] = cart;
            }
            return cart;
        }

        [ChildActionOnly]
        public PartialViewResult Summary()
        {
            // Sepetin özetini gösteren "Summary" partial view'ini döndüren metot.
            return PartialView("_Summary", GetCart());
        }

        public ActionResult Checkout()
        {
            // Kullanıcının sipariş vermek için giriş yapmış olması gerektiğini kontrol eder. 
            // Eğer giriş yapılmamışsa "Error" view'ini gösterir ve hata mesajını gönderir.
            // Eğer giriş yapılmışsa, "ShoppingDetails" modelini kullanarak sipariş bilgilerini alır ve "Checkout" view'ine gönderir.
            if (!Request.IsAuthenticated)
            {
                return View("Error", new string[] { "Lütfen alışverişi tamamlamak için giriş yapınız" });
            }
            return View(new ShoppingDetails());
        }

        [HttpPost]
        public ActionResult Checkout(ShoppingDetails entity)
        {
            // Siparişin veritabanına kaydedilmesini sağlayan metot.
            var cart = GetCart();
            if (cart.CartLines.Count == 0)
            {
                ModelState.AddModelError("", "Sepette ürün yok");
            }

            if (ModelState.IsValid)
            {
                // Siparişin veritabanına kaydedilmesi işlemini "SaveOrder" metodu ile gerçekleştirir.
                // Ardından sepeti temizler ve "Complated" view'ine yönlendirir.
                SaveOrder(cart, entity);
                cart.Clear();
                return View("Completed");
            }
            return View(entity);
        }

        private void SaveOrder(Cart cart, ShoppingDetails entity)
        {
            // Sipariş veritabanına kaydedilirken kullanılacak işlemleri gerçekleştiren metot.
            var order = new Order();
            order.OrderNumber = "A" + (new Random().Next(11111, 99999)).ToString();
            order.Total = cart.Total();
            order.DateTime = DateTime.Now;
            order.UserName = entity.UserName;
            order.AdresBasligi = entity.AdresBasligi;
            order.Adres = entity.Adres;
            order.Sehir = entity.Sehir;
            order.Semt = entity.Semt;
            order.Mahalle = entity.Mahalle;
            order.PostaKodu = entity.PostaKodu;
            order.OrderState = EnumOrderState.Bekleniyor;
            order.OrderLines = new List<OrderLine>();

            foreach (var pr in cart.CartLines)
            {
                var orderline = new OrderLine();
                orderline.Quantity = pr.Quantity;
                orderline.Price = pr.Quantity * pr.Product.Price;
                orderline.ProductId = pr.Product.Id;
                order.OrderLines.Add(orderline);
            }

            db.Orders.Add(order);
            db.SaveChanges();
        }
    }
}
