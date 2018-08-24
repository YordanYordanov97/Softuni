using AutoMapper;
using NbaStore.Common.BindingModels.Admin.Images;
using NbaStore.Data;
using NbaStore.Models;
using NbaStore.Services.Admin.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NbaStore.Services.Admin
{
    public class AdminImagesService : BaseEFService, IAdminImagesService
    {
        public AdminImagesService(NbaStoreDbContext dbContext, IMapper mapper) 
            : base(dbContext, mapper)
        {
        }

        public async Task<ImageBindingModel> GetImage(int id)
        {
            var image= await this.DbContext.Images.FindAsync(id);
            this.CheckIfImageExist(image);

            return this.Mapper.Map<ImageBindingModel>(image);
        }

        public async Task SaveImage(ImageBindingModel model, int productId)
        {
            var image = this.Mapper.Map<Image>(model);
            image.ProductId = productId;
            await this.DbContext.Images.AddAsync(image);
            await this.DbContext.SaveChangesAsync();
        }
        
        public async Task EditProduct(int id, ImageBindingModel model)
        {
            var image = await this.DbContext.Images.FindAsync(id);
            this.CheckIfImageExist(image);

            image.Url = model.Url;

            await this.DbContext.SaveChangesAsync();
        }

        public async Task DeleteProduct(int id)
        {
            var image = await this.DbContext.Images.FindAsync(id);
            this.CheckIfImageExist(image);

            this.DbContext.Remove(image);
            await this.DbContext.SaveChangesAsync();
        }

        private void CheckIfImageExist(Image image)
        {
            if (image == null)
            {
                throw new ArgumentNullException($"Image with {image.Id} id not found!");
            }
        }
    }
}
