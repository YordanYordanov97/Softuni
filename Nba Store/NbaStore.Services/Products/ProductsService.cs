using AutoMapper;
using NbaStore.Common.ViewModels;
using NbaStore.Common.ViewModels.Admin.Products;
using NbaStore.Data;
using NbaStore.Models;
using NbaStore.Services.Products.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NbaStore.Services.Products
{
    public class ProductsService : BaseEFService, IProductsService
    {
        public ProductsService(NbaStoreDbContext dbContext, IMapper mapper) 
            : base(dbContext, mapper)
        {
        }

        public IEnumerable<AllBrandsViewModel> GetAllBrands()
        {
            var brands = this.DbContext.Products
                .Select(x => x.Brand)
                .ToHashSet();

            var allBrandsViewModel = new List<AllBrandsViewModel>();
            var id = 0;
            foreach(var brand in brands)
            {
                id++;
                allBrandsViewModel.Add(new AllBrandsViewModel
                {
                    Id = id,
                    Name = brand
                });
            }

            return allBrandsViewModel;
        }

        public IEnumerable<Team> GetAllTeams()
        {
            var teams = this.DbContext.Teams.ToList();

            return teams;
        }

        public IEnumerable<ProductIndexViewModel> GetProducts(string gender)
        {
            var products = this.DbContext.Products
                .Where(x=>x.Gender.ToLower()== gender.ToLower())
                .ToList();

            return this.Mapper.Map<IEnumerable<ProductIndexViewModel>>(products);
        }

        public IEnumerable<ProductIndexViewModel> GetProductsOrderByPriceDescending(string gender)
        {
            var products = this.DbContext.Products
                .Where(x => x.Gender.ToLower() == gender.ToLower())
                .OrderByDescending(x => Math.Round(x.Price - ((x.Price * x.Discount) / 100)))
                .ToList();

            return this.Mapper.Map<IEnumerable<ProductIndexViewModel>>(products);
        }

        public IEnumerable<ProductIndexViewModel> GetProductsOrderByPriceAscending(string gender)
        {
            var products = this.DbContext.Products
                 .Where(x => x.Gender.ToLower() == gender.ToLower())
                 .OrderBy(x => Math.Round(x.Price - ((x.Price * x.Discount) / 100)))
                 .ToList();

            return this.Mapper.Map<IEnumerable<ProductIndexViewModel>>(products);
        }

        public IEnumerable<ProductIndexViewModel> GetProductsOrderByDiscountDescending(string gender)
        {
            var products = this.DbContext.Products
                 .Where(x => x.Gender.ToLower() == gender.ToLower())
                 .OrderByDescending(x => x.Discount)
                 .ToList();

            return this.Mapper.Map<IEnumerable<ProductIndexViewModel>>(products);
        }

        public IEnumerable<ProductIndexViewModel> GetProductsOrderByDiscountAscending(string gender)
        {
            var products = this.DbContext.Products
                 .Where(x => x.Gender.ToLower() == gender.ToLower())
                 .OrderBy(x => x.Discount)
                 .ToList();

            return this.Mapper.Map<IEnumerable<ProductIndexViewModel>>(products);
        }

        public IEnumerable<ProductIndexViewModel> GetProductsBySearchTerm(string searchTerm)
        {
            var products = this.DbContext.Products
                 .Where(x => x.Title.ToLower().Contains(searchTerm.ToLower()))
                 .ToList();

            return this.Mapper.Map<IEnumerable<ProductIndexViewModel>>(products);
        }

        public IEnumerable<ProductIndexViewModel> GetTheMostSellableProducts(string gender)
        {
            var orderProducts = this.DbContext.OrderProducts.ToList();
            var productsIdContsDic = new Dictionary<int, int>();
            foreach(var orderProduct in orderProducts)
            {
                if (!productsIdContsDic.ContainsKey(orderProduct.ProductId))
                {
                    productsIdContsDic.Add(orderProduct.ProductId, 1);
                }
                productsIdContsDic[orderProduct.ProductId] += orderProduct.Quantity;
            }

            var ordederedProducts = new List<Product>();
            var orderedProductsIds = new List<int>();

            foreach(var kvp in productsIdContsDic.OrderByDescending(x => x.Value))
            {
                var product = this.DbContext.Products
                    .SingleOrDefault(x => x.Id == kvp.Key && x.Gender == gender);

                if (product != null)
                {
                    ordederedProducts.Add(product);
                    orderedProductsIds.Add(kvp.Key);
                }
            }

            var otherProducts = this.DbContext.Products
                .Where(x => x.Gender == gender && !(orderedProductsIds.Contains(x.Id)))
                .ToList();

            var products = ordederedProducts.Concat(otherProducts).ToList();
            
            return this.Mapper.Map<IEnumerable<ProductIndexViewModel>>(products);
        }
    }
}
