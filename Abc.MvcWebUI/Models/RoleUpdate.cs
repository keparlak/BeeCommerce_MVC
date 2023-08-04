using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Abc.MvcWebUI.Identity;

namespace Abc.MvcWebUI.Models
{
    // RoleUpdate, rollerin güncellenmesi için kullanılan model sınıfıdır.
    // Bu model, bir rolün üyelerini ve üye olmayanları listelemek için kullanılır.

    public class RoleUpdate
    {
        public ApplicationRole Role { get; set; } // Güncellenecek rolü temsil eden özellik.

        public IEnumerable<ApplicationUser> Members { get; set; } // Rolün üyelerini temsil eden IEnumerable nesnesi.

        public IEnumerable<ApplicationUser> NonMembers { get; set; } // Rolde olmayan kullanıcıları temsil eden IEnumerable nesnesi.

        // RoleUpdate modeli, rollerin güncellenmesi için kullanılır.
        // Bu model, bir rolün üyelerini ve üye olmayanları listelemek için kullanılır.
        // Rolü temsil eden ApplicationRole nesnesi ve rolün üyelerini ve üye olmayanlarını temsil eden IEnumerable nesneleri içerir.
    }
}
