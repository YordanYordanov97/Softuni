using NbaStore.Common.ViewModels.Admin.Teams;
using NbaStore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NbaStore.Common.ViewModels
{
    public class TeamProductsViewModel
    {
        public IEnumerable<Team> Teams { get; set; }

        public TeamDetailsViewModel TeamWithProducts{ get; set; }

        public PagesViewModel Page { get; set; }
    }
}
