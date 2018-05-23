namespace WebServer.Application.Views.ShoppingCart
{
    using System.IO;
    using WebServer.Server.Contracts;

    public class SuccessOrderView:IView
    {
        public string View()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            string newPath = Path.GetFullPath(Path.Combine(currentDirectory, @"..\..\..\"));
            string html = File.ReadAllText(newPath + @".\Application\Resources\successorder.html");
            

            return html;
        }
    }
}
