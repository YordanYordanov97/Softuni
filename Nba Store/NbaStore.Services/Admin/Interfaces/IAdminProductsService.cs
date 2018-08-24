using NbaStore.Common.BindingModels.Admin.Products;
using NbaStore.Common.ViewModels.Admin.Products;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NbaStore.Services.Admin.Interfaces
{
    public interface IAdminProductsService
    {
        IEnumerable<ProductIndexViewModel> GetProducts();

        ProductBindingModel GetBindingModel();

        Task<ProductDetailsViewModel> GetDetails(int id);

        Task<ProductBindingModel> GetProduct(int id);

        Task SaveProduct(ProductBindingModel model);

        Task EditProduct(int id, ProductBindingModel model);

        Task DeleteProduct(int id);

        Task SetSizeToProductById(int id,string size);
    }
}
