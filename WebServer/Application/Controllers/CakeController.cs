namespace WebServer.Application.Controllers
{
    using System.IO;
    using System.Text;
    using WebServer.Application.Helpers;
    using WebServer.Application.Models;
    using WebServer.Application.Views;
    using WebServer.Server.Enums;
    using WebServer.Server.Http.Contracts;
    using WebServer.Server.Http.Response;
    using WebServer.Server.HTTP;

    public class CakeController:Controller
    {
        private Cart cart;

        public CakeController()
        {
            this.cart = new Cart();
        }

        public IHttpResponse AddGet()
        {
            this.ViewData["<!--replaceCakes-->"] = string.Empty;
            return new ViewResponse(HttpStatusCode.Ok, new HtmlView("addcake",this.ViewData));
        }

        public IHttpResponse AddPost(string name, double price)
        {
            var cake = new Cake();
            cake.AddCakeInFile(name, price);

            this.ViewData["<!--replaceCakes-->"]= $"<div>name: {name}</div>" +
                               $"<div>price: {price:f2}</div>";

            return new ViewResponse(HttpStatusCode.Ok, new HtmlView("addcake", this.ViewData));
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

                var fileLines = GetFileFromDirectory.GetFileLinesByCurrentName("database", "csv");

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
                }
            }

            var productCountAsSting = string.Empty;
            if (productsCount > 1)
            {
                productCountAsSting = $"<p><a href=\"/cart\">Your Cart:</a> {productsCount} products</p>";
            }
            else if (productsCount == 1)
            {
                productCountAsSting = $"<p><a href=\"/cart\">Your Cart:</a> {productsCount} product</p>";
            }

            var form = $"<input name=\"name\" type=\"text\" placeholder=\"Enter Cake name\" value=\"{formValue}\" />";

            this.ViewData["<!--form-->"] = form;
            this.ViewData["<!--productCount-->"] = productCountAsSting;
            this.ViewData["<!--searchedCakes-->"] = sb.ToString().Trim();
            
            return new ViewResponse(HttpStatusCode.Ok, new HtmlView("search", this.ViewData));
        }
    }
}
