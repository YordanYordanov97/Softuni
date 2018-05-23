namespace WebServer.Application.Views.Account
{
    using System.IO;
    using WebServer.Server.Contracts;

    public class LoginUserView:IView
    {
        private const string ReplacementSnip = "<!--replace-->";
        private string message;

        public LoginUserView(string message)
        {
            this.message = message;
        }

        public string View()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            string newPath = Path.GetFullPath(Path.Combine(currentDirectory, @"..\..\..\"));
            string html = File.ReadAllText(newPath + @".\Application\Resources\login.html");

            html = html.Replace(ReplacementSnip, message);

            return html;
        }
    }
}
