using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebServer.GameStoreApplication.Controllers;
using WebServer.GameStoreApplication.Data;
using WebServer.Server.Routing.Contracts;

namespace WebServer.GameStoreApplication
{
    public class GameStoreApp
    {
        public void InitializeDatabase()
        {
            using (var db = new GameStoreDbContext())
            {
                db.Database.Migrate();
            }
        }

        public void Configure(IAppRouteConfig appRouteConfig)
        {
            appRouteConfig.Get(
             "/ownedgames",
             req => new StoreController().OwnedGames(req));

            appRouteConfig.Get(
             "/successorder",
             req => new StoreController().SuccessOrder(req));

            appRouteConfig.Get(
             "/cart",
             req => new StoreController().CartGet(req));

            appRouteConfig.Post(
             "/cart",
             req => new StoreController().CartPost(req));

            appRouteConfig.Get(
             "/info/{(?<id>[0-9]+)}",
             req => new HomeController().Info(req));

            appRouteConfig.Post(
             "/info/{(?<id>[0-9]+)}",
             req => new HomeController().Info(req));

            appRouteConfig.Get(
               "/store",
               req => new StoreController().ShowProducts(req));

            appRouteConfig.Post(
               "/store",
               req => new StoreController().ShowProducts(req));

            appRouteConfig.Get(
              "/",
              req => new HomeController().Home(req));

            appRouteConfig.Get(
             "/deletegame/{(?<id>[0-9]+)}",
             req => new AdminController().DeleteGameGet(req));

            appRouteConfig.Post(
             "/deletegame/{(?<id>[0-9]+)}",
             req => new AdminController().DeleteGamePost(req));

            appRouteConfig.Get(
             "/editgame/{(?<id>[0-9]+)}",
             req => new AdminController().EditGame(req));

            appRouteConfig.Post(
             "/editgame/{(?<id>[0-9]+)}",
             req => new AdminController().EditGame(req));

            appRouteConfig.Get(
              "/addgame",
              req => new AdminController().AddGameGet(req));

            appRouteConfig.Post(
              "/addgame",
              req => new AdminController().AddGamePost(req));

            appRouteConfig.Get(
              "/allgames",
              req => new AdminController().AllGames(req));

            appRouteConfig.Get(
              "/register",
              req => new AccountController().RegisterGet(req));

            appRouteConfig.Post(
              "/register",
              req => new AccountController().RegisterPost(req));

            appRouteConfig.Get(
             "/login",
             req => new AccountController().LogInGet(req));

            appRouteConfig.Post(
              "/login",
              req => new AccountController().LogInPost(req));

            appRouteConfig.Get(
              "/logout",
              req => new AccountController().LogOut(req));

        }
    }
}
