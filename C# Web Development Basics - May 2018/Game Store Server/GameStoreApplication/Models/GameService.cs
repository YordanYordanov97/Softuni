using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebServer.GameStoreApplication.Data;
using WebServer.GameStoreApplication.Data.Models;

namespace WebServer.GameStoreApplication.Models
{
    public class GameService
    {
        public List<Game> GetAllGamesFromDb()
        {
            using(var db=new GameStoreDbContext())
            {
                return db.Games.ToList();
            }
        }

        public void AddGameToDb(string title,string description,double size,decimal price,string imageUrl,
            string videoUrl,DateTime dateTime)
        {
            using (var db = new GameStoreDbContext())
            {
                var game = new Game();
                game.Title = title;
                game.Description = description;
                game.Size = size;
                game.Price = price;
                game.ImageUrl = imageUrl;
                game.Trailer = videoUrl;
                game.ReleaseDate = dateTime;

                db.Games.Add(game);
                db.SaveChanges();
            }
        }

        public Game GetGameByGivenId(int id)
        {
            using (var db = new GameStoreDbContext())
            {
                var game = db.Games.SingleOrDefault(x => x.Id == id);

                return game;
            }
        }

        public void EditGame(int id,string title, string description, double size, decimal price, string imageUrl,
            string videoUrl, DateTime dateTime)
        {
            using (var db = new GameStoreDbContext())
            {
                var game = db.Games.SingleOrDefault(x => x.Id == id);
                game.Title = title;
                game.Description = description;
                game.Size = size;
                game.Price = price;
                game.ImageUrl = imageUrl;
                game.Trailer = videoUrl;
                game.ReleaseDate = dateTime;
                
                db.SaveChanges();
            }
        }

        public void DeleteGameFromDbByGivenId(int id)
        {
            using (var db = new GameStoreDbContext())
            {
                var game = db.Games.SingleOrDefault(x => x.Id == id);

                db.Games.Remove(game);
                db.SaveChanges();
            }
        }
    }
}
