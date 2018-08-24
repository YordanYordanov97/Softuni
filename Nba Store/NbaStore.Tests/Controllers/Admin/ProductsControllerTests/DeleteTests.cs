using Microsoft.AspNetCore.Mvc;
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
    public class DeleteTests
    {
        [TestMethod]
        public void Delete_ViewModelShouldNotBeNull()
        {
            var mockRepository = new Mock<IAdminProductsService>();

            var controller = new ProductsController(mockRepository.Object);

            var result = controller.Delete(1);

            Assert.IsNotNull(result as ViewResult);
        }

        [TestMethod]
        public void Delete_ShouldReturnTypeofViewModel()
        {
            var mockRepository = new Mock<IAdminProductsService>();

            var controller = new ProductsController(mockRepository.Object);

            var result = controller.Delete(1);
            
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
    }
}
