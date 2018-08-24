using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NbaStore.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [MinLength(5)]
        public string Title { get; set; }

        [Required]
        [Url]
        public string PictureUrl { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 3)]
        public string Gender { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string Type { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string Brand { get; set; }

        [Required]
        [MinLength(10)]
        public string Description { get; set; }

        public string Size { get; set; }

        [Range(typeof(decimal), "0.0", "79228162514264337593543950335")]
        public decimal Discount { get; set; }

        [Range(typeof(decimal), "0.0", "79228162514264337593543950335")]
        public decimal Price { get; set; }

        public int TeamId { get; set; }

        public Team Team { get; set; }

        public List<Image> Images{ get; set; } = new List<Image>();
    }
}
