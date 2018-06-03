using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebServer.Application.Data;
using WebServer.Application.Data.Models;

namespace WebServer.Application.Models
{
    public class OrderService
    {
        public void SendOrder(string username, Dictionary<int, List<Product>> products)
        {
            var productIds = new List<int>();

            foreach (var kvp in products)
            {
                productIds.Add(kvp.Key);
            }

            using (var db = new ByTheCakeDbContext())
            {
                var user = db.Users.SingleOrDefault(x => x.Username == username);
                var userId = user.Id;

                var order = new Order
                {
                    UserId = userId,
                    DateOfCreation = DateTime.UtcNow,
                    OrderProducts = productIds
                         .Select(id => new OrderProduct
                         {
                             ProductId = id
                         })
                         .ToList()
                };

                user.Orders.Add(order);

                db.Orders.Add(order);
                db.SaveChanges();

            }
        }

        public string GetDateOfCreation(int orderId)
        {
            using (var db = new ByTheCakeDbContext())
            {
                var order = db.Orders.SingleOrDefault(x => x.Id == orderId);

                return order.DateOfCreation.ToString();
            }
        }

        public List<Product> GetProductsByGivenOrderId(int orderId)
        {
            using (var db = new ByTheCakeDbContext())
            {
                var orderProducts = db.OrderProducts
                    .Where(x => x.OrderId == orderId)
                    .ToList();

                var products = new List<Product>();

                foreach(var orderProduct in orderProducts)
                {
                    var product = db.Products.SingleOrDefault(x => x.Id == orderProduct.ProductId);

                    products.Add(product);
                }

                return products;
            }
        }
    }
}
