using Microsoft.AspNetCore.Identity;
using NbaStore.Common.BindingModels.Admin.Users;
using NbaStore.Common.ViewModels.Admin.Users;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NbaStore.Services.Admin.Interfaces
{
    public interface IAdminUsersService
    {
        Task<IEnumerable<UserIndexViewModel>> GetUsers(ClaimsPrincipal sessionUser);

        Task<UserDetailsViewModel> GetUserDatails(string id);

        Task<IdentityResult> ChangeUserPassword(string id, ChangePasswordBindingModel model);

        Task BanUser(string id);

        Task DeleteUser(string id);
    }
}
