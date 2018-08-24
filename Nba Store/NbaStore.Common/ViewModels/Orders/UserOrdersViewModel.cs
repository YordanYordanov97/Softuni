using System;
using System.Collections.Generic;
using System.Text;

namespace NbaStore.Common.ViewModels.Orders
{
    public class UserOrdersViewModel
    {
        public int Id { get; set; }

        public DateTime DateOfOrdering { get; set; }

        public decimal TotalPrice { get; set; }

        public int OrderProductsCount { get; set; }
    }
}
