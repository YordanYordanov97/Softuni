using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using WebServer.GameStoreApplication.Models;
using WebServer.GameStoreApplication.Models.Validators;
using WebServer.GameStoreApplication.Views;
using WebServer.Server.Enums;
using WebServer.Server.Http.Contracts;
using WebServer.Server.Http.Response;

namespace WebServer.GameStoreApplication.Controllers
{
    public class AdminController:Controller
    {
        private readonly GameService games;

        public AdminController()
        {
            this.games = new GameService();
        }

        public IHttpResponse AllGames(IHttpRequest req)
        {
            var allgames = games.GetAllGamesFromDb();
            var sb = new StringBuilder();

            foreach (var game in allgames)
            {
                sb.AppendLine("<tr>" +
                    $"<th scope = \"col\">{game.Id}</th>" +
                    $"<th scope = \"col\">{game.Title}</th>" +
                    $"<th scope = \"col\">{game.Size:f2}</th>" +
                    $"<th scope = \"col\">{game.Price:f2}</th>" +
                    $"<th scope = \"col\" > <form action=\"/editgame/{game.Id}\" style=\"display: inline\">" +
                    "<button  class=\"btn btn-warning\" type=\"submit\">Edit</button>" +
                    "</form>" +
                    $"  <form action =\"/deletegame/{game.Id}\" style=\"display: inline\">" +
                   "<button  class=\"btn btn-danger\" type=\"submit\">Delete</button>" +
                    "</form>" +
                    "</th >" +
                 "</tr>");
            }

            //foreach (var game in allgames)
            //{
            //    this.ViewData["{{{action}}}"] = $"action=\"/deletegame/{game.Id}\"";
            //    sb.AppendLine("<tr>" +
            //        $"<th scope = \"col\">{game.Id}</th>" +
            //        $"<th scope = \"col\">{game.Title}</th>" +
            //        $"<th scope = \"col\">{game.Size:f2}</th>" +
            //        $"<th scope = \"col\">{game.Price:f2}</th>" +
            //        $"<th scope = \"col\" > <form action=\"/editgame/{game.Id}\" style=\"display: inline\">" +
            //        "<button  class=\"btn btn-warning\" type=\"submit\">Edit</button>" +
            //        "</form>" +
            //       "<button  class=\"btn btn-danger\" type=\"submit\" data-toggle=\"modal\" data-target=\"#exampleModal\">Delete</button>" +
            //        "</th >" +
            //     "</tr>");
            //}

            this.ViewData["<!--Replace-->"] = sb.ToString().Trim();

            return new ViewResponse(HttpStatusCode.Ok, new HtmlView(@"admin\allgames", this.ViewData,req));
        }

        public IHttpResponse AddGameGet(IHttpRequest req)
        {
            return new ViewResponse(HttpStatusCode.Ok, new HtmlView(@"admin\addgame", this.ViewData,req));
        }

        public IHttpResponse AddGamePost(IHttpRequest req)
        {
            var isValid = true;
            var title = string.Empty;
            var description = string.Empty;
            var imageUrl = string.Empty;
            var price = 0.00M;
            var size = 0.00D;
            var videoUrl = string.Empty;
            var releaseDate = string.Empty;

            if (req.FormData.ContainsKey("title") && req.FormData.ContainsKey("description")
                && req.FormData.ContainsKey("image-url") && req.FormData.ContainsKey("price")
                && req.FormData.ContainsKey("size") && req.FormData.ContainsKey("video-url")
                && req.FormData.ContainsKey("release-date"))
            {
                title = req.FormData["title"];
                description = req.FormData["description"];
                imageUrl = req.FormData["image-url"];
                price = decimal.Parse(req.FormData["price"]);
                size = double.Parse(req.FormData["size"]);
                videoUrl = req.FormData["video-url"];
                releaseDate= req.FormData["release-date"];
            }

            isValid = this.isValidGameInformation(title,description,size,price,imageUrl,videoUrl);

            DateTime parsedDate;
            try
            {
                parsedDate = DateTime.ParseExact(releaseDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);

                if (isValid)
                {
                    games.AddGameToDb(title, description, size, price, imageUrl, videoUrl, parsedDate);
                }
            }
            catch
            {
                this.ViewData["<!--ErrorDate-->"] = "<div class=\"col - md - 4\">" +
                         "<p class=\"text-danger\">Invalid Date! Date has to be in format dd-mm-yyyy" +
                               "</div>";
                isValid = false;
            }

            return new ViewResponse(HttpStatusCode.Ok, new HtmlView(@"admin\addgame", this.ViewData,req));
        }

