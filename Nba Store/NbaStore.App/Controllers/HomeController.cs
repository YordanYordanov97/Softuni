using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NbaStore.App.Models;
using NbaStore.Common.Constants;
using NbaStore.Services.Products.Interfaces;

namespace NbaStore.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductsService productsService;

        public HomeController(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Search(string searchTerm)
        {
            this.ViewData[ControllersConstants.SearchResult] = searchTerm;
            var model = this.productsService.GetProductsBySearchTerm(searchTerm);

            return View(model);
        }
        
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
