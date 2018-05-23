namespace WebServer.Application.Views.Cakes
{
    using System.IO;
    using WebServer.Application.Models;
    using WebServer.Server.Contracts;

    public class AddCakeView : IView
    {
        private const string ReplacementSnip = "<!--replace-->";

        private string htmlReplace;

        public AddCakeView(string htmlReplace)
        {
            this.htmlReplace = htmlReplace;
        }

        public string View()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            string newPath = Path.GetFullPath(Path.Combine(currentDirectory, @"..\..\..\"));
            string html = File.ReadAllText(newPath + @".\Application\Resources\addcake.html");
            
            html = html.Replace(ReplacementSnip, htmlReplace);

            return html;
        }
    }
}
