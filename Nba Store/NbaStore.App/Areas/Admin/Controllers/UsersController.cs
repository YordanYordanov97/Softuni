using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NbaStore.Services.Admin.Interfaces;
using NbaStore.Common.BindingModels.Admin.Users;
using NbaStore.App.Filters;
using NbaStore.Common.Constants.AreaAdmin;

namespace NbaStore.App.Areas.Admin.Controllers
{
    public class UsersController : AdminController
    {
       private readonly IAdminUsersService adminUsersService;

        public UsersController(IAdminUsersService adminUsersService)
        {
            this.adminUsersService = adminUsersService;
        }
        
        public async Task<IActionResult> Index()
        {
            var model = await this.adminUsersService.GetUsers(this.User);
            return View(model);
        }
        
        public async Task<IActionResult> Details(string id)
        {
            var model = await this.adminUsersService.GetUserDatails(id);
            return View(model);
        }
        
        public IActionResult ChangePassword(string id)
        {
            return View();
        }
        
        [HttpPost]
        [ValidationModel]
        public async Task<IActionResult> ChangePassword(string id,ChangePasswordBindingModel model)
        {
            var result = await adminUsersService.ChangeUserPassword(id, model);
            if (!result.Succeeded)
            {
                return this.View();
            }

            this.TempData[AdminConstants.MessageType] = AdminConstants.Success;
            this.TempData[AdminConstants.Message] = AdminConstants.PasswordChangeSuccessfully;
            return RedirectToAction(nameof(Index));
        }
        
        public IActionResult Ban(string id)
        {
            this.ViewData[AdminConstants.UserId] = id;
            return this.View();
        }
        
        public async Task<IActionResult> ConfirmBan(string id)
        {
            await this.adminUsersService.BanUser(id);

            this.TempData[AdminConstants.MessageType] = AdminConstants.Success;
            this.TempData[AdminConstants.Message] = AdminConstants.SuccessfullyBan;

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(string id)
        {
            this.ViewData[AdminConstants.UserId] = id;
            return this.View();
        }
        
        public async Task<IActionResult> ConfirmDelete(string id)
        {
            await this.adminUsersService.DeleteUser(id);

            this.TempData[AdminConstants.MessageType] = AdminConstants.Success;
            this.TempData[AdminConstants.Message] = AdminConstants.SuccessfullyDeleteUser;

            return RedirectToAction(nameof(Index));
        }
    }
}