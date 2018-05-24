using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WebServer.Application.Models
{
    public class Cart
    {
        private List<string> Products { get; set; }

        public Cart()
        {
            this.Products = new List<string>();
        }

        public void AddProduct(string cakeId)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            string newPath = Path.GetFullPath(Path.Combine(currentDirectory, @"..\..\..\"));
            var fileLines = File.ReadAllLines(newPath + @".\Application\Data\database.csv");

            var product = string.Empty;
            foreach(var fileLine in fileLines)
            {
                var line = fileLine.Split(',');
                var id = line[0];

                if (id == cakeId)
                {
                    var cakeName = line[1];
                    var cakePrice = line[2];
                    product = $"{cakeName} ${cakePrice}";
                    break;
                }
            }
            
            this.Products.Add(product);
        }

        public int GetProductsCount()
        {
            return this.Products.Count;
        }

        public void Remove(string name)
        {
            this.Products.Remove(name);
        }

        public void Clear()
        {
            this.Products.Clear();
        }

        public string GetAllProductsAsHtml()
        {
            var sb = new StringBuilder();

            if (this.Products.Count == 0)
            {
                sb.AppendLine("<div>No items in your cart</div>");
            }
            else
            {
                foreach (var product in this.Products)
                {
                    var deleteForm = $"<form method=\"post\">" +
                            $"<p>{product}</p>" +
                            $"<input name=\"deleteCake\" value=\"{product}\"type=\"hidden\"  />" +
                        "<input type=\"submit\" value=\"Remove\" />" +
                        "</form>";
                    sb.AppendLine($"{deleteForm}");
                }
            }

            return sb.ToString().Trim();
        }

        public string CalculateTotalCostPrice()
        {
            var totalPrice = 0.00D;
            foreach(var product in this.Products)
            {
                var productParams = product.Split(new[] { " $" }, StringSplitOptions.RemoveEmptyEntries);
                var productPrice = double.Parse(productParams[1]);
                totalPrice += productPrice;
            }

            return $"<p>Total Cost: ${totalPrice:f2}</p>";
        }
        
    }
}
