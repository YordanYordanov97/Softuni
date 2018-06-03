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

    public class CakeController : Controller
    {
        private Cart cart;
        private readonly ProductService product;

        public CakeController()
        {
            this.cart = new Cart();
            this.product = new ProductService();
        }

        public IHttpResponse AddGet()
        {
            return new ViewResponse(HttpStatusCode.Ok, new HtmlView(@"products\addcake", this.ViewData));
        }

        public IHttpResponse AddPost(string name, decimal price, string imageUrl)
        {
            if (name.Length < 3
                || name.Length > 30
                || imageUrl.Length < 3
                || imageUrl.Length > 2000)
            {
                this.ViewData["<!--replaceCakes-->"] = "<div>Product information is not valid!</div>";

                return new ViewResponse(HttpStatusCode.Ok, new HtmlView(@"products\addcake", this.ViewData));
            }

            this.product.SaveInDb(name, price, imageUrl);

            this.ViewData["<!--replaceCakes-->"] = $"<div>name: {name}</div>" +
                               $"<div>price: {price:f2}</div>" +
                               $"<img src=\"{imageUrl}\" style=\"max-width:250px;border-radius:15px\" alt=\"cake\">";

            return new ViewResponse(HttpStatusCode.Ok, new HtmlView(@"products\addcake", this.ViewData));
        }

        public IHttpResponse Search(IHttpRequest req)
        {
            var searchedCakeName = string.Empty;
            var sb = new StringBuilder();
            var ordersCount = 0;
            var formValue = string.Empty;
            
            if (req.UrlParameters.ContainsKey("name") || req.FormData.ContainsKey("cake"))
            {
                searchedCakeName = req.UrlParameters["name"];
                formValue = searchedCakeName;

                var products = this.product.FindByName(searchedCakeName);

                if (products.Count == 0)
                {
                    this.ViewData["<!--searchedCakes-->"] = $"<div>Cake Not Found</div>";
                }

                foreach (var product in products)
                {
                    var productId = product.Id;
                    var productName = product.Name;
                    var productPrice= product.Price;
                    var imageUrlOfProduct = product.ImageUrl;
                    
                    var orderForm = $"<form method=\"post\">" +
                        $"<div><a href=\"/cakeDetails/{productId}\">{productName}</a> ${productPrice}</div>" +
                    $"<input name=\"cakeId\" value=\"{productId}\"type=\"hidden\"  />" +
                    "<input type=\"submit\" value=\"Order\" />" +
                    "</form>";

                    sb.AppendLine($"{orderForm}");

                }

                if (req.FormData.ContainsKey("cakeId"))
                {
                    var orderCakeId = int.Parse(req.FormData["cakeId"]);

                    cart = req.Session.Get<Cart>(SessionStore.CurrentCartKey);

                    cart.AddProduct(orderCakeId);
                    ordersCount = cart.GetProductsCount();
                }
            }

            var ordersCountAsSting = string.Empty;
            if (ordersCount > 1)
            {
                ordersCountAsSting = $"<p><a href=\"/cart\">Your Cart:</a> {ordersCount } products</p>";
            }
            else if (ordersCount == 1)
            {
                ordersCountAsSting = $"<p><a href=\"/cart\">Your Cart:</a> {ordersCount } product</p>";
            }

            var form = $"<input name=\"name\" type=\"text\" placeholder=\"Enter Cake name\" value=\"{formValue}\" />";

            this.ViewData["<!--form-->"] = form;
            this.ViewData["<!--productCount-->"] = ordersCountAsSting;
            this.ViewData["<!--searchedCakes-->"] = sb.ToString().Trim();
            

            return new ViewResponse(HttpStatusCode.Ok, new HtmlView(@"products\search", this.ViewData));
        }

        public IHttpResponse Details(IHttpRequest req)
        {
            var id=int.Parse(req.UrlParameters["id"]);

            var givenProduct=product.FindById(id);

            this.ViewData["<!--CakeName-->"] = $"<h1>{givenProduct.Name}</h1>";
            this.ViewData["<!--CakePrice-->"] = $"<div><em>Price: ${givenProduct.Price}</em></div>";
            this.ViewData["<!--CakeImage-->"] = $"<div><img src=\"{givenProduct.ImageUrl}\" style=\"max-width:500px;border-radius:15px\" alt=\"{givenProduct.Name}\"></div>";

            return new ViewResponse(HttpStatusCode.Ok, new HtmlView(@"products\cakedetails", this.ViewData));
        }
    }
}
