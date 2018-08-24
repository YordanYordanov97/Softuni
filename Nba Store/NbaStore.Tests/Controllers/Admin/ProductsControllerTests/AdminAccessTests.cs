using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NbaStore.App.Areas.Admin.Controllers;
using NbaStore.Common.Constants.AreaAdmin;
using System.Linq;

namespace NbaStore.Tests.Controllers.Admin.ProductsControllerTests
{
    [TestClass]
    public class AdminAccessTests
    {
        [TestMethod]
        public void ProductsController_ShouldBeInAdminArea()
        {
            var area = typeof(ProductsController)
                .GetCustomAttributes(true)
                .FirstOrDefault(atr => atr is AreaAttribute) as AreaAttribute;

            Assert.IsNotNull(area);
            Assert.AreEqual(AdminConstants.AreaName, area.RouteValue);
        }

        [TestMethod]
        public void ProductsController_ShouldBeAuthorizeAdmin()
        {
            var authorization = typeof(ProductsController)
                .GetCustomAttributes(true)
                .FirstOrDefault(atr => atr is AuthorizeAttribute) as AuthorizeAttribute;

            Assert.IsNotNull(authorization);
            Assert.AreEqual(AdminConstants.AdminRoleName, authorization.Roles);
        }
    }
}
