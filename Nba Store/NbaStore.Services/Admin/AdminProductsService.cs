using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NbaStore.Common.BindingModels.Admin.Products;
using NbaStore.Common.ViewModels.Admin.Products;
using NbaStore.Data;
using NbaStore.Models;
using NbaStore.Services.Admin.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NbaStore.Services.Admin
{
    public class AdminProductsService : BaseEFService, IAdminProductsService
    {
        public AdminProductsService(NbaStoreDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }

        public IEnumerable<ProductIndexViewModel> GetProducts()
        {
            var products = this.DbContext.Products.ToList();

            return this.Mapper.Map<IEnumerable<ProductIndexViewModel>>(products);
        }

        public async Task<ProductDetailsViewModel> GetDetails(int id)
        {
            var product = await this.DbContext.Products
                .Include(t => t.Team)
                .Include(i=>i.Images)
                .SingleOrDefaultAsync(x => x.Id == id);
            this.CheckIfProductExist(product);

            var model = this.Mapper.Map<ProductDetailsViewModel>(product);

            return model;
        }

        public async Task<ProductBindingModel> GetProduct(int id)
        {
            var product = await this.DbContext.Products
                .FindAsync(id);
            this.CheckIfProductExist(product);
            
            var bindingModel = this.Mapper.Map<ProductBindingModel>(product);

            return bindingModel;
        }

        public ProductBindingModel GetBindingModel()
        {
            var allteams = this.DbContext.Teams.ToList();

            var productBindingModel = new ProductBindingModel
            {
                Teams = allteams
            };

            return productBindingModel;
        }

        public async Task SaveProduct(ProductBindingModel model)
        {
            var team = await this.DbContext.Teams.FindAsync(model.TeamId);
            this.CheckIfTeamExist(team);

            var product = this.Mapper.Map<Product>(model);
            await this.DbContext.Products.AddAsync(product);
            await this.DbContext.SaveChangesAsync();
        }

        public async Task DeleteProduct(int id)
        {
            var product = await this.DbContext.Products.FindAsync(id);
            this.CheckIfProductExist(product);

            this.DbContext.Remove(product);
            await this.DbContext.SaveChangesAsync();
        }

        public async Task EditProduct(int id, ProductBindingModel model)
        {
            var product = await this.DbContext.Products.FindAsync(id);
            this.CheckIfProductExist(product);
            
            product.Title = model.Title;
            product.Brand = model.Brand;
            product.Description = model.Description;
            product.Discount = model.Discount;
            product.Gender = model.Gender;
            product.Price = model.Price;
            product.Type = model.Type;
            product.PictureUrl = model.PictureUrl;
                
            await this.DbContext.SaveChangesAsync();
        }

        public async Task SetSizeToProductById(int id,string size)
        {
            var product = await this.DbContext.Products.FindAsync(id);
            this.CheckIfProductExist(product);

            product.Size = size;
            this.DbContext.SaveChanges();
        }

        private void CheckIfProductExist(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException();
            }
        }

        private void CheckIfTeamExist(Team team)
        {
            if (team == null)
            {
                throw new ArgumentNullException();
            }
        }
    }
}
