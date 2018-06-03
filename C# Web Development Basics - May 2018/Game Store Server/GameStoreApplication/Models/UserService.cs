using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebServer.GameStoreApplication.Data;
using WebServer.GameStoreApplication.Data.Models;

namespace WebServer.GameStoreApplication.Models
{
    public class UserService
    {
        public bool IsAlreadyHaveUserWithThisEmail(string email)
        {
            using (var db = new GameStoreDbContext())
            {
                if (db.Users.Any(x => x.Email == email))
                {
                    return true;
                }

                return false;
            }
        }
        public void SaveInformationInDb(string email,string fullName,string password,bool isAdmin)
        {
            using(var db=new GameStoreDbContext())
            {
                var user = new User();
                user.Email = email;
                user.FullName = fullName;
                password = BCrypt.Net.BCrypt.HashPassword(password);
                user.Password = password;
                user.Password = password;
                user.IsAdmin = isAdmin;

                db.Users.Add(user);
                db.SaveChanges();
            }
        }

        public bool IsAdmin(string email, string password)
        {
            using (var db = new GameStoreDbContext())
            {
                var user = db.Users.SingleOrDefault(x => x.Email == email);
                
                if (!user.IsAdmin)
                {
                    return false;
                }

                return true;
            }
        }

        public bool Find(string email, string password)
        {
            using (var db = new GameStoreDbContext())
            {
                var user = db.Users.SingleOrDefault(x => x.Email == email);

                if (user == null)
                {
                    return false;
                }

                bool validPassword = BCrypt.Net.BCrypt.Verify(password, user.Password);

                if (!validPassword)
                {
                    return false;
                }

                return true;
            }
        }

        public List<Game> OwnedGames(string userEmail)
        {
            using (var db = new GameStoreDbContext())
            {
                var user = db.Users.SingleOrDefault(x => x.Email == userEmail);
                var userId = user.Id;

                var userGames = db.UserGames.Where(x => x.UserId == userId).ToList();
                var games = new List<Game>();

                foreach(var userGame in userGames)
                {
                    var game = db.Games.SingleOrDefault(x => x.Id == userGame.GameId);
                    games.Add(game);
                }
                
                return games;
            }
        }
    }
}
