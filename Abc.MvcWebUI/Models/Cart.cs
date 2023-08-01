using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Abc.MvcWebUI.Entity;

namespace Abc.MvcWebUI.Models
{
    // Cart, alışveriş sepetini temsil eden sınıf.
    // Bu sınıf, kullanıcının seçtiği ürünleri ve bu ürünlerin miktarlarını içeren bir alışveriş sepeti oluşturur ve yönetir.

    public class Cart
    {
        private List<CartLine> _cartlines = new List<CartLine>(); // Alışveriş sepetini temsil eden liste.

        public List<CartLine> CartLines // Alışveriş sepetindeki ürünleri ve miktarlarını tutan liste özelliği.
        {
            get { return _cartlines; }
        }

        // Ürün eklemek için kullanılan metot.
        public void AddProduckt(Product product, int quantity)
        {
            // Eğer ürün daha önce sepette yoksa, yeni bir CartLine oluşturarak ürünü ekler.
            // Eğer ürün zaten sepette varsa, ürün miktarını günceller.
            var line = _cartlines.FirstOrDefault(i => i.Product.Id == product.Id);
            if (line == null)
            {
                _cartlines.Add(new CartLine() { Product = product, Quantity = quantity });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        // Ürün silmek veya miktarını azaltmak için kullanılan metot.
        public void DeleteProduckt(Product product)
        {
            // Eğer ürünün miktarı 1'den büyükse, ürün miktarını azaltır.
            // Eğer ürünün miktarı 1 ise, ürünü sepette tamamen kaldırır.
            var line = _cartlines.FirstOrDefault(i => i.Product.Id == product.Id);
            if (line.Quantity > 1)
            {
                line.Quantity -= 1;
            }
            else
            {
                _cartlines.RemoveAll(i => i.Product.Id == product.Id);
            }
        }

        // Sepetteki ürünlerin toplam tutarını hesaplayan metot.
        public Double Total()
        {
            // Eğer sepette hiç ürün yoksa, toplam tutar 0 olarak döner.
            // Aksi halde, tüm ürünlerin fiyatları ile miktarları çarpılıp toplanarak toplam tutar elde edilir.
            if (_cartlines.Count == 0)
            {
                return 0;
            }
            else
            {
                return _cartlines.Sum(i => i.Product.Price * i.Quantity);
            }
        }

        // Sepeti temizlemek için kullanılan metot.
        public void Clear()
        {
            _cartlines.Clear();
        }
    }

    // CartLine, alışveriş sepetindeki bir ürün ve miktarını temsil eden sınıf.
    public class CartLine
    {
        public Product Product { get; set; } // Ürün nesnesi.
        public int Quantity { get; set; } // Ürün miktarı.
    }
}
