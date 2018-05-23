using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WebServer.Application.Models;
using WebServer.Server.Contracts;

namespace WebServer.Application.Views
{
    public class AdminView:IView
    {
        private const string ReplacementSnip = "<!--replace-->";

        public string View()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            string newPath = Path.GetFullPath(Path.Combine(currentDirectory, @"..\..\..\"));
            string html = File.ReadAllText(newPath + @".\Application\Resources\success.html");

            var greetingResult = $"<h1>Hi, {Admin.Username}!</h1>";
            html = html.Replace(ReplacementSnip, greetingResult);

            return html;
        }
    }
}
