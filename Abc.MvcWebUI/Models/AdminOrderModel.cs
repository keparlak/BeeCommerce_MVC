using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Abc.MvcWebUI.Entity;

namespace Abc.MvcWebUI.Models
{
    // AdminOrderModel, yönetici tarafından görüntülemek için kullanılan sipariş bilgilerini içeren bir model sınıfıdır.
    // Bu sınıf, siparişlerin listesi ve detayları gibi yönetici arayüzünde siparişlerin görüntülenmesi ve yönetilmesi için kullanılır.

    public class AdminOrderModel
    {
        public int Id { get; set; } // Siparişin benzersiz kimliği.
        public int Count { get; set; } // Siparişteki ürün sayısı.
        public string OrderNumber { get; set; } // Sipariş numarası.
        public double Total { get; set; } // Siparişin toplam tutarı.
        public string UserName { get; set; } // Siparişi veren kullanıcının adı.
        public DateTime DateTime { get; set; } // Siparişin oluşturulduğu tarih ve saat.
        public EnumOrderState OrderState { get; set; } // Siparişin durumu (EnumOrderState tipinde).

        // EnumOrderState, siparişin durumunu temsil eden bir numaralandırma (enum) türüdür.
        // Sipariş durumları "Bekleniyor" ve "Tamamlandı" olarak iki değere sahiptir.
        // Örneğin, siparişin alındığı ancak henüz tamamlanmadığı durum "Bekleniyor", tamamlandığında "Tamamlandı" olacaktır.
        // Bu numaralandırma, siparişlerin yönetilmesi ve durumlarının anlaşılması için kullanılır.
    }
}
