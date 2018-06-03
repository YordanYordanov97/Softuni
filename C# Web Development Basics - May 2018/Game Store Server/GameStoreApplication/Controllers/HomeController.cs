using System;
using System.Collections.Generic;
using System.Text;
using WebServer.GameStoreApplication.Models;
using WebServer.GameStoreApplication.Views;
using WebServer.Server.Enums;
using WebServer.Server.Http.Contracts;
using WebServer.Server.Http.Response;
using WebServer.Server.HTTP;

namespace WebServer.GameStoreApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly GameService games;

        public HomeController()
        {
            this.games = new GameService();
        }

        public IHttpResponse Home(IHttpRequest req)
        {
            return new ViewResponse(HttpStatusCode.Ok, new HtmlView(@"home\index", this.ViewData,req));
        }

        public IHttpResponse Info(IHttpRequest req)
        {
            var gameId = int.Parse(req.UrlParameters["id"]);

            var game = games.GetGameByGivenId(gameId);

            this.ViewData["{{{Title}}}"] = game.Title;
            this.ViewData["{{{videoUrl}}}"] = $"src=\"https://www.youtube.com/embed/{game.Trailer}\"";
            this.ViewData["{{{Description}}}"] = game.Description;
            this.ViewData["{{{Price}}}"] = game.Price.ToString();
            this.ViewData["{{{Size}}}"] = $"{game.Size:f1}";
            this.ViewData["{{{Date}}}"] = Convert.ToDateTime(game.ReleaseDate).ToString("yyyy/MM/dd");
            this.ViewData["{{{Value}}}"] = $"value=\"{game.Id}\"";
            this.ViewData["{{{GameId}}}"]= $"value=\"{game.Id}\"";

            if (req.Session.Contains(SessionStore.CurrentAdminKey))
            {
                this.ViewData["<!--AdminButtons-->"] = $"<form action=\"/editgame/{game.Id}\" style=\"display:inline\">" +
                        $"<button  class=\"btn btn-warning\" type=\"submit\">Edit</button>" +
                        "</form>" +
                        "<span> </span>" +
                        $"<form action=\"/deletegame/{game.Id}\" style=\"display:inline\">" +
                        $"<button  class=\"btn btn-danger\" type=\"submit\">Delete</button>" +
                        "</form>" +
                       "<span> </span>";
            }


            return new ViewResponse(HttpStatusCode.Ok, new HtmlView(@"home\info", this.ViewData,req));
        }
    }
}
