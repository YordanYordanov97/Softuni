using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NbaStore.Common.ViewModels.Admin.Products;
using NbaStore.Common.ViewModels.Admin.Teams;
using NbaStore.Data;
using NbaStore.Models;
using NbaStore.Services.TeamProducts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NbaStore.Services.TeamProducts
{
    public class TeamProductsServices : BaseEFService, ITeamProductsServices
    {
        public TeamProductsServices(NbaStoreDbContext dbContext, IMapper mapper) 
            : base(dbContext, mapper)
        {
        }

        public TeamDetailsViewModel GetTeamWithProducts(int id, string gender)
        {
            var team = this.DbContext.Teams
              .Select(x => new
              {
                  Id = x.Id,
                  LogoId = x.LogoUrl,
                  Name = x.Name,
                  Products = x.Products.Select(p => new
                  {
                      Id = p.Id,
                      Title = p.Title,
                      Price = p.Price,
                      Discount = p.Discount,
                      PictureUrl = p.PictureUrl,
                      Gender = p.Gender
                  })
                  .Where(p => p.Gender.ToLower() == gender.ToLower())
              })
              .SingleOrDefault(x => x.Id == id);

            this.CheckIfTeamExist(id);

            var model = this.CreateModel(team);

            return model;
        }
        
        public TeamDetailsViewModel GetTeamWithProductsOrderByPriceAscending(int id, string gender)
        {
             var team = this.DbContext.Teams
               .Select(x => new
               {
                   Id = x.Id,
                   LogoId = x.LogoUrl,
                   Name = x.Name,
                   Products = x.Products.Select(p => new
                   {
                       Id = p.Id,
                       Title = p.Title,
                       Price = p.Price,
                       Discount = p.Discount,
                       PictureUrl = p.PictureUrl,
                       Gender = p.Gender
                   })
                   .Where(p => p.Gender.ToLower() == gender.ToLower())
                   .OrderBy(pr => Math.Round(pr.Price - ((pr.Price * pr.Discount) / 100)))
               })
               .SingleOrDefault(x => x.Id == id);

            this.CheckIfTeamExist(id);

            var model = this.CreateModel(team);

            return model;
        }

        public TeamDetailsViewModel GetTeamWithProductsOrderByPriceDescending(int id, string gender)
        {
            var team = this.DbContext.Teams
               .Select(x => new
               {
                   Id = x.Id,
                   LogoId = x.LogoUrl,
                   Name = x.Name,
                   Products = x.Products.Select(p => new
                   {
                       Id = p.Id,
                       Title = p.Title,
                       Price = p.Price,
                       Discount = p.Discount,
                       PictureUrl = p.PictureUrl,
                       Gender = p.Gender
                   })
                   .Where(p => p.Gender.ToLower() == gender.ToLower())
                   .OrderByDescending(pr => Math.Round(pr.Price - ((pr.Price * pr.Discount) / 100)))
               })
               .SingleOrDefault(x => x.Id == id);

            this.CheckIfTeamExist(id);
            
            var model = this.CreateModel(team);

            return model;
        }

        public TeamDetailsViewModel GetTeamWithProductsOrderByDiscountAscending(int id, string gender)
        {
            var team = this.DbContext.Teams
               .Select(x => new
               {
                   Id = x.Id,
                   LogoId = x.LogoUrl,
                   Name = x.Name,
                   Products = x.Products.Select(p => new
                   {
                       Id = p.Id,
                       Title = p.Title,
                       Price = p.Price,
                       Discount = p.Discount,
                       PictureUrl = p.PictureUrl,
                       Gender = p.Gender
                   })
                   .Where(p => p.Gender.ToLower() == gender.ToLower())
                   .OrderBy(d=>d.Discount)
               })
               .SingleOrDefault(x => x.Id == id);

            this.CheckIfTeamExist(id);

            var model = this.CreateModel(team);

            return model;
        }

        public TeamDetailsViewModel GetTeamWithProductsOrderByDiscountDescending(int id, string gender)
        {
            var team = this.DbContext.Teams
               .Select(x => new
               {
                   Id = x.Id,
                   LogoId = x.LogoUrl,
                   Name = x.Name,
                   Products = x.Products.Select(p => new
                   {
                       Id = p.Id,
                       Title = p.Title,
                       Price = p.Price,
                       Discount = p.Discount,
                       PictureUrl = p.PictureUrl,
                       Gender = p.Gender
                   })
                   .Where(p => p.Gender.ToLower() == gender.ToLower())
                   .OrderByDescending(d => d.Discount)
               })
               .SingleOrDefault(x => x.Id == id);

            this.CheckIfTeamExist(id);

            var model = this.CreateModel(team);

            return model;
        }

        private void CheckIfTeamExist(int id)
        {
            var team = this.DbContext.Teams.SingleOrDefault(x => x.Id == id);
            if (team == null)
            {
                throw new ArgumentNullException();
            }
        }

        private TeamDetailsViewModel CreateModel(dynamic team)
        {
            var model = new TeamDetailsViewModel();
            model.LogoUrl = team.LogoId;
            model.Name = team.Name;
            var products = new List<ProductIndexViewModel>();
            foreach (var product in team.Products)
            {
                products.Add(new ProductIndexViewModel
                {
                    Id = product.Id,
                    Title = product.Title,
                    Price = product.Price,
                    Discount = product.Discount,
                    PictureUrl = product.PictureUrl
                });
            }
            model.Products = products;

            return model;
        }
    }
}
