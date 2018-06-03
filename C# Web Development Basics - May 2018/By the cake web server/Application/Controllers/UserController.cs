namespace WebServer.Application.Controllers
{
    using Server;
    using Server.Enums;
    using Server.Http.Contracts;
    using Server.Http.Response;
    using System.Collections.Generic;
    using Views.User;
    using WebServer.Application.Models;
    using WebServer.Application.Views;

    public class UserController
    {
        //public IHttpResponse RegisterGet()
        //{
        //    return new ViewResponse(HttpStatusCode.Ok, new RegisterView());
        //}

        //public IHttpResponse RegisterPost(string name)
        //{
        //    return new RedirectResponse($"/user/{name}");
        //}

        public IHttpResponse Details(string name)
        {
            Model model = new Model { ["name"] = name };

            return new ViewResponse(HttpStatusCode.Ok, new UserDetailsView(model));
        }

        public IHttpResponse SendEmail(IHttpRequest req)
        {
            if (Admin.IsAdmin())
            {
                if (req.FormData.ContainsKey("email") && req.FormData.ContainsKey("subject")
                && req.FormData.ContainsKey("textToSend"))
                {
                    var email = req.FormData["email"];
                    var subject = req.FormData["subject"];
                    var text = req.FormData["textToSend"];
                    var sendEmail = new SendEmail();
                    sendEmail.SendEmailTo(email, subject, text);
                }

                return new ViewResponse(HttpStatusCode.Ok, new AdminView());
            }
            else
            {
                return new RedirectResponse($"/loginadmin");
            }
            
        }

        public IHttpResponse UserInformationGet(IHttpRequest req)
        {
            return new ViewResponse(HttpStatusCode.Ok, new UserInformationView(string.Empty));
        }
        
        public IHttpResponse UserInformationPost(IHttpRequest req)
        {
            var firstname = string.Empty;
            var lastname = string.Empty;
            var birthday = string.Empty;
            var status = string.Empty;
            var gender = string.Empty;
            var recommendations = string.Empty;
            var own = new List<string>();

            var message = "<p>Success Register!</p>";

            if (req.FormData.ContainsKey("firstName") && req.FormData.ContainsKey("lastName")
                && req.FormData.ContainsKey("birthday"))
            {
                firstname = req.FormData["firstName"];
                lastname = req.FormData["lastName"];
                birthday = req.FormData["birthday"];
                gender = req.FormData["gender"];
                status = req.FormData["status"];

                if (req.FormData.ContainsKey("noanswer"))
                {
                    own.Add(req.FormData["noanswer"]);
                }
                recommendations = req.FormData["recommendations"];

                if (req.FormData.ContainsKey("laptop"))
                {
                    own.Add(req.FormData["laptop"]);
                }
                if (req.FormData.ContainsKey("smartPhone"))
                {
                    own.Add(req.FormData["smartPhone"]);
                }
                if (req.FormData.ContainsKey("mobilePhone"))
                {
                    own.Add(req.FormData["mobilePhone"]);
                }
                if (req.FormData.ContainsKey("car"))
                {
                    own.Add(req.FormData["car"]);
                }
                if (req.FormData.ContainsKey("bike"))
                {
                    own.Add(req.FormData["bike"]);
                }
            }

            var survey = new Survey(firstname, lastname, birthday, gender, status, recommendations, own);

            if (!survey.HasValidData())
            {
                message = "<p>username/password/birthday are required</p>";
            }

            return new ViewResponse(HttpStatusCode.Ok, new UserInformationView(message));
        }
        
    }
}