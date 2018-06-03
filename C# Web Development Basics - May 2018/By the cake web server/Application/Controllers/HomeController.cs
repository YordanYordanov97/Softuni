namespace WebServer.Application.Controllers
{
    using Server.Enums;
    using Server.Http.Contracts;
    using Server.Http.Response;
    using System;
    using WebServer.Application.Models;
    using WebServer.Application.Views;
    using WebServer.Application.Views.Home;

    public class HomeController: Controller
    {
        public IHttpResponse Index(IHttpRequest req)
        {
            var response = new ViewResponse(HttpStatusCode.Ok, new HtmlView(@"home\index",this.ViewData));

            return response;
        }

        public IHttpResponse AboutUs()
        {
            var response = new ViewResponse(HttpStatusCode.Ok, new HtmlView(@"home\about", this.ViewData));

            return response;
        }
        
        public IHttpResponse Calculate(IHttpRequest req)
        {
            var numberOne = string.Empty;
            var mathSign = string.Empty;
            var numberTwo = string.Empty;
            var result = string.Empty;

            if (req.FormData.ContainsKey("numberOne") && req.FormData.ContainsKey("mathSign")
                && req.FormData.ContainsKey("numberTwo"))
            {
                numberOne = req.FormData["numberOne"];
                mathSign = req.FormData["mathSign"];
                numberTwo = req.FormData["numberTwo"];

                var calculator = new Calculator();
                result = calculator.CalculateNumbers(double.Parse(numberOne), mathSign, double.Parse(numberTwo));
            }

            this.ViewData["<!--replaceNumberOne-->"] = $"<input name = \"numberOne\" type = \"number\" value=\"{numberOne}\" " +
                $"placeholder=\"Enter Number\" step = \"0.25\" autocomplete = \"off\" />";
            this.ViewData["<!--replaceMathSign-->"] = $"<input name = \"mathSign\" type = \"text\" value=\"{mathSign}\" " +
                $"placeholder=\"Math Sign\" autocomplete = \"off\" />";
            this.ViewData["<!--replaceNumberTwo-->"] = $"<input name = \"numberTwo\" type = \"number\" value=\"{numberTwo}\" " +
                $"placeholder=\"Enter Number\" step = \"0.25\" autocomplete = \"off\" />";
            this.ViewData[" <!--replaceResult-->"] = result;

            return new ViewResponse(HttpStatusCode.Ok, new HtmlView(@"home\calculator", this.ViewData));
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