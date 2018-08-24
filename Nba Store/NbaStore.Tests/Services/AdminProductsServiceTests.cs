using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NbaStore.Data;
using NbaStore.Models;
using NbaStore.Services.Admin;
using NbaStore.Tests.Mocks;
using System.Threading.Tasks;
using System.Linq;
using NbaStore.Common.ViewModels.Admin.Teams;
using NbaStore.Common.BindingModels.Admin.Teams;
using System;
using System.Collections.Generic;
using NbaStore.Common.ViewModels.Admin.Products;
using NbaStore.Common.BindingModels.Admin.Products;
using NbaStore.Common.Constants;

namespace NbaStore.Services.Tests
{
    [TestClass]
    public class AdminProductsServiceTests
    {
        private NbaStoreDbContext dbContext;
        private IMapper mapper;
        private AdminProductsService service;

        [TestInitialize]
        public void InitializeTests()
        {
            this.dbContext = MockDbContext.GetContext();
            this.mapper = MockAutoMapper.GetAutoMapper();
            this.service = new AdminProductsService(this.dbContext, this.mapper);
            
            this.dbContext.Teams.Add(new Team() { Id = 1, Name = string.Format(TestsConstants.Team, 1) });
            this.dbContext.SaveChanges();

            this.dbContext.Products.Add(new Product()
            {
                Id = 1,
                Title = string.Format(TestsConstants.Product, 1),
                Brand = string.Format(TestsConstants.Brand, 1),
                TeamId = 1
            });
            this.dbContext.SaveChanges();
        }

        [TestMethod]
        public void GetProducts_WithFewProducts_ShouldReturnAll()
        {
            this.dbContext.Products.Add(new Product() { Id = 2, Title = string.Format(TestsConstants.Product, 2),
                TeamId = 1 });
            this.dbContext.Products.Add(new Product() { Id = 3, Title = string.Format(TestsConstants.Product, 3),
                TeamId = 1 });

            this.dbContext.SaveChanges();

            var products = this.service.GetProducts().ToList();

            Assert.IsNotNull(products);
            Assert.AreEqual(3, products.Count);
            CollectionAssert.AreEqual(new[] { string.Format(TestsConstants.Product, 1),
                string.Format(TestsConstants.Product, 2),
                string.Format(TestsConstants.Product, 3)},
                products.Select(t => t.Title).ToArray());
        }

        [TestMethod]
        public void GetProducts_WithFewProducts_ShouldReturnTypeofIEnumerableProductIndexViewModel()
        {
            this.dbContext.Products.Add(new Product() { Id = 2, Title = string.Format(TestsConstants.Product, 2),
                TeamId = 1 });

            this.dbContext.SaveChanges();

            var products= this.service.GetProducts();

            Assert.IsInstanceOfType(products, typeof(IEnumerable<ProductIndexViewModel>));
        }

        [TestMethod]
        public void GetProducts_WithZeroProducts_ShouldReturnZero()
        {
            var product = this.dbContext.Products.SingleOrDefault(x => x.Id == 1);
            this.dbContext.Products.Remove(product);
            this.dbContext.SaveChanges();

            var products = this.service.GetProducts().ToList();

            Assert.IsNotNull(products);
            Assert.AreEqual(0, products.Count);
        }

        [TestMethod]
        public async Task GetDetailsAsync_WithId_ShouldReturnProductDetailsViewModel()
        {
            this.dbContext.Images.Add(new Image() { Id = 1, ProductId=1 });
            this.dbContext.Images.Add(new Image() { Id = 2, ProductId = 1 });
            this.dbContext.SaveChanges();

            var productFromService = await this.service.GetDetails(1);

            var team = this.dbContext.Teams.SingleOrDefault(x => x.Id == 1);
            var productDetailsViewModel = new ProductDetailsViewModel()
            {
                Title = string.Format(TestsConstants.Product, 1),
                Team= team,
                Images = this.dbContext.Images.Where(x => x.ProductId == 1).ToList()
            };

            Assert.AreEqual(productDetailsViewModel.Title, productFromService.Title);
            Assert.AreEqual(productDetailsViewModel.Team.Name, productFromService.Team.Name);
            CollectionAssert.AreEqual(productDetailsViewModel.Images.ToList(),
               productFromService.Images.ToList());
        }

