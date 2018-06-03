using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebServer.Application.Data;
using WebServer.Application.Data.Models;

namespace WebServer.Application.Models
{
    public class ProductService
    {
        private string name;
        private decimal price;
        private string imageUrl;
        
        public void SaveInDb(string name,decimal price,string imageUrl)
        {
            using (var db = new ByTheCakeDbContext())
            {
                var product = new Product();
                product.Name = name;
                product.Price = price;
                product.ImageUrl = imageUrl;

                db.Products.Add(product);
                db.SaveChanges();
            } 
        }

        public Product FindById(int id)
        {
            using (var db = new ByTheCakeDbContext())
            {
                var product = db.Products.SingleOrDefault(x => x.Id == id);

                return product;
            }
        }

        public List<Product> FindByName(string name)
        {
            using (var db = new ByTheCakeDbContext())
            {
                var products = db.Products.Where(x => x.Name.ToLower().Contains(name.ToLower())).ToList();

                return products;
            }
        }
    }
}
