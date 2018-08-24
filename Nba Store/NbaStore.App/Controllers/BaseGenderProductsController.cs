using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NbaStore.Common.ViewModels;
using NbaStore.Common.ViewModels.Admin.Products;
using NbaStore.Models;
using NbaStore.Services.Products.Interfaces;

namespace NbaStore.App.Controllers
{
    public abstract class BaseGenderProductsController : Controller
    {
        private string controllerName;

        protected BaseGenderProductsController(
            IProductsService productsService, string controllerName)
        {
            this.controllerName = controllerName;
            this.ProductsService = productsService;
        }
        
        protected IProductsService ProductsService { get; private set; }

        protected ProductsViewModel Index(int id,string gender)
        {
            var page = id;
            var productsCount = this.ProductsService.GetProducts(gender).ToList().Count;
            if (page <= 0 || page > productsCount)
            {
                page = 1;
            }
            var maxPage = (productsCount / 12) + 1;

            var skip = (page - 1) * 12;

            var products = this.ProductsService.GetProducts(gender)
                .Skip(skip)
                .Take(12);

            var model = this.CreateProductsModel(page, maxPage, nameof(Index), products);

            return model;
        }

        protected ProductsViewModel PriceHighLow(int id, string gender)
        {
            var page = id;
            var productsCount = this.ProductsService.GetProductsOrderByPriceDescending(gender)
                .ToList().Count;
            if (page <= 0 || page > productsCount)
            {
                page = 1;
            }
            var maxPage = (productsCount / 12) + 1;

            var skip = (page - 1) * 12;

            var products = this.ProductsService.GetProductsOrderByPriceDescending(gender)
                .Skip(skip)
                .Take(12);

            var model = this.CreateProductsModel(page, maxPage, nameof(PriceHighLow), products);

            return model;
        }

        protected ProductsViewModel PriceLowHigh(int id, string gender)
        {
            var page = id;
            var productsCount = this.ProductsService.GetProductsOrderByPriceAscending(gender)
                .ToList().Count;
            if (page <= 0 || page > productsCount)
            {
                page = 1;
            }
            var maxPage = (productsCount / 12) + 1;

            var skip = (page - 1) * 12;

            var products = this.ProductsService.GetProductsOrderByPriceAscending(gender)
                .Skip(skip)
                .Take(12);

            var model = this.CreateProductsModel(page, maxPage, nameof(PriceLowHigh), products);

            return model;
        }

        protected ProductsViewModel DiscountHighLow(int id, string gender)
        {
            var page = id;
            var productsCount = this.ProductsService.GetProductsOrderByDiscountDescending(gender)
                .ToList().Count;
            if (page <= 0 || page > productsCount)
            {
                page = 1;
            }
            var maxPage = (productsCount / 12) + 1;

            var skip = (page - 1) * 12;

            var products = this.ProductsService.GetProductsOrderByDiscountDescending(gender)
                .Skip(skip)
                .Take(12);

            var model = this.CreateProductsModel(page, maxPage, nameof(DiscountHighLow), products);

            return model;
        }

        protected ProductsViewModel DiscountLowHigh(int id, string gender)
        {
            var page = id;
            var productsCount = this.ProductsService.GetProductsOrderByDiscountAscending(gender)
                .ToList().Count;
            if (page <= 0 || page > productsCount)
            {
                page = 1;
            }
            var maxPage = (productsCount / 12) + 1;

            var skip = (page - 1) * 12;

            var products = this.ProductsService.GetProductsOrderByDiscountAscending(gender)
                .Skip(skip)
                .Take(12);

            var model = this.CreateProductsModel(page, maxPage, nameof(DiscountLowHigh), products);

            return model;
        }

        protected ProductsViewModel TopSellers(int id, string gender)
        {
            var page = id;
            var productsCount = this.ProductsService.GetTheMostSellableProducts(gender)
                .ToList().Count;
            if (page <= 0 || page > productsCount)
            {
                page = 1;
            }
            var maxPage = (productsCount / 12) + 1;

            var skip = (page - 1) * 12;

            var products = this.ProductsService.GetTheMostSellableProducts(gender)
                .Skip(skip)
                .Take(12);

            var model = this.CreateProductsModel(page, maxPage, nameof(TopSellers), products);

            return model;
        }

        private IEnumerable<Team> GetAllTeams()
        {
            var teams = this.ProductsService.GetAllTeams();

            return teams;
        }

        private IEnumerable<AllBrandsViewModel> GetAllBrands()
        {
            var brands = this.ProductsService.GetAllBrands();

            return brands;
        }

        private ProductsViewModel CreateProductsModel(int currentPage, int maxPage,
           string actionName, IEnumerable<ProductIndexViewModel> products)
        {
            var pageViewModel = new PagesViewModel()
            {
                CurrentPage = currentPage,
                MaxPage = maxPage,
                AreaName = "",
                ActionName = actionName,
                ControllerName = controllerName
            };

            var teams = this.GetAllTeams();
            var brands = this.GetAllBrands();

            return new ProductsViewModel()
            {
                Brands=brands,
                Teams = teams,
                Products = products,
                Page = pageViewModel
            };
        }
    }
}