        [TestMethod]
        public async Task GetDetailsAsync_WithId_ShouldReturnTypeofProductDetailsViewModel()
        {
            var productFromService = await this.service.GetDetails(1);

            var productDetailsViewModel = new ProductDetailsViewModel()
            {
                Title = string.Format(TestsConstants.Product, 1)
            };

            Assert.IsInstanceOfType(productFromService, typeof(ProductDetailsViewModel));

        }

        [TestMethod]
        public async Task GetProductAsync_WithId_ShouldReturnThisProduct()
        {
            var productFromService = await this.service.GetProduct(1);

            var bindingModelTest = new ProductBindingModel()
            {
                Title = string.Format(TestsConstants.Product, 1),
                Brand= string.Format(TestsConstants.Brand, 1)
            };

            Assert.AreEqual(bindingModelTest.Title, productFromService.Title);
            Assert.AreEqual(bindingModelTest.Brand, productFromService.Brand);
        }

        [TestMethod]
        public async Task GetProductAsync_WithId_ShouldReturnTypeofProductBindingModel()
        {
            var productFromService = await this.service.GetProduct(1);

            var bindingModelTest = new ProductBindingModel()
            {
                Title = string.Format(TestsConstants.Test, 1),
                Brand = string.Format(TestsConstants.Brand, 1)
            };

            Assert.IsInstanceOfType(productFromService, typeof(ProductBindingModel));
        }

        [TestMethod]
        public async Task GetProductAsync_WithNoValidId_ShouldThrowArgumentNullException()
        {
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() =>
             this.service.GetProduct(2));
        }

        [TestMethod]
        public async Task SaveProductAsync_WithProperProduct_ShouldAddCorrectly()
        {
            var productToRemove = this.dbContext.Products.SingleOrDefault(x => x.Id == 1);
            this.dbContext.Products.Remove(productToRemove);
            this.dbContext.SaveChanges();

            var productModel = new ProductBindingModel
            {
               Title = string.Format(TestsConstants.Title, 1),
                Brand = string.Format(TestsConstants.Brand, 1),
                TeamId=1,
            };

            await this.service.SaveProduct(productModel);
            
            var product = this.dbContext.Products.First();

            Assert.AreEqual(string.Format(TestsConstants.Title, 1), product.Title);
            Assert.AreEqual(string.Format(TestsConstants.Brand, 1), product.Brand);
        }

        [TestMethod]
        public async Task DeleteProductAsync_WithId_ShouldDeleteCorrectly()
        {
            await service.DeleteProduct(1);

            Assert.AreEqual(0, this.dbContext.Products.Count());
        }

        [TestMethod]
        public async Task DeleteProductAsync_WithNoValidId_ShouldThrowArgumentNullException()
        {
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() =>
            this.service.DeleteProduct(2));
        }

        [TestMethod]
        public async Task EditProductAsync_WithId_ShouldEditCorrectly()
        {
            var bindingModel = new ProductBindingModel
            {
                Title = string.Format(TestsConstants.EditTeam, 1),
                Brand= string.Format(TestsConstants.EditBrand, 1)
            };

            await service.EditProduct(1, bindingModel);

            var product = this.dbContext.Products.SingleOrDefault(x => x.Id == 1);

            Assert.AreEqual(1, product.Id);
            Assert.AreEqual(string.Format(TestsConstants.EditTeam, 1), product.Title);
            Assert.AreEqual(string.Format(TestsConstants.EditBrand, 1), product.Brand);
        }

        [TestMethod]
        public async Task EditProductAsync_WithNoValidId_ShouldThrowArgumentNullException()
        {
            var bindingModel = new ProductBindingModel
            {
                Title = string.Format(TestsConstants.EditTeam, 1),
                Brand = string.Format(TestsConstants.EditBrand, 1)
            };

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() =>
            this.service.EditProduct(2, bindingModel));
        }
    }
}
