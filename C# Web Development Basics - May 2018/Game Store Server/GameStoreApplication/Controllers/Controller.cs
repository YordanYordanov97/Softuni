using System;
using System.Collections.Generic;
using System.Text;

namespace WebServer.GameStoreApplication.Controllers
{
    public abstract class Controller
    {
        public Dictionary<string, string> ViewData { get; set; }

        public Controller()
        {
            this.ViewData = new Dictionary<string, string>();
        }
    }
}
