namespace WebServer.Application.Controllers
{
    using System.Collections.Generic;

    public abstract class Controller
    {
        public Dictionary<string,string> ViewData { get; set; }

        public Controller()
        {
            this.ViewData = new Dictionary<string, string>();
        }
    }
}
