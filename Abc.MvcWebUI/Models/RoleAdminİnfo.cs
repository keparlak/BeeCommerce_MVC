using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Abc.MvcWebUI.Models
{
    // RoleAdminInfo, yönetici tarafından kullanıcı rollerini düzenlemek için kullanılan model sınıfıdır.
    // Bu model, kullanıcıya rolleri eklemek ve rolleri silmek için kullanılacak verileri taşır.

    public class RoleAdminInfo
    {
        public string Id { get; set; } // Rolün kimlik bilgisini temsil eden özellik.

        public string Name { get; set; } // Rolün adını temsil eden özellik.

        public string[] AddToIds { get; set; } // Rollerin eklenmesi için kullanılacak kimlik dizisi.

        public string[] DeleteToIds { get; set; } // Rollerin silinmesi için kullanılacak kimlik dizisi.

        // Bu model, yönetici tarafından kullanıcı rollerini düzenlemek için kullanılır.
        // Kullanıcıya rolleri eklemek ve rolleri silmek için kullanılacak verileri taşır.
        // İlgili rollerin kimlik bilgileri ve adları bu modelde yer alır.
        // Ayrıca, rollerin eklenmesi ve silinmesi için kullanılacak kimlik dizileri de bulunur.
    }
}
