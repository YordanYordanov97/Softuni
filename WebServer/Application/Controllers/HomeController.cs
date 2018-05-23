namespace WebServer.Application.Controllers
{
    using Server.Enums;
    using Server.Http.Contracts;
    using Server.Http.Response;
    using System;
    using WebServer.Application.Models;
    using WebServer.Application.Views;
    using WebServer.Application.Views.Home;
    using WebServer.Server.HTTP;

    public class HomeController
    {
        public IHttpResponse Index(IHttpRequest req)
        {
            var response = new ViewResponse(HttpStatusCode.Ok, new IndexView());

            return response;
        }

        public IHttpResponse AboutUs()
        {
            var response = new ViewResponse(HttpStatusCode.Ok, new AboutUsView());

            return response;
        }
        
        public IHttpResponse Calculate(IHttpRequest req)
        {
            var numberOne = 0.00D;
            var numberTwo = 0.00D;
            var matSign = string.Empty;

            if (req.FormData.Count > 0)
            {
                numberOne = double.Parse(req.FormData["numberOne"]);
                numberTwo = double.Parse(req.FormData["numberTwo"]);
                matSign = req.FormData["mathSign"];
            }

            return new ViewResponse(HttpStatusCode.Ok, new CalculatorView(numberOne, matSign, numberTwo));
        }

        public IHttpResponse LogInAdmin(IHttpRequest req, string method)
        {

            if (req.FormData.ContainsKey("username") && req.FormData.ContainsKey("password"))
            {
                Admin.Username = req.FormData["username"];
                Admin.Password = req.FormData["password"];
            }

            if (Admin.IsAdmin())
            {
                return new RedirectResponse($"/success");
            }
            else
            {
                return new ViewResponse(HttpStatusCode.Ok, new LoginAdminView(method));
            }
        }

        public IHttpResponse Greeting(IHttpRequest req)
        {
            var firstname = string.Empty;
            var lastname = string.Empty;
            var age = string.Empty;

            if (req.FormData.ContainsKey("firstName"))
            {
                firstname = req.FormData["firstName"];

                UserData.Firstname = firstname;
            }
            else if (req.FormData.ContainsKey("lastName"))
            {
                lastname = req.FormData["lastName"];
                UserData.Lastname = lastname;
            }
            else if (req.FormData.ContainsKey("age"))
            {
                age = req.FormData["age"];
                UserData.Age = int.Parse(age);
            }

            return new ViewResponse(HttpStatusCode.Ok, new GreetingView(firstname, lastname, age));
        }

        public IHttpResponse SessionTest(IHttpRequest req)
        {
            var session = req.Session;

            const string sessionDateKey = "saved_date";
            
            if (session.Get(sessionDateKey) == null)
            {
                session.Add(sessionDateKey, DateTime.UtcNow);
            }

            return new ViewResponse(
                HttpStatusCode.Ok,
                new SessionTestView(session.Get<DateTime>(sessionDateKey)));
        }
    }
}