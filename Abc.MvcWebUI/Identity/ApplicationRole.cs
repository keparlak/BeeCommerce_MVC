using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Abc.MvcWebUI.Identity
{
    // ApplicationRole, IdentityRole sınıfından miras alarak özel bir rol sınıfıdır.
    // AspNet.Identity.EntityFramework kütüphanesindeki IdentityRole sınıfı, kullanıcı rollerini temsil eder.
    // Bu sınıf, ApplicationRole adı altında ek özellikler eklemek için kullanılır.

    public class ApplicationRole : IdentityRole
    {
        // Rol açıklaması - Bir rolü açıklamak için kullanılan özel bir özelliktir.
        // Örneğin, "admin" rolü için açıklama, "Yönetici rolü" olabilir.
        public string Description { get; set; }

        // Parametresiz kurucu metot - Varsayılan olarak oluşturulacak nesneler için kullanılır.
        public ApplicationRole()
        {
            // Boş kurucu metot, IdentityRole sınıfının kurucu metotlarını çağırır.
        }

        // Parametreli kurucu metot - Rol adı ve açıklamasını parametre olarak alır.
        public ApplicationRole(string roleName, string description)
            : base(roleName)
        {
            // IdentityRole sınıfının parametreli kurucu metotlarına rol adını gönderir.
            // Ek olarak, bu özel ApplicationRole sınıfının Description özelliğini de tanımlar.
            this.Description = description;
        }
    }
}
