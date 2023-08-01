using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Abc.MvcWebUI.Identity
{
    // ApplicationUser, IdentityUser sınıfından miras alarak özel bir kullanıcı sınıfıdır.
    // AspNet.Identity.EntityFramework kütüphanesindeki IdentityUser sınıfı, kullanıcıları temsil eder.
    // Bu sınıf, ApplicationUser adı altında ek özellikler eklemek için kullanılır.

    public class ApplicationUser : IdentityUser
    {
        // Kullanıcı adı (Name) - Kullanıcının ismini temsil eder.
        public string Name { get; set; }

        // Kullanıcı soyadı (SurName) - Kullanıcının soyadını temsil eder.
        public string SurName { get; set; }
    }
}
