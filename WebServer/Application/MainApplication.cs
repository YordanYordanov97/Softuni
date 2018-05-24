namespace WebServer.Application
{
    using Controllers;
    using Server.Contracts;
    using Server.Routing.Contracts;

    public class MainApplication : IApplication
    {
        public void Configure(IAppRouteConfig appRouteConfig)
        {
            appRouteConfig.Get(
               "/cart",
               req => new CartController().CartGet(req));

            appRouteConfig.Post(
               "/cart",
                   req => new CartController().CartPost(req));

            appRouteConfig.Get(
               "/successorder",
                   req => new CartController().SuccessOrder(req));

            appRouteConfig.Get(
               "/login",
               req => new AccountController().UserLogInGet());

            appRouteConfig.Post(
               "/login",
                   req => new AccountController().UserLogInPost(req));

            appRouteConfig.Get(
                "/testsession",
                req => new HomeController().SessionTest(req));

            appRouteConfig.Get(
                "/",
                req => new HomeController().Index(req));
            
            appRouteConfig.Post(
               "/greeting",
                   req => new HomeController().Greeting(req));

            appRouteConfig.Get(
               "/greeting",
               req => new HomeController().Greeting(req));

            appRouteConfig.Get(
               "/loginadmin",    
               req => new HomeController().LogInAdmin(req,"Get"));

            appRouteConfig.Post(
               "/loginadmin",
                   req => new HomeController().LogInAdmin(req,"Post"));

            appRouteConfig.Get(
                "/success",
                req => new UserController().SendEmail(req));

            appRouteConfig.Post(
                "/success",
                req => new UserController().SendEmail(req));

            appRouteConfig.Get(
               "/calculator",
               req => new HomeController().Calculate(req));

            appRouteConfig.Post(
               "/calculator",
                   req => new HomeController().Calculate(req));

            appRouteConfig.Get(
                "/about",
                req => new HomeController().AboutUs());

            appRouteConfig.Get(
                "/search", 
                req => new CakeController().Search(req));

            appRouteConfig.Post(
               "/search",
               req => new CakeController().Search(req));

            appRouteConfig.Post(
               "/add",
                   req => new CakeController().AddPost(req.FormData["name"], double.Parse(req.FormData["price"])));

            appRouteConfig.Get(
                "/add",
                 req => new CakeController().AddGet());

            appRouteConfig.Get(
                "/register",
                req => new UserController().RegisterGet());

            appRouteConfig.Post(
                "/register",
                req => new UserController().RegisterPost(req.FormData["name"]));

            appRouteConfig.Get(
                "/user/{(?<name>[a-z]+)}",
                req => new UserController().Details(req.UrlParameters["name"]));

            appRouteConfig.Get(
              "/survey",
              req => new UserController().UserInformationGet(req));

            appRouteConfig.Post(
              "/survey",
              req => new UserController().UserInformationPost(req));

            appRouteConfig.Post(
               "/logout",
               req => new AccountController().LogOut(req));
        }
    }
}