using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Abc.MvcWebUI.Entity
{
    public class Comment
    {
        // Yorum ID'si - Her yorum için benzersiz bir tanımlayıcıdır.
        public int Id { get; set; }

        // Yorum yapan kişinin adı - Yorum yapan kullanıcının adını temsil eder.
        [Display(Name = "İsim")]
        [Required(ErrorMessage = "Lütfen ismi giriniz.")]
        public string Name { get; set; }

        // Yorum yapan kişinin soyadı - Yorum yapan kullanıcının soyadını temsil eder.
        [Display(Name = "Soyisim")]
        public string Surname { get; set; }

        // Yorum açıklaması - Kullanıcının ürünle ilgili yorumunu içerir.
        [Display(Name = "Açıklama")]
        [Required(ErrorMessage = "Lütfen açıklama giriniz.")]
        public string Description { get; set; }

        // Yoruma bağlı ürün ID'si - Yorumun hangi ürüne ait olduğunu belirtir.
        public int ProductId { get; set; }

        // Yorumun bağlı olduğu ürün nesnesi - Yorumun ait olduğu ürünü temsil eder.
        public Product Product { get; set; }
    }
}
