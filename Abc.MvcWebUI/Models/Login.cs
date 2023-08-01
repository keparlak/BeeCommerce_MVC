using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Abc.MvcWebUI.Models
{
    // Login, kullanıcı girişi için kullanılacak model sınıfıdır.
    // Bu model, giriş sayfasında kullanılacak verileri taşır.

    public class Login
    {
        [Required] // Bu özellik, kullanıcı adının boş olmamasını zorunlu kılar.
        [DisplayName("Kullanıcı Adı")] // Bu özellik, ekranda "Kullanıcı Adı" olarak gösterilir.
        public string UserName { get; set; } // Kullanıcı adını temsil eden özellik.

        [Required] // Bu özellik, parolanın boş olmamasını zorunlu kılar.
        [DisplayName("Parola")] // Bu özellik, ekranda "Parola" olarak gösterilir.
        public string Password { get; set; } // Parolayı temsil eden özellik.

        [Required] // Bu özellik, "Beni Hatırla" seçeneğinin seçilmiş olmasını zorunlu kılar.
        [DisplayName("Beni Hatırla")] // Bu özellik, ekranda "Beni Hatırla" olarak gösterilir.
        public bool RememberMe { get; set; } // "Beni Hatırla" seçeneğini temsil eden özellik.
    }
}
