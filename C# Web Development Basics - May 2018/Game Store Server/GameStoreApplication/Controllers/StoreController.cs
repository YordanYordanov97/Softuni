using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebServer.GameStoreApplication.Data.Models;
using WebServer.GameStoreApplication.Helpers;
using WebServer.GameStoreApplication.Models;
using WebServer.GameStoreApplication.Views;
using WebServer.Server.Enums;
using WebServer.Server.Http.Contracts;
using WebServer.Server.Http.Response;
using WebServer.Server.HTTP;

namespace WebServer.GameStoreApplication.Controllers
{
    public class StoreController : Controller
    {
        private Cart cart;
        private readonly GameService games;
        private readonly UserService userService;

        public StoreController()
        {
            this.cart = new Cart();
            this.games = new GameService();
            this.userService = new UserService();
        }

        public IHttpResponse ShowProducts(IHttpRequest req)
        {
            var products = games.GetAllGamesFromDb();
            var skip =0;
            var sb = new StringBuilder();
            
            while (true)
            {
                var productItems = products.Skip(skip).Take(3).ToList();

                if (productItems.Count == 0)
                {
                    break;
                }
                sb.AppendLine("<tr>");
                foreach(var product in productItems)
                {
                    if (!req.Session.Contains(SessionStore.CurrentAdminKey))
                    {
                        sb.AppendLine("<th scope = \"col\" style=\"word-wrap: break-word\"><div>" +
                        $"<img src=\"{product.ImageUrl}\" \"height=\"200\" width=\"200\">" +
                        "<br/>" +
                        $"<div>{product.Title}</div>" +
                        "<br/>" +
                        $"<div><strong>Price</strong> - {product.Price}&euro;</div>" +
                        "<br/>" +
                        $"<div><strong>Size</strong> - {product.Size:f1}GB</div>" +
                        "<br/>" +
                        $"<p>{product.Description}</p>" +
                        "<div style=\"background-color: #deeff5\">" +
                        $"<form action=\"/info/{product.Id}\" style=\"display:inline\">" +
                         $"<button  class=\"btn btn-outline-info\" type=\"submit\">Info</button>" +
                         "</form>" +
                         "<span> </span>" +
                        "<form method=\"post\" style=\"display:inline\">" +
                        $"<input name=\"gameId\" value=\"{product.Id}\"type=\"hidden\"  />" +
                         $"<button  class=\"btn btn-primary\" value=\"{product.Id}\" type=\"submit\">Buy</button>" +
                         "</form>" +
                         "</div>" +
                        "</th>");
                    }
                    else
                    {
                        sb.AppendLine("<th scope = \"col\" style=\"word-wrap: break-word\"><div>" +
                       $"<img src=\"{product.ImageUrl}\" \"height=\"200\" width=\"200\">" +
                       "<br/>" +
                       $"<div>{product.Title}</div>" +
                       "<br/>" +
                       $"<div><strong>Price</strong> - {product.Price}&euro;</div>" +
                       "<br/>" +
                       $"<div><strong>Size</strong> - {product.Size:f1}GB</div>" +
                       "<br/>" +
                       $"<p>{product.Description}</p>" +
                       "<div style=\"background-color: #deeff5\">" +
                       $"<form action=\"/editgame/{product.Id}\" style=\"display:inline\">" +
                        $"<button  class=\"btn btn-warning\" type=\"submit\">Edit</button>" +
                        "</form>" +
                        "<span> </span>" +
                        $"<form action=\"/deletegame/{product.Id}\" style=\"display:inline\">" +
                        $"<button  class=\"btn btn-danger\" type=\"submit\">Delete</button>" +
                        "</form>" +
                        "<span> </span>" +
                       $"<form action=\"/info/{product.Id}\" style=\"display:inline\">" +
                        $"<button  class=\"btn btn-outline-info\" type=\"submit\">Info</button>" +
                        "</form>" +
                        "<span> </span>" +
                       "<form method=\"post\" style=\"display:inline\">" +
                        $"<input name=\"gameId\" value=\"{product.Id}\"type=\"hidden\"  />" +
                        $"<button  class=\"btn btn-primary\" type=\"submit\">Buy</button>" +
                        "</form>" +
                        "</div>" +
                       "</th>");
                    }
                }
                sb.AppendLine("</tr>");
                skip += 3;
            }

            if (req.FormData.ContainsKey("gameId"))
            {
                if(!req.Session.Contains(SessionStore.CurrentAdminKey) &&
                    !req.Session.Contains(SessionStore.CurrentUserKey))
                {
                    return new RedirectResponse($"/login");
                }
                
                cart = req.Session.Get<Cart>(SessionStore.CurrentCartKey);

                var userEmail = string.Empty;

                if (req.Session.Contains(SessionStore.CurrentUserKey))
                {
                    userEmail = req.Session.Get(SessionStore.CurrentUserKey).ToString();
                }
                else if (req.Session.Contains(SessionStore.CurrentAdminKey))
                {
                    userEmail = req.Session.Get(SessionStore.CurrentAdminKey).ToString();
                }
                
                var gameId = int.Parse(req.FormData["gameId"]);

                var ownedGames = userService.OwnedGames(userEmail);

                if (ownedGames.Any(x => x.Id == gameId))
                {
                    //var modal = GetFileFromDirectory.GetFileAllTextByCurrentName(@"cart\modal", "html");
                    //this.ViewData["<!--FailMessage-->"] = modal;
                    this.ViewData["<!--FailMessage-->"] = "<p class=\"text-danger\">You already buy this product</p>";
                }
                else
                {
                    cart.AddGame(gameId);
                }
            }
            
            this.ViewData["<!--Replace-->"] = sb.ToString().Trim();

            return new ViewResponse(HttpStatusCode.Ok, new HtmlView(@"home\store", this.ViewData,req));
        }

