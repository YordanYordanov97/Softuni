using System;
using System.Collections.Generic;
using System.Text;

namespace NbaStore.Common.ViewModels.ShoppingCart
{
    public class ProductCartViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }
        
        public string PictureUrl { get; set; }

        public decimal Price { get; set; }

        public decimal Discount { get; set; }

        public string Size { get; set; }

        public int Quantity { get; set; }
    }
}
