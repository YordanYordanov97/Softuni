using System;
using System.Collections.Generic;
using System.Text;

namespace NbaStore.Common.ViewModels
{
    public class PagesViewModel
    {
        public int CurrentPage { get; set; }

        public int MaxPage { get; set; }

        public string AreaName { get; set; }

        public string ControllerName { get; set; }

        public string ActionName { get; set; }

        public int RouteId { get; set; }
    }
}
