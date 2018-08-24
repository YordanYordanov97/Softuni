using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NbaStore.App.Filters;
using NbaStore.Common.BindingModels.Admin.Images;
using NbaStore.Common.Constants.AreaAdmin;
using NbaStore.Services.Admin.Interfaces;

namespace NbaStore.App.Areas.Admin.Controllers
{
    public class ImagesController : AdminController
    {
        private readonly IAdminImagesService adminImagesService;

        public ImagesController(IAdminImagesService adminImagesService)
        {
            this.adminImagesService = adminImagesService;
        }

        public IActionResult Create(int id)
        {
            this.ViewData[AdminConstants.ProductId] = id;

            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        [ValidationModel]
        public async Task<IActionResult> Create(int id, ImageBindingModel model)
        {
            await this.adminImagesService.SaveImage(model, id);

            this.TempData[AdminConstants.MessageType] = AdminConstants.Success;
            this.TempData[AdminConstants.Message] = string.Format(AdminConstants.SuccessfullyAdd,
                AdminConstants.Image);

            return RedirectToAction(AdminConstants.ActionDetails, AdminConstants.ActionProducts, 
                new { id = id });
        }

        public async Task<IActionResult> Edit(int productId, int imageId)
        {
            this.ViewData[AdminConstants.ProductId] = productId;
            var model = await this.adminImagesService.GetImage(imageId);

            return View(model);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        [ValidationModel]
        public async Task<IActionResult> Edit(int productId, int imageId, ImageBindingModel model)
        {
            await this.adminImagesService.EditProduct(imageId, model);

            this.TempData[AdminConstants.MessageType] = AdminConstants.Success;
            this.TempData[AdminConstants.Message] = string.Format(AdminConstants.SuccessfullyEdit,
                AdminConstants.Image);

            return RedirectToAction(AdminConstants.ActionDetails, AdminConstants.ActionProducts,
                new { id = productId });
        }

        public IActionResult Delete(int productId, int imageId)
        {
            this.ViewData[AdminConstants.ImageId] = imageId;
            this.ViewData[AdminConstants.ProductId] = productId;
            return View();
        }

        public async Task<IActionResult> ConfirmDelete(int productId, int imageId)
        {
            await this.adminImagesService.DeleteProduct(imageId);

            this.TempData[AdminConstants.MessageType] = AdminConstants.Success;
            this.TempData[AdminConstants.Message] = string.Format(AdminConstants.SuccessfullyDelete,
               AdminConstants.Image);

            return RedirectToAction(AdminConstants.ActionDetails, AdminConstants.ActionProducts,
                new { id = productId });
        }
    }
}