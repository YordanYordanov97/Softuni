using NbaStore.Common.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NbaStore.Common.BindingModels.Admin.Images
{
    public class ImageBindingModel
    {
        [Required]
        [Url]
        [Display(Name = ValidationConstants.ImageUrl)]
        public string Url { get; set; }
    }
}
