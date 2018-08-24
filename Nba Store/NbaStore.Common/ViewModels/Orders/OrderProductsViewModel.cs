using System;
using System.Collections.Generic;
using System.Text;

namespace NbaStore.Common.ViewModels.Orders
{
    public class OrderProductsViewModel
    {
        public string ProductSize { get; set; }
        
        public decimal ProductPrice { get; set; }

        public string ProductTitle { get; set; }
        
        public string ProductPicture { get; set; }

        public int Quantity { get; set; }
    }
}
