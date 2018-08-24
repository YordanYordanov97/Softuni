using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NbaStore.Models;

namespace NbaStore.App.Areas.Identity.Pages.Account
{
    public class WaitConfirmationModel : PageModel
    {
        private readonly UserManager<User> userManager;

        public WaitConfirmationModel(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public string UserEmail{ get; set; }

        public async Task OnGet(string id)
        {
            var user = await this.userManager.FindByIdAsync(id);
            this.UserEmail = user.Email;
        }
    }
}