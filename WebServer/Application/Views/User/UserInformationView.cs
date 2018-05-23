namespace WebServer.Application.Views.User
{
    using System.IO;
    using WebServer.Application.Models;
    using WebServer.Server.Contracts;

    public class UserInformationView : IView
    {
        private const string ReplacementSnip = "<!--replace-->";

        private string message;

        public UserInformationView(string message)
        {
            this.message = message;
        }

        public string View()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            string newPath = Path.GetFullPath(Path.Combine(currentDirectory, @"..\..\..\"));
            string html = File.ReadAllText(newPath + @".\Application\Resources\survey.html");
            var sendResult = string.Empty;
            
            html = html.Replace(ReplacementSnip, message);
            return html;
        }
    }
}
