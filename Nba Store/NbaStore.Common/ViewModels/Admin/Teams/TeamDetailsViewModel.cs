using NbaStore.Common.ViewModels.Admin.Products;
using NbaStore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NbaStore.Common.ViewModels.Admin.Teams
{
    public class TeamDetailsViewModel
    {
        public string Name { get; set; }

        public string LogoUrl { get; set; }

        public IEnumerable<ProductIndexViewModel> Products { get; set; }
    }
}
