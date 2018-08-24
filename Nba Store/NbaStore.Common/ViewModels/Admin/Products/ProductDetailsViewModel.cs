using NbaStore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NbaStore.Common.ViewModels.Admin.Products
{
    public class ProductDetailsViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }
        
        public string PictureUrl { get; set; }
        
        public string Gender { get; set; }

        public string Type { get; set; }
        
        public string Brand { get; set; }
        
        public string Description { get; set; }

        public string Size { get; set; }
        
        public decimal Discount { get; set; }
        
        public decimal Price { get; set; }

        public int TeamId { get; set; }

        public Team Team { get; set; }
        
        public ICollection<Image> Images { get; set; }
    }
}
