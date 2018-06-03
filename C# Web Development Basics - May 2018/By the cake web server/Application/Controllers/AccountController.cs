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
        private readonly UserService user;

        public AccountController()
        {
            this.user = new UserService();
        }

        public IHttpResponse RegisterGet()
        {
            return new ViewResponse(HttpStatusCode.Ok, new HtmlView(@"account\register", this.ViewData));
        }

        public IHttpResponse RegisterPost(IHttpRequest req)
        {
            var username = string.Empty;
            var password = string.Empty;
            var confirmPassword = string.Empty;

            if(req.FormData.ContainsKey("username") && req.FormData.ContainsKey("password")
                && req.FormData.ContainsKey("confirm-password"))
            {
                username = req.FormData["username"];
                password = req.FormData["password"];
                confirmPassword = req.FormData["confirm-password"];
            }

            if(username.Length>20 || username.Length<3 || password.Length>100
                || password.Length<4)
            {
                this.ViewData["<!--ErrorMessage-->"] = "<div style=\"color:red\">Username, password or confirm password are invalid</div>";
            }
            else
            {
                if (password != confirmPassword)
                {
                    this.ViewData["<!--ErrorMessage-->"] = "<div style=\"color:red\">Confirm password is different</div>";
                }
                else
                {
                    this.user.Create(username, password);

                    if (!this.user.Success())
                    {
                        this.ViewData["<!--ErrorMessage-->"] = "<div style=\"color:red\">Username is already taken!</div>";
                    }
                    else
                    {
                        this.LoginUser(req, username);

                        return new RedirectResponse($"/");
                    }
                }
            }

            return new ViewResponse(HttpStatusCode.Ok, new HtmlView(@"account\register", this.ViewData));
        }

        public IHttpResponse UserLogInGet()
        {
            return new ViewResponse(HttpStatusCode.Ok, new HtmlView(@"account\login", this.ViewData));
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
            }
            else
            {
                if (user.Find(username, password))
                {
                    this.LoginUser(req, username);
                    return new RedirectResponse($"/");
                }
                else
                {
                    this.ViewData["<!--unsuccessfulLogin-->"] = "<div style=\"color:red\">Invalid Username or Passoword!</div>";
                }
            }

            return new ViewResponse(HttpStatusCode.Ok, new HtmlView(@"account\login", this.ViewData));
        }

        public IHttpResponse Profile(IHttpRequest req)
        {
            var username = req.Session.Get(SessionStore.CurrentUserKey).ToString();
            var profileInformation = user.GetProfile(username);

            var profileUsername = profileInformation[0];
            var profileRegisterOn = profileInformation[1];
            var profileOrdersCount = profileInformation[2];

            this.ViewData["<!--Name-->"] = $"<div>Name: {profileUsername}</div>";
            this.ViewData["<!--RegisterDate-->"] = $"<div>Registerd on: {profileRegisterOn}</div>";
            this.ViewData["<!--OrdersCount-->"] = $"<div>Orders Count: {profileOrdersCount}</div>";

            return new ViewResponse(HttpStatusCode.Ok, new HtmlView(@"account\profile", this.ViewData));
        }

        public IHttpResponse LogOut(IHttpRequest req)
        {
            req.Session.Clear();
            return new RedirectResponse($"/login");
        }

        private void LoginUser(IHttpRequest req, string username)
        {
            req.Session.Add(SessionStore.CurrentUserKey, username);
            req.Session.Add(SessionStore.CurrentCartKey, new Cart());
        }
    }
}
