using NbaStore.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NbaStore.Common.BindingModels.Admin.Products
{
    public class ProductBindingModel
    {
        [Required]
        [Url]
        [Display(Name = "Picture Url")]
        public string PictureUrl { get; set; }

        [Required]
        [MinLength(5)]
        public string Title { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.",MinimumLength = 3)]
        public string Gender { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.",MinimumLength = 3)]
        public string Type { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.",MinimumLength = 2)]
        public string Brand { get; set; }

        [Required]
        [MinLength(10)]
        public string Description { get; set; }

        public string Size { get; set; }

        [Range(typeof(decimal), "0.0", "79228162514264337593543950335")]
        public decimal Discount { get; set; }

        [Range(typeof(decimal), "0.0", "79228162514264337593543950335")]
        public decimal Price { get; set; }

        [Display(Name = "Team Name")]
        public int TeamId { get; set; }

        public List<Team> Teams { get; set; }

    }
}
