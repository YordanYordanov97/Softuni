
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebServer.GameStoreApplication.Data;
using WebServer.GameStoreApplication.Data.Models;

namespace WebServer.GameStoreApplication.Models
{
    public class OrderService
    {
        public void SaveOrderProductsInDb(List<Game> products, string userEmail)
        {
            using (var db = new GameStoreDbContext())
            {
                var user = db.Users.SingleOrDefault(x => x.Email == userEmail);
                var userId = user.Id;
                
                foreach(var product in products)
                {
                    var currentProduct = db.Games.SingleOrDefault(x => x.Id == product.Id);
                    var userGame = new UserGame();
                    userGame.UserId = userId;
                    userGame.GameId = currentProduct.Id;

                    db.UserGames.Add(userGame);
                }

                db.SaveChanges();
            }
        }
    }
}
