using System;
using System.Collections.Generic;
using System.Text;
using WebServer.GameStoreApplication.Models;
using WebServer.GameStoreApplication.Models.Validators;
using WebServer.GameStoreApplication.Views;
using WebServer.Server.Enums;
using WebServer.Server.Http.Contracts;
using WebServer.Server.Http.Response;
using WebServer.Server.HTTP;

namespace WebServer.GameStoreApplication.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserService user;

        public AccountController()
        {
            this.user = new UserService();
        }

        public IHttpResponse RegisterGet(IHttpRequest req)
        {
            return new ViewResponse(HttpStatusCode.Ok, new HtmlView(@"account\register", this.ViewData,req));
        }

        public IHttpResponse RegisterPost(IHttpRequest req)
        {
            var email = string.Empty;
            var fullName = string.Empty;
            var password = string.Empty;
            var confirmPassword = string.Empty;
            var isValid = true;

            if (req.FormData.ContainsKey("email"))
            {
                email = req.FormData["email"];

                if (!EmailValidator.IsValid(email))
                {
                    this.ViewData["<!--ErrorEmail-->"] = "<div class=\"col - md - 4\">" +
                         "<p class=\"text-danger\">Invalid Email. It should contains @</p>" +
                               "</div>";
                    isValid = false;
                }
                else
                {
                    if (user.IsAlreadyHaveUserWithThisEmail(email))
                    {
                        this.ViewData["<!--ErrorEmail-->"] = "<div class=\"col - md - 4\">" +
                             "<p class=\"text-danger\">Email has taken!</p>" +
                                   "</div>";
                        isValid = false;
                    }
                }
            }

            if (req.FormData.ContainsKey("full-name"))
            {
                fullName = req.FormData["full-name"];

                if (string.IsNullOrEmpty(fullName) || string.IsNullOrWhiteSpace(fullName))
                {
                    this.ViewData["<!--ErrorFullName-->"] = "<div class=\"col - md - 4\">" +
                         "<p class=\"text-danger\">Invalid Full Name. Full Name is Required!</p>" +
                               "</div>";
                    isValid = false;
                }
            }

            if (req.FormData.ContainsKey("password"))
            {
                password = req.FormData["password"];

                if (!PasswordValidator.IsValid(password))
                {
                    this.ViewData["<!--ErrorPassoword-->"] = "<div class=\"col - md - 4\">" +
                         "<p class=\"text-danger\">Invalid Password. Password shout be at least 6 symbols long," +
                         "containing 1 uppercase letter,1 lowercase letter nad 1 digit!</p>" +
                               "</div>";
                    isValid = false;
                }
            }

            if (req.FormData.ContainsKey("confirm-passoword"))
            {
                confirmPassword = req.FormData["confirm-passoword"];

                if (confirmPassword != password)
                {
                    this.ViewData["<!--ErrorConfirmPassword-->"] = "<div class=\"col - md - 4\">" +
                         "<p class=\"text-danger\">Invalid Confirm Password. Confirm Password is different than Password!</p>" +
                               "</div>";
                    isValid = false;
                }
            }
            if (isValid)
            {
                user.SaveInformationInDb(email, fullName, password,false);
                this.LoginUser(req, email,false);

                return new RedirectResponse($"/");
            }
            else
            {
                return new ViewResponse(HttpStatusCode.Ok, new HtmlView(@"account\register", this.ViewData,req));
            }
        }

        public IHttpResponse LogInGet(IHttpRequest req)
        {
            return new ViewResponse(HttpStatusCode.Ok, new HtmlView(@"account\login", this.ViewData,req));
        }

        public IHttpResponse LogInPost(IHttpRequest req)
        {
            var email = string.Empty;
            var password = string.Empty;
            if (req.FormData.ContainsKey("email") && req.FormData.ContainsKey("password"))
            {
                email = req.FormData["email"];
                password = req.FormData["password"];
            }

            if (user.Find(email, password))
            {
                var isAdmin = user.IsAdmin(email, password);
                this.LoginUser(req, email, isAdmin);
                return new RedirectResponse($"/");
            }
            else
            {
                this.ViewData["<!--unsuccessfulLogin-->"] = "<p class=\"text-danger\">Invalid email or password!</p>";
            }

            return new ViewResponse(HttpStatusCode.Ok, new HtmlView(@"account\login", this.ViewData,req));
        }

        public IHttpResponse LogOut(IHttpRequest req)
        {
            req.Session.Clear();
            return new RedirectResponse($"/login");
        }

        private void LoginUser(IHttpRequest req, string email,bool isAdmin)
        {
            if (isAdmin)
            {
                req.Session.Add(SessionStore.CurrentAdminKey, email);
            }
            else
            {
                req.Session.Add(SessionStore.CurrentUserKey, email);
            }

            req.Session.Add(SessionStore.CurrentCartKey, new Cart());
        }

    }
}
