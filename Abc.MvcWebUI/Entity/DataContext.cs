using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace Abc.MvcWebUI.Entity
{
    public class DataContext : DbContext
    {
        // DataContext sınıfı, Entity Framework ile veritabanı işlemlerini yönetmek için kullanılan sınıftır.
        // DbContext sınıfını miras alarak veritabanı işlemlerini gerçekleştirir.

        public DataContext() : base("DataConnection")
        {
            // DataContext sınıfının yapıcı metodu (constructor).
            // DbContext sınıfının yapıcı metodunu çağırır ve veritabanı bağlantı adı "DataConnection" olarak belirtilir.
        }

        // DbSet, veritabanındaki tabloları temsil etmek için kullanılan koleksiyonlar sağlar.

        // Kategoriler tablosu için DbSet koleksiyonu.
        public DbSet<Category> Categories { get; set; }

        // Ürünler tablosu için DbSet koleksiyonu.
        public DbSet<Product> Products { get; set; }

        // Siparişler tablosu için DbSet koleksiyonu.
        public DbSet<Order> Orders { get; set; }

        // Sipariş Detayları (Satırı) tablosu için DbSet koleksiyonu.
        public DbSet<OrderLine> OrderLines { get; set; }

        // Yorumlar tablosu için DbSet koleksiyonu.
        public DbSet<Comment> Comments { get; set; }

    }
}
