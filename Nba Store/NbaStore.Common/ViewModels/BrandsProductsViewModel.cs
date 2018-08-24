using NbaStore.Common.ViewModels.Admin.Products;
using NbaStore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NbaStore.Common.ViewModels
{
    public class BrandsProductsViewModel
    {
        public IEnumerable<AllBrandsViewModel> Brands { get; set; }

        public IEnumerable<Team> Teams { get; set; }

        public IEnumerable<ProductIndexViewModel> Products { get; set; }

        public PagesViewModel Page { get; set; }

        public string Gender { get; set; }
    }
}
