using Microsoft.AspNetCore.Mvc;
using NbaStore.Common.Constants;
using NbaStore.Common.ViewModels;
using NbaStore.Common.ViewModels.Admin.Products;
using NbaStore.Models;
using NbaStore.Services.Products.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NbaStore.App.Controllers
{
    public class MensController:BaseGenderProductsController
    {
        public MensController(IProductsService productsService)
            : base(productsService, ControllersConstants.Mens)
        {
        }
        
        public IActionResult Index(int id)
        {
            var model = this.Index(id, ControllersConstants.Mens);

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
                new { brands = brands, gender = ControllersConstants.Mens });
        }

        public IActionResult PriceHighLow(int id)
        {
            var model = this.PriceHighLow(id, ControllersConstants.Mens);

            return View(model);
        }

        public IActionResult PriceLowHigh(int id)
        {
            var model = this.PriceLowHigh(id, ControllersConstants.Mens);

            return View(model);
        }

        public IActionResult DiscountHighLow(int id)
        {
            var model = this.DiscountHighLow(id, ControllersConstants.Mens);

            return View(model);
        }

        public IActionResult DiscountLowHigh(int id)
        {
            var model = this.DiscountLowHigh(id, ControllersConstants.Mens);

            return View(model);
        }

        public IActionResult TopSellers(int id)
        {
            var model = this.TopSellers(id, ControllersConstants.Mens);

            return View(model);
        }
    }
}
