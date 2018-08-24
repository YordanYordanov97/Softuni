using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NbaStore.Common.ViewModels.Orders;
using NbaStore.Models;
using NbaStore.Services.Orders.Interfaces;

namespace NbaStore.App.Pages.Orders
{
    [Authorize]
    public class MyOrdersModel : PageModel
    {
        private readonly IOrdersService ordersService;
        private readonly UserManager<User> userManager;

        public MyOrdersModel(IOrdersService ordersService,
            UserManager<User> userManager)
        {
            this.ordersService = ordersService;
            this.userManager = userManager;
        }

        public IEnumerable<UserOrdersViewModel> ViewModel { get; set; }
        
        public void OnGet()
        {
            var userId = this.userManager.GetUserId(User);
            var model = this.ordersService.GetUserOrders(userId);

            this.ViewModel = model;
        }
    }
}