        public IHttpResponse EditGame(IHttpRequest req)
        {
            var gameId = int.Parse(req.UrlParameters["id"]);

            var game=games.GetGameByGivenId(gameId);

            this.ViewData["{{{Name}}}}"] = game.Title;
            this.ViewData["{{{TitleValue}}}"]=$"value=\"{game.Title}\"";
            this.ViewData["{{{DescriptionValue}}}"] = game.Description;
            this.ViewData["{{{ImageUrlValue}}}"] = $"value=\"{game.ImageUrl}\"";
            this.ViewData["{{{PriceValue}}}"] = $"value=\"{game.Price}\"";
            this.ViewData["{{{SizeValue}}}"] = $"value=\"{game.Size}\"";
            this.ViewData["{{{VideoUrlValue}}}"] = $"value=\"{game.Trailer}\"";

            var convertDate = Convert.ToDateTime(game.ReleaseDate).ToString("yyyy-MM-dd");
            this.ViewData["{{{DateValue}}}"] = $"value=\"{convertDate}\"";


            if (req.FormData.ContainsKey("title") && req.FormData.ContainsKey("description")
                && req.FormData.ContainsKey("image-url") && req.FormData.ContainsKey("price")
                && req.FormData.ContainsKey("size") && req.FormData.ContainsKey("video-url")
                && req.FormData.ContainsKey("release-date"))
            {
                var title = req.FormData["title"];
                var description = req.FormData["description"];
                var imageUrl = req.FormData["image-url"];
                var price = decimal.Parse(req.FormData["price"]);
                var size = double.Parse(req.FormData["size"]);
                var videoUrl = req.FormData["video-url"];
                var releaseDate = req.FormData["release-date"];

                var isValid = this.isValidGameInformation(title, description, size, price, imageUrl, videoUrl);

                DateTime parsedDate;
                try
                {
                    parsedDate = DateTime.ParseExact(releaseDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);

                    if (isValid)
                    {
                        games.EditGame(gameId, title, description, size, price, imageUrl, videoUrl, parsedDate);

                        return new RedirectResponse($"/allgames");
                    }
                }
                catch
                {
                    this.ViewData["<!--ErrorDate-->"] = "<div class=\"col - md - 4\">" +
                             "<p class=\"text-danger\">Invalid Date! Date has to be in format dd-mm-yyyy</p>" +
                                   "</div>";
                    isValid = false;
                }
            }

            return new ViewResponse(HttpStatusCode.Ok, new HtmlView(@"admin\editgame", this.ViewData,req));
        }

        public IHttpResponse DeleteGameGet(IHttpRequest req)
        {
            var gameId = int.Parse(req.UrlParameters["id"]);
            var game = games.GetGameByGivenId(gameId);

            this.ViewData["{{{Name}}}}"] = game.Title;
            this.ViewData["{{{TitleValue}}}"] = $"value=\"{game.Title}\"";
            this.ViewData["{{{DescriptionValue}}}"] = game.Description;
            this.ViewData["{{{ImageUrlValue}}}"] = $"value=\"{game.ImageUrl}\"";
            this.ViewData["{{{PriceValue}}}"] = $"value=\"{game.Price}\"";
            this.ViewData["{{{SizeValue}}}"] = $"value=\"{game.Size}\"";
            this.ViewData["{{{VideoUrlValue}}}"] = $"value=\"{game.Trailer}\"";

            var convertDate = Convert.ToDateTime(game.ReleaseDate).ToString("yyyy-MM-dd");
            this.ViewData["{{{DateValue}}}"] = $"value=\"{convertDate}\"";
            
            return new ViewResponse(HttpStatusCode.Ok, new HtmlView(@"admin\deletegame", this.ViewData,req));
        }

        public IHttpResponse DeleteGamePost(IHttpRequest req)
        {
            var gameId = int.Parse(req.UrlParameters["id"]);

            games.DeleteGameFromDbByGivenId(gameId);

            return new RedirectResponse($"/allgames");
        }


        private bool isValidGameInformation(string title, string description, double size, decimal price, string imageUrl,
            string videoUrl)
        {
            var isValid = true;
            if (!TitleValidator.IsValid(title))
            {
                this.ViewData["<!--ErrorTitle-->"] = "<div class=\"col - md - 4\">" +
                         "<p class=\"text-danger\">Invalid Title. Has to begin with uppercase letter and" +
                         "has length between 3 and 100 symbols (inclusive)" +
                               "</div>";
                isValid = false;
            }

            if (description.Length < 20)
            {
                this.ViewData["<!--ErrorDescription-->"] = "<div class=\"col - md - 4\">" +
                         "<p class=\"text-danger\">Invalid Description! Must be at least 20 symbols" +
                               "</div>";
                isValid = false;
            }

            if (price < 0)
            {
                this.ViewData["<!--ErrorPrice-->"] = "<div class=\"col - md - 4\">" +
                         "<p class=\"text-danger\">Invalid Price! Must be a positive number" +
                               "</div>";
                isValid = false;
            }

            if (size < 0)
            {
                this.ViewData["<!--ErrorSize-->"] = "<div class=\"col - md - 4\">" +
                         "<p class=\"text-danger\">Invalid Size! Must be a positive number" +
                               "</div>";
                isValid = false;
            }

            if (string.IsNullOrEmpty(imageUrl) || string.IsNullOrWhiteSpace(imageUrl))
            {
                this.ViewData["<!--ErrorImageUrl-->"] = "<div class=\"col - md - 4\">" +
                         "<p class=\"text-danger\">Invalid Image URL! Is Required" +
                               "</div>";
                isValid = false;
            }

            if (videoUrl.Length != 11)
            {
                this.ViewData["<!--ErrorVideoUrl-->"] = "<div class=\"col - md - 4\">" +
                         "<p class=\"text-danger\">Invalid Video URL! Is always 11 characters" +
                               "</div>";
                isValid = false;
            }
            if (isValid)
            {
                return true;
            }

            return false;
        }
    }
}
