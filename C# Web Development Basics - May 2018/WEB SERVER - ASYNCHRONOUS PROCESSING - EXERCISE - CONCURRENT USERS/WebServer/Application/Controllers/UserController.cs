namespace WebServer.Application.Controllers
{
    using Server.Enums;
    using Server.Http.Contracts;
    using Server.Http.Response;
    using System;
    using WebServer.Application.Views;

    public class UserController : Controller
    {
        public IHttpResponse RegisterGet()
        {
            var response = new ViewResponse(HttpStatusCode.Ok, new HtmlView(@"register", this.ViewData));

            return response;
        }

        public IHttpResponse RegisterPost(IHttpRequest req)
        {
            var name = req.FormData["name"];

            return new RedirectResponse($"/user/{name}");
        }

        public IHttpResponse Details(IHttpRequest req)
        {
            var name = req.UrlParameters["name"];
            this.ViewData["<!--Replace-->"] = $"<div>Hello, {name}!</div>";

            var response = new ViewResponse(HttpStatusCode.Ok, new HtmlView(@"details", this.ViewData));

            return response;
        }
    }
}