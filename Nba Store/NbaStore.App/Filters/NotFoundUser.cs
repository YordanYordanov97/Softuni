using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NbaStore.Data;
using System;
using System.Linq;

namespace NbaStore.App.Filters
{
    public class NotFoundUser : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var db = (NbaStoreDbContext)context.HttpContext.RequestServices.GetService(typeof(NbaStoreDbContext));
            var id = context.RouteData.Values.Values.Last().ToString();

            var userExist = db.Users.Find(id);
            if (userExist == null)
            {
                //{
                //    var routeValues = context.RouteData.Values;

                //    var areaName=routeValues["Area"];
                //    var controllerName= routeValues["Controller"];

                context.Result = new NotFoundResult();
            }
        }
    }
}
