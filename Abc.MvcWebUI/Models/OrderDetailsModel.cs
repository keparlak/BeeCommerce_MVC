using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Permissions;
using System.Web;
using Abc.MvcWebUI.Entity;

namespace Abc.MvcWebUI.Models
{
    // OrderDetailsModel, sipariş detayları için kullanılacak model sınıfıdır.
    // Bu model, sipariş detay sayfasında kullanılacak verileri taşır.

    public class OrderDetailsModel
    {
        public int OrderId { get; set; } // Sipariş ID'sini temsil eden özellik.
        public string OrderNumber { get; set; } // Sipariş numarasını temsil eden özellik.
        public double Total { get; set; } // Sipariş toplam tutarını temsil eden özellik.

        public string UserName { get; set; } // Kullanıcı adını temsil eden özellik.
        public EnumOrderState OrderState { get; set; } // Sipariş durumunu temsil eden özellik.
        public string AdresBasligi { get; set; } // Adres başlığını temsil eden özellik.

        public string Adres { get; set; } // Adresi temsil eden özellik.
        public string Sehir { get; set; } // Şehri temsil eden özellik.
        public string Semt { get; set; } // Semti temsil eden özellik.
        public string Mahalle { get; set; } // Mahalleyi temsil eden özellik.
        public string PostaKodu { get; set; } // Posta kodunu temsil eden özellik.
        public DateTime DateTime { get; set; } // Sipariş tarihini temsil eden özellik.
        public virtual List<OrderLineModel> OrderLines { get; set; } // Sipariş satırlarını içeren liste.

        // Burada "OrderLineModel" sınıfı kullanılmış; bu muhtemelen "Abc.MvcWebUI.Models.OrderLineModel" şeklinde düşünülmelidir.
    }

    // OrderLineModel, sipariş satırı detayları için kullanılacak model sınıfıdır.
    // Bu model, sipariş detay sayfasında her bir ürün satırı için kullanılacak verileri taşır.

    public class OrderLineModel
    {
        public int ProductId { get; set; } // Ürün ID'sini temsil eden özellik.
        public string ProductName { get; set; } // Ürün ismini temsil eden özellik.
        public string Images { get; set; } // Ürün resimlerini temsil eden özellik.
        public int Quantity { get; set; } // Ürün adedini temsil eden özellik.
        public double Price { get; set; } // Ürün birim fiyatını temsil eden özellik.

        // Burada "Images" özelliği, muhtemelen ürünün resimlerini içeren bir liste olmalıdır.
    }
}
