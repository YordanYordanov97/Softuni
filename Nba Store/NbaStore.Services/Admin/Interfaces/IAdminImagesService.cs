using NbaStore.Common.BindingModels.Admin.Images;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NbaStore.Services.Admin.Interfaces
{
    public interface IAdminImagesService
    {
        Task<ImageBindingModel> GetImage(int id);

        Task SaveImage(ImageBindingModel model,int productId);

        Task EditProduct(int id, ImageBindingModel model);

        Task DeleteProduct(int id);
    }
}
