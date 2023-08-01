using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Abc.MvcWebUI.Entity
{
    public class Category
    {
        // Kategori ID'si - Her kategori için benzersiz bir tanımlayıcıdır.
        public int Id { get; set; }

        // Kategori adı - Kategoriye verilecek adı temsil eder.
        [DisplayName("Kategori ismi")]
        [Required(ErrorMessage = "Lütfen kategori adını giriniz.")]
        public string Name { get; set; }

        // Kategori açıklaması - Kategorinin ne hakkında olduğunu açıklayan metnidir.
        [DisplayName("Kategori açıklaması")]
        [Required(ErrorMessage = "Lütfen kategori açıklaması giriniz.")]
        public string Description { get; set; }

        // Ürünler listesi - Kategorideki ürünleri temsil eden bir liste.
        public List<Product> Products { get; set; }
    }
}
