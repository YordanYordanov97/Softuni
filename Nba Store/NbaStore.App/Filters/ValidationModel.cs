using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace NbaStore.App.Filters
{
    public class ValidationModel : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var routeValues = context.RouteData.Values;
                var areaName = routeValues["Area"];
                var controllerName = routeValues["Controller"];
                var action = routeValues["Action"];
                
                context.Result = new RedirectResult($"/{areaName}/{controllerName}/{action}");
            }
        }
    }
}
