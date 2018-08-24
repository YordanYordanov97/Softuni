using AutoMapper;
using NbaStore.Common.ViewModels.Admin.Products;
using NbaStore.Data;
using NbaStore.Models;
using NbaStore.Services.Brands.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NbaStore.Services.Brands
{
    public class BrandsService : BaseEFService, IBrandsService
    {
        public BrandsService(NbaStoreDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }

        public IEnumerable<ProductIndexViewModel> GetProducts(string[] brandNames, string gender)
        {
            var products = this.DbContext.Products
                .Where(x => x.Gender.ToLower() == gender.ToLower()
                && brandNames.Contains(x.Brand))
                .ToList();

            return this.Mapper.Map<IEnumerable<ProductIndexViewModel>>(products);
        }

        public IEnumerable<ProductIndexViewModel> GetProductsOrderByPriceDescending(string[] brandNames, 
            string gender)
        {
            var products = this.DbContext.Products
                .Where(x => x.Gender.ToLower() == gender.ToLower()
                && brandNames.Contains(x.Brand))
                .OrderByDescending(x => Math.Round(x.Price - ((x.Price * x.Discount) / 100)))
                .ToList();

            return this.Mapper.Map<IEnumerable<ProductIndexViewModel>>(products);
        }

        public IEnumerable<ProductIndexViewModel> GetProductsOrderByPriceAscending(string[] brandNames,
            string gender)
        {
            var products = this.DbContext.Products
                 .Where(x => x.Gender.ToLower() == gender.ToLower()
                && brandNames.Contains(x.Brand))
                 .OrderBy(x => Math.Round(x.Price - ((x.Price * x.Discount) / 100)))
                 .ToList();

            return this.Mapper.Map<IEnumerable<ProductIndexViewModel>>(products);
        }

        public IEnumerable<ProductIndexViewModel> GetProductsOrderByDiscountDescending(string[] brandNames,
            string gender)
        {
            var products = this.DbContext.Products
                 .Where(x => x.Gender.ToLower() == gender.ToLower()
                && brandNames.Contains(x.Brand))
                 .OrderByDescending(x => x.Discount)
                 .ToList();

            return this.Mapper.Map<IEnumerable<ProductIndexViewModel>>(products);
        }

        public IEnumerable<ProductIndexViewModel> GetProductsOrderByDiscountAscending(string[] brandNames, 
            string gender)
        {
            var products = this.DbContext.Products
                 .Where(x => x.Gender.ToLower() == gender.ToLower()
                && brandNames.Contains(x.Brand))
                 .OrderBy(x => x.Discount)
                 .ToList();

            return this.Mapper.Map<IEnumerable<ProductIndexViewModel>>(products);
        }
        
        public IEnumerable<ProductIndexViewModel> GetTheMostSellableProducts(string[] brandNames,
            string gender)
        {
            var orderProducts = this.DbContext.OrderProducts.ToList();
            var productsIdContsDic = new Dictionary<int, int>();
            foreach (var orderProduct in orderProducts)
            {
                if (!productsIdContsDic.ContainsKey(orderProduct.ProductId))
                {
                    productsIdContsDic.Add(orderProduct.ProductId, 1);
                }
                productsIdContsDic[orderProduct.ProductId] += orderProduct.Quantity;
            }

            var ordederedProducts = new List<Product>();
            var orderedProductsIds = new List<int>();

            foreach (var kvp in productsIdContsDic.OrderByDescending(x => x.Value))
            {
                var product = this.DbContext.Products
                    .SingleOrDefault(x => x.Id == kvp.Key && x.Gender == gender
                    && brandNames.Contains(x.Brand));

                if (product != null)
                {
                    ordederedProducts.Add(product);
                    orderedProductsIds.Add(kvp.Key);
                }
            }

            var otherProducts = this.DbContext.Products
                .Where(x => x.Gender == gender && brandNames.Contains(x.Brand)
                && !(orderedProductsIds.Contains(x.Id)))
                .ToList();

            var products = ordederedProducts.Concat(otherProducts).ToList();

            return this.Mapper.Map<IEnumerable<ProductIndexViewModel>>(products);
        }
    }
}
