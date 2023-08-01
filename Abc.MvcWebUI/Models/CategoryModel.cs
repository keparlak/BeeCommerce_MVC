using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Abc.MvcWebUI.Models
{
    // CategoryModel, kategori bilgilerini ve kategoriye ait ürünleri içeren model sınıfıdır.
    // Bu model, kategori sayfasında kullanılacak verileri taşır.

    public class CategoryModel
    {
        public int Id { get; set; } // Kategori ID'sini temsil eden özellik.
        public string Name { get; set; } // Kategori ismini temsil eden özellik.
        public int CategoryCount { get; set; } // Kategoriye ait ürün sayısını temsil eden özellik.
        public List<ProductModel> Products { get; set; } // Kategoriye ait ürünleri içeren liste.

        // Burada "ProductModel" sınıfı kullanılmış; bu muhtemelen "Abc.MvcWebUI.Models.ProductModel" şeklinde düşünülmelidir.
    }
}
