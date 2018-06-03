using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using WebServer.Application.Data;
using WebServer.Application.Data.Models;

namespace WebServer.Application.Models
{
    public class Cart
    {
        private Dictionary<int, List<Product>> Products { get; set; }
        private int productCount;
        public Cart()
        {
            this.Products = new Dictionary<int, List<Product>>();
        }

        public void AddProduct(int productId)
        {
            using (var db = new ByTheCakeDbContext())
            {
                var product = db.Products.SingleOrDefault(x => x.Id == productId);

                if (!this.Products.ContainsKey(product.Id))
                {
                    this.Products.Add(product.Id, new List<Product>());
                }
                this.Products[product.Id].Add(product);
                productCount++;
            }
        }

        public int GetProductsCount()
        {
            return productCount;
        }

        public void Remove(int cakeId)
        {
            if (this.Products[cakeId].Count >0)
            {
                this.Products[cakeId].RemoveAt(0);
                this.productCount--;
            }
               
            if (this.Products[cakeId].Count == 0)
            {
                this.Products.Remove(cakeId);
            }
           
        }
        
        public void Clear()
        {
            this.Products.Clear();
        }

        public Dictionary<int, List<Product>> GetAllProducts()
        {
            return this.Products;
        }

        public decimal CalculateTotalCostPrice()
        {
            var totalPrice = 0.00M;

            foreach(var kvp in this.Products)
            {
                var productPrice = kvp.Value.Sum(x=>x.Price);
                totalPrice += productPrice;
            }

            return totalPrice;
        }

    }
}
