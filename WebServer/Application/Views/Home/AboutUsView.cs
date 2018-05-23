namespace WebServer.Application.Views.Home
{
    using System.IO;
    using WebServer.Server.Contracts;

    public class AboutUsView : IView
    {
        public string View()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            string newPath = Path.GetFullPath(Path.Combine(currentDirectory, @"..\..\..\"));
            string html = File.ReadAllText(newPath + @".\Application\Resources\about.html");

            return html;
        }
    }
}
