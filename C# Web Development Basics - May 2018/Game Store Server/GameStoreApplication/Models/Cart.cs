using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebServer.GameStoreApplication.Data;
using WebServer.GameStoreApplication.Data.Models;

namespace WebServer.GameStoreApplication.Models
{
    public class Cart
    {
        private List<Game> Games;

        public Cart()
        {
            this.Games = new List<Game>();
        }

        public void AddGame(int id)
        {
            using (var db = new GameStoreDbContext())
            {
                var game = db.Games.SingleOrDefault(x => x.Id == id);

                this.Games.Add(game);
            }
        }

        public List<Game> GetAllProducts()
        {
            return this.Games;
        }

        public int ProductsCount()
        {
            return this.Games.Count;
        }

        public void RemoveProduct(int id)
        {
            using (var db = new GameStoreDbContext())
            {
                var indexToRemove = 0;

                for (int i = 0; i < this.Games.Count; i++)
                {
                    var game = this.Games[i];

                    if (game.Id == id)
                    {
                        indexToRemove = i;

                        break;
                    }
                }

                this.Games.RemoveAt(indexToRemove);
            }
        }

        public void Clear()
        {
            this.Games.Clear();
        }

    }
}
