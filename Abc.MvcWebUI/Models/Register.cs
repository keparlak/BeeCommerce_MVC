using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Abc.MvcWebUI.Models
{
    // Register, kullanıcı kaydı için kullanılan model sınıfıdır.
    // Bu model, kullanıcının kaydolma işlemi sırasında girilen bilgileri taşır.

    public class Register
    {
        [Required]
        [DisplayName("İsim")]
        public string Name { get; set; } // Kullanıcının adını temsil eden özellik.

        [Required]
        [DisplayName("Soyisim")]
        public string SurName { get; set; } // Kullanıcının soyadını temsil eden özellik.

        [Required]
        [DisplayName("Kullanıcı Adı")]
        public string UserName { get; set; } // Kullanıcının kullanıcı adını temsil eden özellik.

        [Required]
        [DisplayName("E-mail")]
        public string Email { get; set; } // Kullanıcının e-posta adresini temsil eden özellik.

        [Required]
        [DisplayName("Parola")]
        public string Password { get; set; } // Kullanıcının parolasını temsil eden özellik.

        [Required]
        [DisplayName("Parola Tekrar")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string RePassword { get; set; } // Kullanıcının parolasını tekrar girmek için kullanılan özellik.

        // Bu model, kullanıcının kaydolma işlemi sırasında girilen bilgileri taşır.
        // Kullanıcı arayüzündeki kayıt formunda kullanılır ve kullanıcının adını, soyadını,
        // kullanıcı adını, e-posta adresini, parolasını ve parola tekrarını içerir.
    }
}
