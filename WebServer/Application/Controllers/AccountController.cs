namespace WebServer.Application.Controllers
{
    using WebServer.Application.Models;
    using WebServer.Application.Views.Account;
    using WebServer.Server.Enums;
    using WebServer.Server.Http.Contracts;
    using WebServer.Server.Http.Response;
    using WebServer.Server.HTTP;

    public class AccountController
    {
        public IHttpResponse UserLogInGet()
        {
            return new ViewResponse(HttpStatusCode.Ok, new LoginUserView(string.Empty));
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
                var invalidMessage = "<div style=\"color:red\">Username and Password are required</div>";
                return new ViewResponse(HttpStatusCode.Ok, new LoginUserView(invalidMessage));
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
            return new RedirectResponse($"/");
        }
    }
}
