using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Abc.MvcWebUI.Models
{
    // ProductModel, ürün bilgilerini taşıyan model sınıfıdır.
    // Bu model, ürünlerin listelenmesi ve gösterilmesi için kullanılacaktır.

    public class ProductModel
    {
        public int Id { get; set; } // Ürün ID'sini temsil eden özellik.
        public string Name { get; set; } // Ürün ismini temsil eden özellik.
        public string Description { get; set; } // Ürün açıklamasını temsil eden özellik.
        public string Image { get; set; } // Ürün resmini temsil eden özellik.
        public double Price { get; set; } // Ürün fiyatını temsil eden özellik.
        public int Stock { get; set; } // Ürün stok adedini temsil eden özellik.
        public bool IsHome { get; set; } // Ürünün anasayfada gösterilip gösterilmeyeceğini temsil eden özellik.
        public bool IsApproved { get; set; } // Ürünün onay durumunu temsil eden özellik.
        public int CategoryId { get; set; } // Ürünün ait olduğu kategori ID'sini temsil eden özellik.

        // Bu model, veritabanında bulunan "Product" tablosunun verilerini taşımak için kullanılır.
        // Ürünlerin listelenmesi ve gösterilmesi için kullanıcı arayüzü ile haberleşmeyi kolaylaştırır.
    }
}
