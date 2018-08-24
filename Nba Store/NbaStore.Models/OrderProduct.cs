using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NbaStore.Models
{
    public class OrderProduct
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public Order Order { get; set; }

        [Required]
        public string ProductSize { get; set; }

        [Range(typeof(decimal), "0.0", "79228162514264337593543950335")]
        public decimal ProductPrice { get; set; }

        public int ProductId { get; set; }
        
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string ProductTitle { get; set; }

        [Required]
        [Url]
        public string ProductPicture { get; set; }
        
        public int Quantity { get; set; }
    }
}
