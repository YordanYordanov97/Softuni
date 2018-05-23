namespace WebServer.Application.Views.Home
{
    using System.IO;
    using WebServer.Application.Models;
    using WebServer.Server.Contracts;

    public class GreetingView:IView
    {
        private const string ReplacementSnip = "<!--replace-->";
        private string firstname;
        private string lastname;
        private string age;

        public GreetingView(string firstname,string lastname,string age)
        {
            this.firstname = firstname;
            this.lastname = lastname;
            this.age = age;
        }

        public string View()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            string newPath = Path.GetFullPath(Path.Combine(currentDirectory, @"..\..\..\"));
            string html = File.ReadAllText(newPath + @".\Application\Resources\firstname.html");
           
            if (!string.IsNullOrEmpty(firstname))
            {
                html = File.ReadAllText(newPath + @".\Application\Resources\lastname.html");
            }

            if (!string.IsNullOrEmpty(lastname))
            {
                html = File.ReadAllText(newPath + @".\Application\Resources\age.html");
            }

            if (!string.IsNullOrEmpty(age))
            {
                html = File.ReadAllText(newPath + @".\Application\Resources\greeting.html");
                var result = $"<h1>Hello {UserData.Firstname} {UserData.Lastname} at age {UserData.Age}!";

                html = html.Replace(ReplacementSnip, result);
            }

            return html;
        }
    }
}