        public IHttpResponse CartGet(IHttpRequest req)
        {
            cart = req.Session.Get<Cart>(SessionStore.CurrentCartKey);

            var products = cart.GetAllProducts();
            
            var productsAsHtml=this.DisplayCartProductsAsHtml(products);

            this.ViewData["<!--Replace-->"] = productsAsHtml;
            var totalPrice = products.Sum(x => x.Price);
            this.ViewData["{{{TotalPrice}}}"] = $"{totalPrice}";

            return new ViewResponse(HttpStatusCode.Ok, new HtmlView(@"cart\cart", this.ViewData, req));
        }

        public IHttpResponse CartPost(IHttpRequest req)
        {
            cart = req.Session.Get<Cart>(SessionStore.CurrentCartKey);

            if (req.FormData.ContainsKey("deleteGameId"))
            {
                var gameIdToRemove = int.Parse(req.FormData["deleteGameId"]);
                cart.RemoveProduct(gameIdToRemove);

                var products = cart.GetAllProducts();
                var productsAsHtml = this.DisplayCartProductsAsHtml(products);

                this.ViewData["<!--Replace-->"] = productsAsHtml;
                var totalPrice = products.Sum(x => x.Price);
                this.ViewData["{{{TotalPrice}}}"] = $"{totalPrice}";

                return new ViewResponse(HttpStatusCode.Ok, new HtmlView(@"cart\cart", this.ViewData, req));
            }

            return new RedirectResponse($"/successorder");
        }

        public IHttpResponse SuccessOrder(IHttpRequest req)
        {
            var cart = req.Session.Get<Cart>(SessionStore.CurrentCartKey);

            var userEmail = string.Empty;

            if (req.Session.Contains(SessionStore.CurrentUserKey))
            {
                userEmail = req.Session.Get(SessionStore.CurrentUserKey).ToString();
            }
            else if (req.Session.Contains(SessionStore.CurrentAdminKey))
            {
                userEmail = req.Session.Get(SessionStore.CurrentAdminKey).ToString();
            }

            var cartProducts = cart.GetAllProducts();

            var orderService = new OrderService();
            orderService.SaveOrderProductsInDb(cartProducts, userEmail);
            cart.Clear();

            return new ViewResponse(HttpStatusCode.Ok, new HtmlView(@"cart\successorder", this.ViewData,req));
        }

        public IHttpResponse OwnedGames(IHttpRequest req)
        {
            var userEmail = string.Empty;

            if (req.Session.Contains(SessionStore.CurrentUserKey))
            {
                userEmail = req.Session.Get(SessionStore.CurrentUserKey).ToString();
            }
            else if (req.Session.Contains(SessionStore.CurrentAdminKey))
            {
                userEmail = req.Session.Get(SessionStore.CurrentAdminKey).ToString();
            }

            var ownedGames=userService.OwnedGames(userEmail);

            var sb = new StringBuilder();

            foreach (var ownedGame in ownedGames)
            {
                sb.AppendLine("<tr>" +
                    "<th scope = \"col\" style=\"word-wrap: break-word\">" +
                      "<span class=\"col - md - 12\">" +
                      "<span class=\"col - md - 1\">" +
                     "<span class=\"col - md - 4\">" +
                         $"<img src=\"{ownedGame.ImageUrl}\" \"height=\"30\" width=\"30%\">" +
                     "</span>" +
                     "<pre class=\"col - md - 7\" style=\"display:inline;white-space: pre-wrap;word-wrap: break-word;text-align: justify;\">" +
                         $"<span style=\"color:#0099CC;font-size: 30px;\">{ownedGame.Title}</span>" +
                         @"
                         "+
                         $"{ownedGame.Description}" +
                      "</pre>" +
                      "<span class=\"col - md - 1\">" +
                         $"<span>{ownedGame.Price}&euro;</span>" +
                         "</span>" +
                          "</span>" +
                        "</th>" +
                    "</tr>"
                    );
            }

            this.ViewData["<!--Replace-->"] = sb.ToString().Trim();

            return new ViewResponse(HttpStatusCode.Ok, new HtmlView(@"cart\ownedgames", this.ViewData, req));

        }

        private string DisplayCartProductsAsHtml(List<Game> products)
        {
            var sb = new StringBuilder();

            foreach (var product in products)
            {
                sb.AppendLine("<tr>" +
                    "<th scope = \"col\" style=\"word-wrap: break-word\">" +
                      "<span class=\"col - md - 12\">" +
                      "<span class=\"col - md - 1\">" +
                         "<form method=\"post\" style=\"display:inline\">" +
                        $"<input name=\"deleteGameId\" value=\"{product.Id}\"type=\"hidden\"  />" +
                        $"<button  class=\"btn btn-outline-danger\" type=\"submit\">X</button>" +
                        "</form>" +
                      "</span>" +
                     "<span class=\"col - md - 3\">" +
                         $"<img src=\"{product.ImageUrl}\" \"height=\"30\" width=\"30%\">" +
                     "</span>" +
                     "<pre class=\"col - md - 7\" style=\"display:inline;white-space: pre-wrap;word-wrap: break-word;text-align: justify;\">" +
                         $"<span style=\"color:#0099CC;font-size: 30px;\">{product.Title}</span>" +
                          @"
                         " +
                         $"{product.Description}" +
                      "</pre>" +
                      "<span class=\"col - md - 1\">" +
                         $"<span>{product.Price}&euro;</span>" +
                         "</span>" +
                          "</span>" +
                        "</th>" +
                    "</tr>"
                    );
            }

            return sb.ToString().Trim();
        }
    }
}
