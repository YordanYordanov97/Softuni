using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NbaStore.Common.Constants;
using NbaStore.Common.ViewModels;
using NbaStore.Common.ViewModels.Admin.Products;
using NbaStore.Models;
using NbaStore.Services.Products.Interfaces;

namespace NbaStore.App.Controllers
{
    public class LadiesController : BaseGenderProductsController
    {
        public LadiesController(IProductsService productsService)
            : base(productsService, ControllersConstants.Ladies)
        {
        }

        public IActionResult Index(int id)
        {
            var model = this.Index(id, ControllersConstants.Ladies);

            return View(model);
        }

        [HttpPost]
        public IActionResult SelectBrand(string[] brands)
        {
            if (brands.Count() == 0)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index), ControllersConstants.Brands,
                new { brands = brands, gender = ControllersConstants.Ladies });
        }

        public IActionResult PriceHighLow(int id)
        {
            var model = this.PriceHighLow(id, ControllersConstants.Ladies);

            return View(model);
        }

        public IActionResult PriceLowHigh(int id)
        {
            var model = this.PriceLowHigh(id, ControllersConstants.Ladies);

            return View(model);
        }

        public IActionResult DiscountHighLow(int id)
        {
            var model = this.DiscountHighLow(id, ControllersConstants.Ladies);

            return View(model);
        }

        public IActionResult DiscountLowHigh(int id)
        {
            var model = this.DiscountLowHigh(id, ControllersConstants.Ladies);

            return View(model);
        }

        public IActionResult TopSellers(int id)
        {
            var model = this.TopSellers(id, ControllersConstants.Ladies);

            return View(model);
        }
    }
}