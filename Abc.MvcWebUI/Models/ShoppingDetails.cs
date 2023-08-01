using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Abc.MvcWebUI.Models
{
    // ShoppingDetails, alışveriş detaylarını temsil eden model sınıfıdır.
    // Bu model, kullanıcının alışveriş sırasında gerekli bilgileri girmesi için kullanılır.

    public class ShoppingDetails
    {
        public string UserName { get; set; } // Kullanıcının adını temsil eden özellik.

        [Required(ErrorMessage = "Lütfen adres başlığını giriniz.")] // Adres başlığı girişi zorunlu bir özellik olduğunu belirtir.
        public string AdresBasligi { get; set; } // Alışverişin teslim edileceği adres başlığını temsil eden özellik.

        [Required(ErrorMessage = "Lütfen adresi giriniz.")] // Adres girişi zorunlu bir özellik olduğunu belirtir.
        public string Adres { get; set; } // Alışverişin teslim edileceği adresi temsil eden özellik.

        [Required(ErrorMessage = "Lütfen şehir adını giriniz.")] // Şehir girişi zorunlu bir özellik olduğunu belirtir.
        public string Sehir { get; set; } // Alışverişin teslim edileceği şehri temsil eden özellik.

        [Required(ErrorMessage = "Lütfen semt adını giriniz.")] // Semt girişi zorunlu bir özellik olduğunu belirtir.
        public string Semt { get; set; } // Alışverişin teslim edileceği semti temsil eden özellik.

        [Required(ErrorMessage = "Lütfen mahalle adını giriniz.")] // Mahalle girişi zorunlu bir özellik olduğunu belirtir.
        public string Mahalle { get; set; } // Alışverişin teslim edileceği mahalleyi temsil eden özellik.

        [Required(ErrorMessage = "Lütfen posta kodunu giriniz.")] // Posta kodu girişi zorunlu bir özellik olduğunu belirtir.
        public string PostaKodu { get; set; } // Alışverişin teslim edileceği posta kodunu temsil eden özellik.

        // ShoppingDetails modeli, alışveriş detaylarını temsil eder ve kullanıcının alışveriş sırasında gerekli bilgileri girmesi için kullanılır.
        // Kullanıcı adını temsil eden UserName özelliği ve alışverişin teslim edileceği adres bilgilerini temsil eden özellikleri içerir.
        // Bu özelliklerin bazıları, girişin zorunlu olduğunu belirtmek için [Required] niteliği ile işaretlenmiştir.
    }
}
