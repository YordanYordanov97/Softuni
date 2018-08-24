using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NbaStore.App.Areas.Admin.Controllers;
using NbaStore.Services.Admin.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace NbaStore.Tests.Controllers.Admin.ProductsControllerTests
{
    [TestClass]
    public class ConfirmDeleteTests
    {
        [TestMethod]
        public void ConfirmDelete_WithIdCallServiceGetProduct()
        {
            var serviceCalled = false;
            var mockReposity = new Mock<IAdminProductsService>();
            mockReposity.
                Setup(repo => repo.DeleteProduct(1))
                .Callback(() => serviceCalled = true);

            var controller = new ProductsController(mockReposity.Object);

            var result = controller.ConfirmDelete(1);

            Assert.IsTrue(serviceCalled);
        }
    }
}
