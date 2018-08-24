using Microsoft.AspNetCore.Mvc;
using NbaStore.Common.Constants;
using NbaStore.Common.ViewModels;
using NbaStore.Common.ViewModels.Admin.Products;
using NbaStore.Models;
using NbaStore.Services.Brands.Interfaces;
using NbaStore.Services.Products.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NbaStore.App.Controllers
{
    public class BrandsController:Controller
    {
        private readonly IProductsService productsService;
        private readonly IBrandsService brandsService;

        public BrandsController(IProductsService productsService,
            IBrandsService brandsService)
        {
            this.productsService = productsService;
            this.brandsService = brandsService;
        }

        public IActionResult Index(string[] brands, string gender, int page)
        {
            var productsCount = this.brandsService.GetProducts(brands,gender).ToList().Count;
            if (page <= 0 || page > productsCount)
            {
                page = 1;
            }
            var maxPage = (productsCount / 12) + 1;

            var skip = (page - 1) * 12;

            var products = this.brandsService.GetProducts(brands, gender)
                .Skip(skip)
                .Take(12);
            var model = this.CreateProductsModel(brands, gender,page, maxPage, nameof(Index), products);

            return View(model);
        }

        public IActionResult TopSellers(string[] brands, string gender, int page)
        {
            var productsCount = this.brandsService.GetTheMostSellableProducts(brands, gender).ToList().Count;
            if (page <= 0 || page > productsCount)
            {
                page = 1;
            }
            var maxPage = (productsCount / 12) + 1;

            var skip = (page - 1) * 12;

            var products = this.brandsService.GetTheMostSellableProducts(brands, gender)
                .Skip(skip)
                .Take(12);
            var model = this.CreateProductsModel(brands, gender, page, maxPage, nameof(TopSellers), products);

            return View(model);
        }

        public IActionResult PriceHighLow(string[] brands, string gender, int page)
        {
            var productsCount = this.brandsService.GetProductsOrderByPriceDescending(brands, gender).ToList().Count;
            if (page <= 0 || page > productsCount)
            {
                page = 1;
            }
            var maxPage = (productsCount / 12) + 1;

            var skip = (page - 1) * 12;

            var products = this.brandsService.GetProductsOrderByPriceDescending(brands, gender)
                .Skip(skip)
                .Take(12);
            var model = this.CreateProductsModel(brands, gender, page, maxPage, nameof(PriceHighLow), products);

            return View(model);
        }

        public IActionResult PriceLowHigh(string[] brands, string gender, int page)
        {
            var productsCount = this.brandsService.GetProductsOrderByPriceAscending(brands, gender).ToList().Count;
            if (page <= 0 || page > productsCount)
            {
                page = 1;
            }
            var maxPage = (productsCount / 12) + 1;

            var skip = (page - 1) * 12;

            var products = this.brandsService.GetProductsOrderByPriceAscending(brands, gender)
                .Skip(skip)
                .Take(12);
            var model = this.CreateProductsModel(brands, gender, page, maxPage, nameof(PriceLowHigh), products);

            return View(model);
        }

        public IActionResult DiscountHighLow(string[] brands, string gender, int page)
        {
            var productsCount = this.brandsService.GetProductsOrderByDiscountDescending(brands, gender).ToList().Count;
            if (page <= 0 || page > productsCount)
            {
                page = 1;
            }
            var maxPage = (productsCount / 12) + 1;

            var skip = (page - 1) * 12;

            var products = this.brandsService.GetProductsOrderByDiscountDescending(brands, gender)
                .Skip(skip)
                .Take(12);
            var model = this.CreateProductsModel(brands, gender, page, maxPage, nameof(DiscountHighLow), products);

            return View(model);
        }

        public IActionResult DiscountLowHigh(string[] brands, string gender, int page)
        {
            var productsCount = this.brandsService.GetProductsOrderByDiscountAscending(brands, gender).ToList().Count;
            if (page <= 0 || page > productsCount)
            {
                page = 1;
            }
            var maxPage = (productsCount / 12) + 1;

            var skip = (page - 1) * 12;

            var products = this.brandsService.GetProductsOrderByDiscountAscending(brands, gender)
                .Skip(skip)
                .Take(12);
            var model = this.CreateProductsModel(brands, gender, page, maxPage, nameof(DiscountLowHigh), products);

            return View(model);
        }

        private BrandsProductsViewModel CreateProductsModel(string[] brandsNames,string gender,
            int currentPage, int maxPage,
           string actionName, IEnumerable<ProductIndexViewModel> products)
        {
            var pageViewModel = new PagesViewModel()
            {
                CurrentPage = currentPage,
                MaxPage = maxPage,
                AreaName = "",
                ActionName = actionName,
                ControllerName = ControllersConstants.Brands
            };

            var teams = this.GetAllTeams();
            var brands = this.CreateBrandsViewModel(brandsNames);

            return new BrandsProductsViewModel()
            {
                Brands = brands,
                Teams = teams,
                Products = products,
                Page = pageViewModel,
                Gender=gender
            };
        }

        private IEnumerable<Team> GetAllTeams()
        {
            var teams = this.productsService.GetAllTeams();

            return teams;
        }

        private IEnumerable<AllBrandsViewModel> CreateBrandsViewModel(string[] brandsNames)
        {
            var allBrands = this.productsService.GetAllBrands();

            foreach(var brand in allBrands)
            {
                if (brandsNames.Contains(brand.Name))
                {
                    brand.IsChecked = true;
                }
            }

            return allBrands;
        }
    }
}
