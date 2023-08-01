using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Abc.MvcWebUI.Entity;

namespace Abc.MvcWebUI.Models
{
    // UserOrderModel, kullanıcının siparişlerini temsil eden model sınıfıdır.
    // Bu model, kullanıcının siparişlerinin listelenmesi ve görüntülenmesi için kullanılır.

    public class UserOrderModel
    {
        public int Id { get; set; } // Siparişin benzersiz kimliğini temsil eden özellik.

        public string OrderNumber { get; set; } // Sipariş numarasını temsil eden özellik.

        public double Total { get; set; } // Siparişin toplam tutarını temsil eden özellik.

        public string UserName { get; set; } // Kullanıcının adını temsil eden özellik.

        public DateTime DateTime { get; set; } // Siparişin tarih ve saati temsil eden özellik.

        public EnumOrderState OrderState { get; set; } // Siparişin durumunu temsil eden EnumOrderState tipindeki özellik.

        // UserOrderModel, kullanıcının siparişlerini temsil eder ve kullanıcının siparişlerinin listelenmesi ve görüntülenmesi için kullanılır.
        // Siparişin benzersiz kimliğini temsil eden Id özelliği ve sipariş numarasını temsil eden OrderNumber özelliği gibi bazı temel bilgileri içerir.
        // Siparişin toplam tutarını temsil eden Total özelliği ve siparişin tarih ve saati için DateTime özelliği de bulunmaktadır.
        // Ayrıca, siparişin durumunu temsil eden EnumOrderState tipindeki OrderState özelliği de yer alır.
    }
}
