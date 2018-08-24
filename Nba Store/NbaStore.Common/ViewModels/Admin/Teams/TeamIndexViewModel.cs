using NbaStore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NbaStore.Common.ViewModels.Admin.Teams
{
    public class TeamIndexViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string LogoUrl { get; set; }

        public int ProductsCount { get; set; }
    }
}
