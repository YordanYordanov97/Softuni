namespace WebServer.Application.Views.Home
{
    using Server.Contracts;
    using System.IO;

    public class IndexView : IView
    {
        public string View()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            string newPath = Path.GetFullPath(Path.Combine(currentDirectory, @"..\..\..\"));
            string html = File.ReadAllText(newPath + @".\Application\Resources\index.html");

            return html;
        }
    }
}