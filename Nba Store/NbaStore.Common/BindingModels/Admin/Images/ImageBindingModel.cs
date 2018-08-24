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
        [Display(Name = "Image Url")]
        public string Url { get; set; }
    }
}
