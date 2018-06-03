using System;
using System.Collections.Generic;
using System.Text;

namespace WebServer.GameStoreApplication.Data.Models
{
    public class UserGame
    {
        public int UserId { get; set; }

        public User User { get; set; }

        public int GameId { get; set; }

        public Game Game { get; set; }
    }
}
