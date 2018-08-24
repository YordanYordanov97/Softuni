using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NbaStore.App.Filters;
using NbaStore.Common.BindingModels.Admin.Teams;
using NbaStore.Common.Constants.AreaAdmin;
using NbaStore.Common.ViewModels.Admin.Teams;
using NbaStore.Services.Admin.Interfaces;

namespace NbaStore.App.Areas.Admin.Controllers
{
    public class TeamsController : AdminController
    {
        private readonly IAdminTeamsService adminTeamsService;

        public TeamsController(IAdminTeamsService adminTeamsService)
        {
            this.adminTeamsService = adminTeamsService;
        }

        public IActionResult Index(int id)
        { 
            var page = id;
            var teamsCount = this.adminTeamsService.GetTeams().ToList().Count;
            if (page <= 0 || page> teamsCount)
            {
                page = 1;
            }
            ViewData[AdminConstants.PagesCount] = (teamsCount / 4) + 1;
            ViewData[AdminConstants.CurrentPage] = page;
            var skip = (page - 1) * 4;

            var model = this.adminTeamsService.GetTeams()
                .Skip(skip)
                .Take(4);
            return View(model);
        }
        
        public async Task<IActionResult> Details(int id,int page)
        {
            var model = await this.adminTeamsService.GetDetails(id);
            var productsCount = model.Products.ToList().Count;
            if (page <= 0 || page > productsCount)
            {
                page = 1;
            }
            ViewData[AdminConstants.PagesCount] = (productsCount / 6) + 1;
            ViewData[AdminConstants.CurrentPage] = page;
            ViewData[AdminConstants.RouteId] = id;
            var skip = (page - 1) * 6;

            var products = model.Products
                .Skip(skip)
                .Take(6)
                .ToList();

            model.Products = products;

            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        [ValidationModel]
        public async Task<IActionResult> Create(TeamBindingModel model)
        {
            await this.adminTeamsService.SaveTeam(model);

            this.TempData[AdminConstants.MessageType] = AdminConstants.Success;
            this.TempData[AdminConstants.Message] = string.Format(AdminConstants.SuccessfullyAdd,
                AdminConstants.Team);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var model = await this.adminTeamsService.GetTeam(id);
            return View(model);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        [ValidationModel]
        public async Task<IActionResult> Edit(int id, TeamBindingModel model)
        {
            await this.adminTeamsService.EditTeam(id, model);

            this.TempData[AdminConstants.MessageType] = AdminConstants.Success;
            this.TempData[AdminConstants.Message] = string.Format(AdminConstants.SuccessfullyEdit,
                AdminConstants.Team);

            return RedirectToAction(AdminConstants.ActionDetails, new { id = id });
        }

        public IActionResult Delete(int id)
        {
            this.ViewData[AdminConstants.TeamId] = id;

            return View();
        }

        public async Task<IActionResult> ConfirmDelete(int id)
        {
            await this.adminTeamsService.DeleteTeam(id);

            this.TempData[AdminConstants.MessageType] = AdminConstants.Success;
            this.TempData[AdminConstants.Message] = string.Format(AdminConstants.SuccessfullyDelete,
                AdminConstants.Team);

            return RedirectToAction(nameof(Index));
        }
    }
}