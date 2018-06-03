using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebServer.Application.Data;

namespace WebServer.Application.Models
{
    using WebServer.Application.Data.Models;

    public class UserService
    {
        private string username;
        private string password;

        public void Create(string username, string password)
        {
            this.username = username;
            this.password = password;
        }

        public bool Success()
        {
            using (var db = new ByTheCakeDbContext())
            {
                if (db.Users.Any(x => x.Username == username))
                {
                    return false;
                }

                var user = new User();
                user.Username = username;

                password= BCrypt.Net.BCrypt.HashPassword(password);
                user.Password = password;
                user.DateOfRegistration = DateTime.UtcNow;

                db.Add(user);
                db.SaveChanges();

                return true;
            }
        }

        public List<string> GetProfile(string username)
        {
            var result = new List<string>();
            using (var db = new ByTheCakeDbContext())
            {
                var user = db.Users.SingleOrDefault(x => x.Username == username);

                result.Add(user.Username);
                result.Add(user.DateOfRegistration.ToString());

                var ordersCount = db.Users.Where(x => x.Username == user.Username)
                    .Select(x => new
                    {
                        OrdersCount = x.Orders.Count
                    })
                    .FirstOrDefault();
                
                result.Add(ordersCount.OrdersCount.ToString());
            }

            return result;
        }

        public bool Find(string username,string password)
        {
            using (var db = new ByTheCakeDbContext())
            {
                var user = db.Users.SingleOrDefault(x => x.Username == username);

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
    }
}
