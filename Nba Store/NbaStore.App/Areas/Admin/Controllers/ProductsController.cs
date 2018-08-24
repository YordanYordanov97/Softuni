using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NbaStore.App.Filters;
using NbaStore.Common.BindingModels.Admin.Products;
using NbaStore.Common.Constants.AreaAdmin;
using NbaStore.Services.Admin.Interfaces;

namespace NbaStore.App.Areas.Admin.Controllers
{
    public class ProductsController : AdminController
    {
        private readonly IAdminProductsService adminProductsService;

        public ProductsController(IAdminProductsService adminProductsService)
        {
            this.adminProductsService = adminProductsService;
        }

        public IActionResult Index(int id)
        {
            var page = id;
            var teamsCount = this.adminProductsService.GetProducts().ToList().Count;
            if (page <= 0 || page > teamsCount)
            {
                page = 1;
            }
            ViewData[AdminConstants.PagesCount] = (teamsCount / 6) + 1;
            ViewData[AdminConstants.CurrentPage] = page;
            var skip = (page - 1) * 6;

            var model = this.adminProductsService.GetProducts()
                .Skip(skip)
                .Take(6);

            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var model = await this.adminProductsService.GetDetails(id);

            return View(model);
        }

        public IActionResult Create()
        {
            var model = this.adminProductsService.GetBindingModel();
            return View(model);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidationModel]
        public async Task<IActionResult> Create(ProductBindingModel model)
        {
            await this.adminProductsService.SaveProduct(model);

            this.TempData[AdminConstants.MessageType] = AdminConstants.Success;
            this.TempData[AdminConstants.Message] = string.Format(AdminConstants.SuccessfullyAdd,
                AdminConstants.Product);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var model = await this.adminProductsService.GetProduct(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidationModel]
        public async Task<IActionResult> Edit(int id, ProductBindingModel model)
        {
            await this.adminProductsService.EditProduct(id, model);

            this.TempData[AdminConstants.MessageType] = AdminConstants.Success;
            this.TempData[AdminConstants.Message] = string.Format(AdminConstants.SuccessfullyEdit,
                AdminConstants.Product);

            return RedirectToAction(AdminConstants.ActionDetails, new { id = id });
        }

        public IActionResult Delete(int id)
        {
            this.ViewData[AdminConstants.ProductId] = id;

            return View();
        }

        public async Task<IActionResult> ConfirmDelete(int id)
        {
            await this.adminProductsService.DeleteProduct(id);

            this.TempData[AdminConstants.MessageType] = AdminConstants.Success;
            this.TempData[AdminConstants.Message] = string.Format(AdminConstants.SuccessfullyDelete,
                AdminConstants.Product);

            return RedirectToAction(nameof(Index));
        }
    }
}