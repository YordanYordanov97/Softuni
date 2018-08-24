using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NbaStore.Common.Constants;
using NbaStore.Common.ViewModels;
using NbaStore.Common.ViewModels.Admin.Teams;
using NbaStore.Models;
using NbaStore.Services.Products.Interfaces;
using NbaStore.Services.TeamProducts.Interfaces;

namespace NbaStore.App.Controllers
{
    public class LadiesTeamProductsController : BaseGenderTeamProductsController
    {
        public LadiesTeamProductsController(
            ITeamProductsServices teamProductsServices,
            IProductsService productsService)
            : base(teamProductsServices, productsService, ControllersConstants.LadiesTeamProducts)
        {
        }

        public IActionResult Index(int id, int page)
        {
            var model = this.Index(id, page, ControllersConstants.Ladies);

            return View(model);
        }

        public IActionResult PriceHighLow(int id, int page)
        {
            var model = this.PriceHighLow(id, page, ControllersConstants.Ladies);

            return View(model);
        }

        public IActionResult PriceLowHigh(int id, int page)
        {
            var model = this.PriceLowHigh(id, page, ControllersConstants.Ladies);

            return View(model);
        }

        public IActionResult DiscountHighLow(int id, int page)
        {
            var model = this.DiscountHighLow(id, page, ControllersConstants.Ladies);

            return View(model);
        }

        public IActionResult DiscountLowHigh(int id, int page)
        {
            var model = this.DiscountLowHigh(id, page, ControllersConstants.Ladies);

            return View(model);
        }
    }
}