using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NbaStore.App.Filters
{
    public class AdminAuthorize : Attribute,IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var result=context.HttpContext.User.IsInRole("Administrator");

            if (!result)
            {
                if (!context.HttpContext.User.Identity.IsAuthenticated)
                {
                    context.Result = new RedirectResult("/Identity/Account/Login");
                }
                else
                {
                    context.Result = new RedirectResult("/Identity/Account/AccessDenied");
                }
            }
        }
    }
}
