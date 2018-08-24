using NbaStore.Common.ViewModels;
using NbaStore.Common.ViewModels.Admin.Products;
using NbaStore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NbaStore.Services.Products.Interfaces
{
    public interface IProductsService
    {
        IEnumerable<ProductIndexViewModel> GetTheMostSellableProducts(string gender);

        IEnumerable<ProductIndexViewModel> GetProductsBySearchTerm(string searchTerm);

        IEnumerable<Team> GetAllTeams();

        IEnumerable<AllBrandsViewModel> GetAllBrands();

        IEnumerable<ProductIndexViewModel> GetProducts(string gender);

        IEnumerable<ProductIndexViewModel> GetProductsOrderByPriceDescending(string gender);

        IEnumerable<ProductIndexViewModel> GetProductsOrderByPriceAscending(string gender);

        IEnumerable<ProductIndexViewModel> GetProductsOrderByDiscountDescending(string gender);

        IEnumerable<ProductIndexViewModel> GetProductsOrderByDiscountAscending(string gender);
    }
}
