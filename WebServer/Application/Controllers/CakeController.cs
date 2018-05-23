namespace WebServer.Application.Controllers
{
    using System.IO;
    using System.Text;
    using WebServer.Application.Models;
    using WebServer.Application.Views.Cakes;
    using WebServer.Server.Enums;
    using WebServer.Server.Http.Contracts;
    using WebServer.Server.Http.Response;
    using WebServer.Server.HTTP;

    public class CakeController
    {
        private Cart cart;

        public CakeController()
        {
            this.cart = new Cart();
        }

        public IHttpResponse AddGet()
        {
            return new ViewResponse(HttpStatusCode.Ok, new AddCakeView(string.Empty));
        }

        public IHttpResponse AddPost(string name, double price)
        {
            var cake = new Cake();
            cake.AddCakeInFile(name, price);

            string cakesHtml = $"<div>name: {name}</div>" +
                               $"<div>price: {price:f2}</div>";

            return new ViewResponse(HttpStatusCode.Ok, new AddCakeView(cakesHtml));
        }

        public IHttpResponse Search(IHttpRequest req)
        {
            var searchedCakeName = string.Empty;
            var sb = new StringBuilder();
            var productsCount = 0;
            var formValue = string.Empty;

            if (req.UrlParameters.ContainsKey("name") || req.FormData.ContainsKey("cake"))
            {
                searchedCakeName = req.UrlParameters["name"];
                formValue = searchedCakeName;

                var currentDirectory = Directory.GetCurrentDirectory();
                string newPath = Path.GetFullPath(Path.Combine(currentDirectory, @"..\..\..\"));
                var fileLines = File.ReadAllLines(newPath + @".\Application\Data\database.csv");

                foreach (var line in fileLines)
                {
                    if (line.ToLower().Contains(searchedCakeName.ToLower()))
                    {
                        var lineSplit = line.Split(',');
                        var id = lineSplit[0];
                        var cakeName = lineSplit[1];
                        var cakePrice = lineSplit[2];

                        var cakeHtml = $"{cakeName} ${cakePrice}";
                        var orderForm = $"<form method=\"post\">" +
                            $"<div>{cakeName} ${cakePrice}</div>" +
                            $"<input name=\"cakeId\" value=\"{id}\"type=\"hidden\"  />" +
                        "<input type=\"submit\" value=\"Order\" />" +
                        "</form>";

                        sb.AppendLine($"{orderForm}");
                    }
                }

                if (req.FormData.ContainsKey("cakeId"))
                {
                    var orderCakeId = req.FormData["cakeId"];

                    cart = req.Session.Get<Cart>(SessionStore.CurrentCartKey);
                    
                    cart.AddProduct(orderCakeId);
                    productsCount = cart.GetProductsCount();
                    req.Session.Add(SessionStore.CurrentCartKey, cart);
                }
            }
            
            return new ViewResponse(HttpStatusCode.Ok, new SearchView(sb.ToString().Trim(), productsCount, formValue));
        }
    }
}
