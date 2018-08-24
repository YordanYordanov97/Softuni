using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NbaStore.Common.ViewModels;
using NbaStore.Common.ViewModels.Admin.Teams;
using NbaStore.Models;
using NbaStore.Services.Products.Interfaces;
using NbaStore.Services.TeamProducts.Interfaces;

namespace NbaStore.App.Controllers
{
    public abstract class BaseGenderTeamProductsController : Controller
    {
        private string controllerName;

        protected BaseGenderTeamProductsController(
            ITeamProductsServices teamProductsServices,
            IProductsService productsService, 
            string controllerName)
        {
            this.controllerName = controllerName;
            this.TeamProductsServices = teamProductsServices;
            this.ProductsService = productsService;
        }

        protected IProductsService ProductsService { get; private set; }

        protected ITeamProductsServices TeamProductsServices { get; private set; }

        protected TeamProductsViewModel Index(int id, int page,string gender)
        {
            var teams = this.GetAllTeams();
            var productsCount = this.TeamProductsServices.GetTeamWithProducts(id, gender)
                .Products.ToList().Count;
            if (page <= 0 || page > productsCount)
            {
                page = 1;
            }
            var maxPage = (productsCount / 12) + 1;

            var skip = (page - 1) * 12;

            var teamWithProducts = this.TeamProductsServices.GetTeamWithProducts(id, gender);
            teamWithProducts.Products = teamWithProducts.Products
                .Skip(skip)
                .Take(12)
                .ToList();

            var model = this.TeamProductsViewModel(id, page, maxPage, nameof(Index), teamWithProducts);

            return model;
        }

        protected TeamProductsViewModel PriceHighLow(int id, int page, string gender)
        {
            var teams = this.GetAllTeams();
            var productsCount = this.TeamProductsServices.GetTeamWithProductsOrderByPriceDescending(id, gender)
                .Products.ToList().Count;
            if (page <= 0 || page > productsCount)
            {
                page = 1;
            }
            var maxPage = (productsCount / 12) + 1;

            var skip = (page - 1) * 12;

            var teamWithProducts = this.TeamProductsServices.GetTeamWithProductsOrderByPriceDescending(id, gender);
            teamWithProducts.Products = teamWithProducts.Products
                .Skip(skip)
                .Take(12)
                .ToList();

            var model = this.TeamProductsViewModel(id, page, maxPage, nameof(PriceHighLow), teamWithProducts);

            return model;
        }

        protected TeamProductsViewModel PriceLowHigh(int id, int page, string gender)
        {
            var teams = this.GetAllTeams();
            var productsCount = this.TeamProductsServices.GetTeamWithProductsOrderByPriceAscending(id, gender)
                .Products.ToList().Count;
            if (page <= 0 || page > productsCount)
            {
                page = 1;
            }
            var maxPage = (productsCount / 12) + 1;

            var skip = (page - 1) * 12;

            var teamWithProducts = this.TeamProductsServices.GetTeamWithProductsOrderByPriceAscending(id, gender);
            teamWithProducts.Products = teamWithProducts.Products
                .Skip(skip)
                .Take(12)
                .ToList();

            var model = this.TeamProductsViewModel(id, page, maxPage, nameof(PriceLowHigh), teamWithProducts);

            return model;
        }

        protected TeamProductsViewModel DiscountHighLow(int id, int page, string gender)
        {
            var teams = this.GetAllTeams();
            var productsCount = this.TeamProductsServices.GetTeamWithProductsOrderByDiscountDescending(id, gender)
                .Products.ToList().Count;
            if (page <= 0 || page > productsCount)
            {
                page = 1;
            }
            var maxPage = (productsCount / 12) + 1;

            var skip = (page - 1) * 12;

            var teamWithProducts = this.TeamProductsServices.GetTeamWithProductsOrderByDiscountDescending(id, gender);
            teamWithProducts.Products = teamWithProducts.Products
                .Skip(skip)
                .Take(12)
                .ToList();

            var model = this.TeamProductsViewModel(id, page, maxPage, nameof(DiscountHighLow), teamWithProducts);

            return model;
        }

        protected TeamProductsViewModel DiscountLowHigh(int id, int page, string gender)
        {
            var teams = this.GetAllTeams();
            var productsCount = this.TeamProductsServices.GetTeamWithProductsOrderByDiscountAscending(id, gender)
                .Products.ToList().Count;
            if (page <= 0 || page > productsCount)
            {
                page = 1;
            }
            var maxPage = (productsCount / 12) + 1;

            var skip = (page - 1) * 12;

            var teamWithProducts = this.TeamProductsServices.GetTeamWithProductsOrderByDiscountAscending(id, gender);
            teamWithProducts.Products = teamWithProducts.Products
                .Skip(skip)
                .Take(12)
                .ToList();

            var model = this.TeamProductsViewModel(id, page, maxPage, nameof(DiscountLowHigh), teamWithProducts);

            return model;
        }
        
        private IEnumerable<Team> GetAllTeams()
        {
            var teams = this.ProductsService.GetAllTeams();

            return teams;
        }

        private TeamProductsViewModel TeamProductsViewModel(int id, int currentPage, int maxPage,
            string actionName, TeamDetailsViewModel teamDetailsViewModel)
        {
            var pageViewModel = new PagesViewModel()
            {
                CurrentPage = currentPage,
                MaxPage = maxPage,
                AreaName = "",
                ActionName = actionName,
                ControllerName = controllerName,
                RouteId = id,
            };

            var teams = this.GetAllTeams();

            return new TeamProductsViewModel()
            {
                Teams = teams,
                TeamWithProducts = teamDetailsViewModel,
                Page = pageViewModel
            };
        }
    }
}