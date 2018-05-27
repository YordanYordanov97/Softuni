namespace WebServer.Application.Controllers
{
    using Server.Enums;
    using Server.Http.Contracts;
    using Server.Http.Response;
    using System;
    using WebServer.Application.Views;

    public class HomeController: Controller
    {
        public IHttpResponse Index()
        {
            var response = new ViewResponse(HttpStatusCode.Ok, new HtmlView(@"index",this.ViewData));

            return response;
        }
    }
}