using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NbaStore.App.Filters;
using NbaStore.Common.Constants.AreaAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NbaStore.App.Areas.Admin.Controllers
{
    [Area(AdminConstants.AreaName)]
    [Authorize(Roles = AdminConstants.AdminRoleName)]
    public abstract class AdminController:Controller
    {
    }
}
