using NbaStore.Common.Constants;
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
        [Display(Name = ValidationConstants.PictureUrl)]
        public string PictureUrl { get; set; }

        [Required]
        [MinLength(5)]
        public string Title { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = ValidationConstants.ErrorMessageForMinAndMaxLength, MinimumLength = 3)]
        public string Gender { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = ValidationConstants.ErrorMessageForMinAndMaxLength, MinimumLength = 3)]
        public string Type { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = ValidationConstants.ErrorMessageForMinAndMaxLength, MinimumLength = 2)]
        public string Brand { get; set; }

        [Required]
        [MinLength(10)]
        public string Description { get; set; }

        public string Size { get; set; }

        [Range(typeof(decimal), ValidationConstants.MinDecimal, ValidationConstants.MaxDecimal)]
        public decimal Discount { get; set; }

        [Range(typeof(decimal), ValidationConstants.MinDecimal, ValidationConstants.MaxDecimal)]
        public decimal Price { get; set; }

        [Display(Name = ValidationConstants.TeamName)]
        public int TeamId { get; set; }

        public List<Team> Teams { get; set; }

    }
}
