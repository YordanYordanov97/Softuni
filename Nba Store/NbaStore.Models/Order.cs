using System;
using System.Collections.Generic;
using System.Text;

namespace NbaStore.Models
{
    public class Order
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public DateTime DateOfOrdering { get; set; }

        public decimal TotalPrice { get; set; }

        public List<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
    }
}
