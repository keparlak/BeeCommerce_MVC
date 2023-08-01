using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Abc.MvcWebUI.Entity
{
    // Order sınıfı, siparişleri temsil eder.
    public class Order
    {
        // Sipariş ID'si - Her sipariş için benzersiz bir tanımlayıcıdır.
        public int Id { get; set; }

        // Sipariş numarası - Siparişin benzersiz numarasını temsil eder.
        public string OrderNumber { get; set; }

        // Toplam tutar - Siparişin toplam tutarını temsil eder.
        public double Total { get; set; }

        // Kullanıcı adı - Siparişi veren kullanıcının adını temsil eder.
        public string UserName { get; set; }

        // Sipariş durumu - Siparişin durumunu EnumOrderState tipinde belirtir.
        public EnumOrderState OrderState { get; set; }

        // Adres başlığı - Teslimat adresine verilecek başlık veya isim.
        public string AdresBasligi { get; set; }

        // Adres - Teslimat yapılacak adres.
        public string Adres { get; set; }

        // Şehir - Teslimat yapılacak şehir bilgisi.
        public string Sehir { get; set; }

        // Semt - Teslimat yapılacak semt bilgisi.
        public string Semt { get; set; }

        // Mahalle - Teslimat yapılacak mahalle bilgisi.
        public string Mahalle { get; set; }

        // Posta Kodu - Teslimat yapılacak bölgenin posta kodu.
        public string PostaKodu { get; set; }

        // Sipariş tarihi ve saati - Siparişin alındığı tarih ve saat bilgisini temsil eder.
        public DateTime DateTime { get; set; }

        // Sipariş Detayları - Bir siparişin içerdiği ürünleri temsil eden bir OrderLine listesi.
        public virtual List<OrderLine> OrderLines { get; set; }
    }

    // OrderLine sınıfı, bir siparişteki ürünlerin detaylarını temsil eder.
    public class OrderLine
    {
        // Sipariş Detay ID'si - Her sipariş detayı için benzersiz bir tanımlayıcıdır.
        public int Id { get; set; }

        // Sipariş ID'si - Bu sipariş detayının bağlı olduğu siparişin ID'sini temsil eder.
        public int OrderId { get; set; }

        // Sipariş - Bu sipariş detayının bağlı olduğu siparişi temsil eden Order nesnesi.
        public virtual Order Order { get; set; }

        // Ürün miktarı - Bu sipariş detayında yer alan ürünün miktarını temsil eder.
        public int Quantity { get; set; }

        // Ürün fiyatı - Bu sipariş detayında yer alan ürünün birim fiyatını temsil eder.
        public double Price { get; set; }

        // Ürün ID'si - Bu sipariş detayında yer alan ürünün ID'sini temsil eder.
        public int ProductId { get; set; }

        // Ürün - Bu sipariş detayında yer alan ürünü temsil eden Product nesnesi.
        public virtual Product Product { get; set; }
    }
}
