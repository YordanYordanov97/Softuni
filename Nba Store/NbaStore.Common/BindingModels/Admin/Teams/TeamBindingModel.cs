using NbaStore.Common.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NbaStore.Common.BindingModels.Admin.Teams
{
    public class TeamBindingModel
    {
        [Required]
        [StringLength(30, ErrorMessage = ValidationConstants.ErrorMessageForMinAndMaxLength, MinimumLength = 5)]
        [Display(Name = ValidationConstants.Name)]
        public string Name { get; set; }

        [Required]
        [Url]
        [Display(Name = ValidationConstants.LogoUrl)]
        public string LogoUrl { get; set; }
    }
}
