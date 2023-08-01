using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Abc.MvcWebUI.Identity
{
    // IdentityDataContext, IdentityDbContext<ApplicationUser> sınıfından miras alarak özel bir kimlik veritabanı bağlamı sınıfıdır.
    // AspNet.Identity.EntityFramework kütüphanesindeki IdentityDbContext sınıfı, kimlik verilerini yönetmek için kullanılır.
    // Bu sınıf, ApplicationUser sınıfıyla özelleştirilmiş kimlik veritabanını temsil eder.

    public class IdentityDataContext : IdentityDbContext<ApplicationUser>
    {
        // Parametresiz kurucu metot - Varsayılan olarak oluşturulacak nesneler için kullanılır.
        public IdentityDataContext()
            : base("DataConnection")
        {
            // base("DataConnection") ifadesi, temel sınıf olan IdentityDbContext'in parametreli kurucu metoduna "DataConnection" adlı bağlantı dizesini gönderir.
        }
    }
}
