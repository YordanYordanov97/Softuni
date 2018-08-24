using System;
using System.Collections.Generic;
using System.Text;

namespace NbaStore.Common.ViewModels.Admin.Products
{
    public class ProductIndexViewModel
    {
        public int Id { get; set; }

        public string PictureUrl { get; set; }

        public string Title { get; set; }
        
        public decimal Price { get; set; }

        public decimal Discount { get; set; }
    }
}
