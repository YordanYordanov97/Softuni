using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NbaStore.App.Infrastructure;
using NbaStore.App.Infrastructure.Validations;
using NbaStore.Common.Constants;
using NbaStore.Common.Constants.AreaAdmin;
using NbaStore.Common.ViewModels.Admin.Products;
using NbaStore.Services.Admin.Interfaces;

namespace NbaStore.App.Pages
{
    public class ProductInfoModel : PageModel
    {
        private readonly IAdminProductsService productsService;
        private readonly ProductCountToBuy productCountToBuy;

        public ProductInfoModel(IAdminProductsService productsService,ProductCountToBuy productCountToBuy)
        {
            this.productsService = productsService;
            this.productCountToBuy = productCountToBuy;
        }

        public int Quantity { get; set; } = 1;

        public Dictionary<int, string> SelectSizes { get; set; } = new Dictionary<int, string>()
        {
            {1, "Small"},
            {2,"Medium"},
            {3, "Large"},
            {4,"XXL"},
        };

        [BindProperty]
        public int SizeId { get; set; }

        public ProductDetailsViewModel ProductModel { get; set; }

        public async Task OnGet(int id)
        {
            this.productCountToBuy.Number = 1;
            this.ProductModel = await this.productsService.GetDetails(id);
        }

        public async Task <IActionResult> OnPost(int id)
        {
            if (this.SizeId<=0 || this.SizeId>4)
            {
                this.ModelState.AddModelError(string.Empty, PagesConstants.SelectSize);
                this.ProductModel = await this.productsService.GetDetails(id);
                return this.Page();
            }

            var size = this.SelectSizes[this.SizeId];
            await this.productsService.SetSizeToProductById(id, size);

            this.TempData[AdminConstants.MessageType] = AdminConstants.Success;
            this.TempData[AdminConstants.Message] = PagesConstants.AddProductToCart;
            this.Quantity = productCountToBuy.Number;

            return RedirectToAction(PagesConstants.AddToCart, PagesConstants.ShoppingCart, new { id = id, quantity = this.Quantity, size= size });
        }

        public async Task OnGetIncreaseCount(int id)
        {
            this.ProductModel = await this.productsService.GetDetails(id);

            this.productCountToBuy.Increase();
            this.Quantity = this.productCountToBuy.Number;
        }

        public async Task OnGetDecreaseCount(int id)
        {
            this.ProductModel = await this.productsService.GetDetails(id);

            this.productCountToBuy.Decrease();
            this.Quantity = this.productCountToBuy.Number;
        }
    }
}