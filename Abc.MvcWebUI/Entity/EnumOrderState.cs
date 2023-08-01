using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Abc.MvcWebUI.Entity
{
    // EnumOrderState adında bir enum (numaralandırma) tanımlanmıştır.
    // Bu enum, sipariş durumlarını temsil eder.

    public enum EnumOrderState
    {
        // Enum değerleri (sipariş durumları) belirtilmiştir.

        // "Bekleniyor" değeri, bir siparişin henüz tamamlanmamış olduğunu belirtir.
        Bekleniyor,

        // "Tamamlandı" değeri, bir siparişin tamamlandığını belirtir.
        Tamamlandı
    }
}
