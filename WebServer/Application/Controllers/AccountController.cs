namespace WebServer.Application.Controllers
{
    using WebServer.Application.Models;
    using WebServer.Application.Views;
    using WebServer.Server.Enums;
    using WebServer.Server.Http.Contracts;
    using WebServer.Server.Http.Response;
    using WebServer.Server.HTTP;

    public class AccountController:Controller
    {
        public IHttpResponse UserLogInGet()
        {
            this.ViewData["<!--unsuccessfulLogin-->"] = string.Empty;

            return new ViewResponse(HttpStatusCode.Ok, new HtmlView("login",this.ViewData));
        }

        public IHttpResponse UserLogInPost(IHttpRequest req)
        {
            var username = string.Empty;
            var password = string.Empty;
            if (req.FormData.ContainsKey("username") && req.FormData.ContainsKey("password"))
            {
                username = req.FormData["username"];
                password = req.FormData["password"];
            }

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                this.ViewData["<!--unsuccessfulLogin-->"] = "<div style=\"color:red\">Username and Password are required</div>";

                return new ViewResponse(HttpStatusCode.Ok, new HtmlView("login", this.ViewData));
            }
            else
            {
                var user = new User(username, password);
                req.Session.Add(SessionStore.CurrentUserKey, user);

                var cart = new Cart();
                req.Session.Add(SessionStore.CurrentCartKey, cart);

                return new RedirectResponse($"/");
            }
        }

        public IHttpResponse LogOut(IHttpRequest req)
        {
            req.Session.Clear();
            return new RedirectResponse($"/login");
        }
    }
}
