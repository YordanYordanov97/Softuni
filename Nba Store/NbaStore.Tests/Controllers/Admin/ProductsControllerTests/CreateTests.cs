using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NbaStore.App.Areas.Admin.Controllers;
using NbaStore.Common.BindingModels.Admin.Products;
using NbaStore.Services.Admin.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace NbaStore.Tests.Controllers.Admin.ProductsControllerTests
{
    [TestClass]
    public class CreateTests
    {
        [TestMethod]
        public void Create_ShouldReturnTypeofViewModel()
        {
            var mockReposity = new Mock<IAdminProductsService>();
            mockReposity.
                Setup(repo => repo.GetBindingModel());

            var controller = new ProductsController(mockReposity.Object);

            var result = controller.Create();

            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Create_WithShouldCallServiceGetBindingModel()
        {
            var serviceCalled = false;
            var mockReposity = new Mock<IAdminProductsService>();
            mockReposity.
                Setup(repo => repo.GetBindingModel())
                .Callback(() => serviceCalled = true);

            var controller = new ProductsController(mockReposity.Object);

            var result = controller.Create();

            Assert.IsTrue(serviceCalled);
        }

        [TestMethod]
        public void Create_WithValidProductShouldCallService()
        {
            var model = new ProductBindingModel();
            var serviceCalled = false;
            var mockReposity = new Mock<IAdminProductsService>();
            mockReposity.
                Setup(repo => repo.SaveProduct(model))
                .Callback(() => serviceCalled = true);

            var controller = new ProductsController(mockReposity.Object);

            var result = controller.Create(model);

            Assert.IsTrue(serviceCalled);
        }
    }
}
