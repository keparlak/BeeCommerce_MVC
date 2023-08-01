using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Abc.MvcWebUI.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Abc.MvcWebUI.Models;

namespace Abc.MvcWebUI.Controllers
{
    // Sadece "admin" rolüne sahip kullanıcıların bu controller'a erişebilmesi için [Authorize(Roles = "admin")] attribute'ünü kullanıyoruz.
    [Authorize(Roles = "admin")]
    public class AdminRoleController : Controller
    {
        private UserManager<ApplicationUser> userManager;
        private RoleManager<ApplicationRole> roleManager;

        public AdminRoleController()
        {
            // UserManager ve RoleManager nesnelerini oluşturuyoruz.
            // IdentityDataContext, kullanıcı ve rol verilerini saklamak için kullanılan veritabanı bağlantısını temsil eder.
            userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new IdentityDataContext()));
            roleManager = new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>(new IdentityDataContext()));
        }

        // GET: AdminRole
        // Rol listesini döndüren Index action metodu.
        public ActionResult Index()
        {
            return View(roleManager.Roles.ToList());
        }

        // Rol düzenleme sayfasını açan Edit action metodu.
        public ActionResult Edit(string id)
        {
            var role = roleManager.FindById(id);
            var member = new List<ApplicationUser>();
            var nonmember = new List<ApplicationUser>();

            foreach (var user in userManager.Users.ToList())
            {
                var list = userManager.IsInRole(user.Id, role.Name) ? member : nonmember;
                list.Add(user);
            }

            return View(new RoleUpdate()
            {
                Role = role,
                Members = member,
                NonMembers = nonmember
            });

        }

        // Rol düzenleme işlemini gerçekleştiren Edit action metodu (Post verisi alınır).
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RoleAdminInfo model)
        {
            IdentityResult result;
            if (ModelState.IsValid)
            {
                // Eklenecek kullanıcıları rolde olup olmadıklarına göre belirliyoruz.
                foreach (var userId in model.AddToIds ?? new string[] { })
                {
                    result = userManager.AddToRole(userId, model.Name);
                    if (!result.Succeeded)
                    {
                        // Başarısızlık durumunda "Eror" view'ine hata mesajı gönderiyoruz.
                        return View("Eror", new string[]
                        {
                            " hata oluştu."
                        });
                    }
                }

                // Çıkarılacak kullanıcıları rolde olup olmadıklarına göre belirliyoruz.
                foreach (var userId in model.DeleteToIds ?? new string[] { })
                {
                    result = userManager.RemoveFromRole(userId, model.Name);

                    if (!result.Succeeded)
                    {
                        // Başarısızlık durumunda "Eror" view'ine hata mesajı gönderiyoruz.
                        return View("Eror", new string[]
                        {
                            " hata oluştu."
                        });
                    }
                }

                return RedirectToAction("Index"); // Başarılı bir şekilde düzenleme işlemi tamamlandığında rol listesine yönlendiriyoruz.
            }
            else
            {
                // Model geçerli değilse, "Eror" view'ine hata mesajı gönderiyoruz.
                return View("Eror", new string[]
                {
                    " hata oluştu."
                });
            }
        }

        // Rolü silmek için kullanılan Delete action metodu.
        public ActionResult Delete(string id)
        {
            var user = userManager.FindById(id);
            var result = userManager.Delete(user);
            if (result.Succeeded)
            {
                return RedirectToAction("Index"); // Başarılı bir şekilde kullanıcı silindiğinde rol listesine yönlendiriyoruz.
            }
            else
            {
                // Başarısızlık durumunda "Eror" view'ine hata mesajı gönderiyoruz.
                return View("Eror", new string[] { "hata oluştu" });
            }
        }
    }
}
