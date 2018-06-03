using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WebServer.Application.Models;
using WebServer.Server.Contracts;

namespace WebServer.Application.Views
{
    public class LoginAdminView : IView
    {
        private const string ReplacementSnip = "<!--replace-->";
        private string method;
        private const string FailMessage = "<p style=\"color:red\">Invalid username or password</p>";

        public LoginAdminView(string method)
        {
            this.method = method;
        }

        public string View()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            string newPath = Path.GetFullPath(Path.Combine(currentDirectory, @"..\..\..\"));
            string html = File.ReadAllText(newPath + @".\Application\Resources\loginadmin.html");

            var failMessageString = string.Empty;
            if (this.method == "Post")
            {
                failMessageString = FailMessage;
            }

            html = html.Replace(ReplacementSnip, failMessageString);

            return html;
        }
        
    }
}
