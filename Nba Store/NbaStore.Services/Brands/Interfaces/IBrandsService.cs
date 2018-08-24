using NbaStore.Common.ViewModels.Admin.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace NbaStore.Services.Brands.Interfaces
{
    public interface IBrandsService
    {
        IEnumerable<ProductIndexViewModel> GetTheMostSellableProducts(string[] brandNames,string gender);

        IEnumerable<ProductIndexViewModel> GetProducts(string[] brands, string gender);

        IEnumerable<ProductIndexViewModel> GetProductsOrderByPriceDescending(string[] brandNames, string gender);

        IEnumerable<ProductIndexViewModel> GetProductsOrderByPriceAscending(string[] brandNames, string gender);

        IEnumerable<ProductIndexViewModel> GetProductsOrderByDiscountDescending(string[] brandNames, string gender);

        IEnumerable<ProductIndexViewModel> GetProductsOrderByDiscountAscending(string[] brandNames, string gender);
    }
}
