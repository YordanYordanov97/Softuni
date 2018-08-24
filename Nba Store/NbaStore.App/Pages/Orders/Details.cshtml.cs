using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NbaStore.Common.ViewModels.Orders;
using NbaStore.Services.Orders.Interfaces;

namespace NbaStore.App.Pages.Orders
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly IOrdersService ordersService;

        public DetailsModel(IOrdersService ordersService)
        {
            this.ordersService = ordersService;
        }

        public IEnumerable<OrderProductsViewModel> ViewModel { get; set; }

        public void OnGet(int id)
        {
            this.ViewModel = this.ordersService.GetOrderProducts(id);
        }
    }
}