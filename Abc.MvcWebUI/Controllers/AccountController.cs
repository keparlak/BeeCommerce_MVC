using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Abc.MvcWebUI.Entity;
using Abc.MvcWebUI.Identity;
using Abc.MvcWebUI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;

namespace Abc.MvcWebUI.Controllers
{
    public class AccountController : Controller
    {
        // Veritabanına erişim için DataContext sınıfını tanımlıyoruz.
        private DataContext db = new DataContext();

        // Asp.Net Identity kullanarak kullanıcı yönetimi işlemleri için UserManager ve RoleManager nesnelerini tanımlıyoruz.
        private UserManager<ApplicationUser> userManager;
        private RoleManager<ApplicationRole> roleManager;

        public AccountController()
        {
            // UserManager ve RoleManager nesnelerini oluşturuyoruz.
            // IdentityDataContext, kullanıcı ve rol verilerini saklamak için kullanılan veritabanı bağlantısını temsil eder.
            userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new IdentityDataContext()));
            roleManager = new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>(new IdentityDataContext()));
        }

        // Kullanıcının sipariş geçmişini görüntülemek için kullanılan Index action metodu.
        [Authorize]
        public ActionResult Index()
        {
            // Kullanıcının adını alıyoruz.
            var user = User.Identity.Name;
            // Kullanıcının sipariş geçmişini veritabanından çekiyoruz ve UserOrderModel sınıfını kullanarak düzenliyoruz.
            var order = db.Orders.Where(i => i.UserName == user).Select(i => new UserOrderModel()
            {
                Id = i.Id,
                Total = i.Total,
                UserName = i.UserName,
                DateTime = i.DateTime,
                OrderState = i.OrderState,
                OrderNumber = i.OrderNumber
            }).OrderByDescending(i => i.DateTime).ToList();

            return View(order);
        }

        // Kullanıcının detaylı sipariş bilgilerini görüntülemek için kullanılan Details action metodu.
        [Authorize]
        public ActionResult Details(int Id)
        {
            // Sipariş ID'sine göre sipariş detaylarını çekiyoruz ve OrderDetailsModel sınıfı ile düzenliyoruz.
            var product = db.Orders.Where(i => i.Id == Id)
                .Select(i => new OrderDetailsModel()
                {
                    OrderId = i.Id,
                    OrderNumber = i.OrderNumber,
                    Total = i.Total,
                    DateTime = i.DateTime,
                    OrderState = i.OrderState,
                    AdresBasligi = i.AdresBasligi,
                    Adres = i.Adres,
                    Mahalle = i.Mahalle,
                    Sehir = i.Sehir,
                    Semt = i.Semt,
                    PostaKodu = i.PostaKodu,
                    OrderLines = i.OrderLines.Select(a => new OrderLineModel()
                    {
                        Quantity = a.Quantity,
                        Price = a.Price,
                        Images = a.Product.Image,
                        ProductId = a.ProductId,
                        ProductName = a.Product.Name
                    }).ToList()

                }).FirstOrDefault();

            return View(product);
        }

        // Kullanıcı kayıt formunu görüntülemek için kullanılan Register action metodu.
        public ActionResult Register()
        {
            return View();
        }

        // Kullanıcı kayıt işlemini gerçekleştirmek için kullanılan Register action metodu (Post verisi alınır).
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Register model)
        {
            if (ModelState.IsValid)
            {
                // Yeni bir ApplicationUser nesnesi oluşturuyoruz ve model üzerinden gelen verilerle dolduruyoruz.
                var user = new ApplicationUser();
                user.Name = model.Name;
                user.SurName = model.SurName;
                user.Email = model.Email;
                user.UserName = model.UserName;
                // Kullanıcıyı Asp.Net Identity kullanarak veritabanına ekliyoruz.
                var result = userManager.Create(user, model.Password);
                if (result.Succeeded)
                {
                    // Kullanıcı başarıyla oluşturulduysa ve rol "user" varsa, kullanıcıya "user" rolü atanır.
                    if (roleManager.RoleExists("user"))
                    {
                        userManager.AddToRole(user.Id, "user");
                    }

                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    ModelState.AddModelError("RegisterUserError", "Kullanıcı oluşturulurken hata oluştu.");
                }
            }
            return View(model);
        }

        // Kullanıcı giriş formunu görüntülemek için kullanılan Login action metodu.
        public ActionResult Login(string ReturnUrl)
        {
            if (Request.IsAuthenticated)
            {
                // Eğer kullanıcı zaten oturum açmışsa, "Yetkisiz Giriş" hatası gösterilir.
                return View("Error", new string[] { "Yetkisiz Giriş" });
            }

            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        // Kullanıcı giriş işlemini gerçekleştirmek için kullanılan Login action metodu (Post verisi alınır).
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login model, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                // Giriş yapılmak istenen kullanıcı adı ve şifreyle Asp.Net Identity üzerinden kullanıcıyı buluyoruz.
                var user = userManager.Find(model.UserName, model.Password);
                if (user != null)
                {
                    var authManager = HttpContext.GetOwinContext().Authentication;
                    // Kullanıcının kimlik bilgilerini oluşturuyoruz.
                    var identity = userManager.CreateIdentity(user, "ApplicationCookie");
                    var authProperties = new AuthenticationProperties()
                    {
                        IsPersistent = model.RememberMe
                    };

                    authManager.SignOut(); // Eski kimlik bilgileri silinir.
                    authManager.SignIn(authProperties, identity); // Yeni kimlik bilgileriyle giriş yapılır.

                    // Kullanıcının rolü "admin" ise, yönetim paneline yönlendirilir, değilse ana sayfaya yönlendirilir.
                    if (userManager.IsInRole(user.Id, "admin"))
                    {
                        return Redirect(string.IsNullOrEmpty(ReturnUrl) ? "/AdminRole/Index" : ReturnUrl);
                    }
                    else
                    {
                        return Redirect(string.IsNullOrEmpty(ReturnUrl) ? "/Home/Index" : ReturnUrl);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Kullanıcı adı veya parola yanlış.");
                }
            }

            ViewBag.ReturnUrl = ReturnUrl;
            return View(model);
        }

        // Kullanıcı çıkış işlemi için kullanılan Logout action metodu.
        public ActionResult Logout()
        {
            var authManager = HttpContext.GetOwinContext().Authentication;
            authManager.SignOut(); // Kullanıcı çıkış yapar.

            return RedirectToAction("Index", "Home");
        }

        public ActionResult ReturnToHome()
        {
            var authManager = HttpContext.GetOwinContext().Authentication;
             // Kullanıcı çıkış yapar.

            return RedirectToAction("Index", "Home");
        }


        public async Task<ActionResult> UserDetails()
        {
            var userId = User.Identity.GetUserId();
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                // Kullanıcı bulunamadı durumunu işle
            }

            return View(user);
        }


    }
}
