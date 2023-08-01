using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Abc.MvcWebUI.Identity
{
    // IdentityInitializer, CreateDatabaseIfNotExists<IdentityDataContext> sınıfından miras alarak kimlik veritabanı başlatıcı sınıfıdır.
    // Bu sınıf, veritabanı oluşturulduğunda veya değiştirildiğinde varsayılan kullanıcı rollerini ve kimlik bilgilerini eklemek için kullanılır.

    public class IdentityInitializer : CreateDatabaseIfNotExists<IdentityDataContext>
    {
        // Seed metodu, kimlik veritabanına varsayılan verileri eklemek için kullanılır.
        protected override void Seed(IdentityDataContext context)
        {
            // RoleManager ve UserManager sınıflarını tanımlar ve kimlik veritabanı bağlamı ile ilişkilendirir.
            RoleManager<ApplicationRole> roleManager;
            roleManager = new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>(context));
            UserManager<ApplicationUser> userManager;
            userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            // "admin" rolünü oluşturur ve veritabanına ekler (eğer zaten varsa eklenmez).
            if (!context.Roles.Any(i => i.Name == "admin"))
            {
                roleManager.Create(new ApplicationRole()
                {
                    Description = "yönetici rolü",
                    Name = "admin"
                });
            }

            // "user" rolünü oluşturur ve veritabanına ekler (eğer zaten varsa eklenmez).
            if (!context.Roles.Any(i => i.Name == "user"))
            {
                roleManager.Create(new ApplicationRole()
                {
                    Description = "kullanıcı rolü",
                    Name = "user"
                });
            }
            base.Seed(context);
        }
    }
}
