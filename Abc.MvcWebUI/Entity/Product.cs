using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Abc.MvcWebUI.Entity
{
    // Product sınıfı, ürünleri temsil eder.
    public class Product
    {
        // Ürün ID'si - Her ürün için benzersiz bir tanımlayıcıdır.
        public int Id { get; set; }

        // Ürün adı - Ürünün ismini temsil eder.
        [DisplayName("Urun ismi")]
        public string Name { get; set; }

        // Ürün açıklaması - Ürünün açıklamasını temsil eder.
        [DisplayName("Urun acıklaması")]
        public string Description { get; set; }

        // Ürün resmi - Ürünü temsil eden resmin dosya adını temsil eder.
        public string Image { get; set; }

        // Ürün fiyatı - Ürünün birim fiyatını temsil eder.
        public double Price { get; set; }

        // Ürün stoku - Ürünün mevcut stok miktarını temsil eder.
        public int Stock { get; set; }

        // Ana Sayfa'da Göster - Ürünün ana sayfada gösterilip gösterilmeyeceğini temsil eder.
        public bool IsHome { get; set; }

        // Onay Durumu - Ürünün onaylanıp onaylanmadığını temsil eder.
        public bool IsApproved { get; set; }

        // Kategori ID'si - Bu ürünün bağlı olduğu kategorinin ID'sini temsil eder.
        public int CategoryId { get; set; }

        // Kategori - Bu ürünün bağlı olduğu kategoriyi temsil eden Category nesnesi.
        public Category Category { get; set; }

        // Yorumlar - Bu ürüne yapılan yorumları temsil eden bir Comment listesi.
        public List<Comment> Comments { get; set; }
    }
}
