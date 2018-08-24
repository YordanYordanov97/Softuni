using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using NbaStore.Common.Constants.AreaAdmin;
using NbaStore.Common.SeedDtoModels;
using NbaStore.Data;
using NbaStore.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;

namespace NbaStore.App.Common
{
    public static class ApplicationBuilderExtensions
    {
        public static async void SeedDatabase(this IApplicationBuilder app)
        {
            var serviceFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            var scope = serviceFactory.CreateScope();

            using (scope)
            {
                var context = scope.ServiceProvider.GetService<NbaStoreDbContext>();

                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

                if (!await roleManager.RoleExistsAsync(AdminConstants.Admin))
                {
                    await roleManager.CreateAsync(new IdentityRole(AdminConstants.AdminRoleName));
                }

                var user = await userManager.FindByNameAsync(AdminConstants.Admin);
                if (user == null)
                {
                    user = new User()

                    {
                        UserName = AdminConstants.Admin,
                        Email = AdminConstants.AdminEmail,
                        DateOfRegistration = DateTime.UtcNow,
                        FullName = AdminConstants.AdminFullName,
                        EmailConfirmed=true,
                    };

                    await userManager.CreateAsync(user, AdminConstants.AdminPassword);
                    await userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, AdminConstants.AdminRoleName));
                    await userManager.AddToRoleAsync(user, AdminConstants.AdminRoleName);
                }
               
                if (!context.Teams.Any())
                {
                    var jsonTeams = File.ReadAllText(@"wwwroot\seedfiles\teams.json");
                    var teamDtos = JsonConvert.DeserializeObject<TeamDto[]>(jsonTeams);

                    SeedTeams(context, teamDtos);
                }

                if (!context.Products.Any())
                {
                    var jsonProducts = File.ReadAllText(@"wwwroot\seedfiles\products.json");
                    var productDtos = JsonConvert.DeserializeObject<ProductDto[]>(jsonProducts);

                    SeedProducts(context, productDtos);
                }

                if (!context.Images.Any())
                {
                    var jsonImages = File.ReadAllText(@"wwwroot\seedfiles\images.json");
                    var imageDtos = JsonConvert.DeserializeObject<ImageDto[]>(jsonImages);

                    SeedImages(context, imageDtos);
                }
            }
        }

        private static void SeedTeams(NbaStoreDbContext context, TeamDto[] teamDtos)
        {
            var teamsToCreate = teamDtos
                .Select(t => new Team
                {
                    Name = t.Name,
                    LogoUrl = t.LogoUrl,
                })
                .ToArray();

            context.Teams.AddRange(teamsToCreate);
            context.SaveChanges();
        }

        private static void SeedProducts(NbaStoreDbContext context, ProductDto[] productDtos)
        {
            var productsToCreate = new List<Product>();
            foreach (var productDto in productDtos)
            {
                var team = context.Teams.SingleOrDefault(x => x.Name == productDto.TeamName);

                if (team != null)
                {
                    var teamId = team.Id;
                    var product = new Product
                    {
                        Title = productDto.Title,
                        PictureUrl = productDto.PictureUrl,
                        Price = productDto.Price,
                        Discount = productDto.Discount,
                        Gender = productDto.Gender,
                        Type = productDto.Type,
                        Brand = productDto.Brand,
                        Description = productDto.Description,
                        TeamId = teamId
                    };

                    productsToCreate.Add(product);
                }
            }

            context.Products.AddRange(productsToCreate);
            context.SaveChanges();
        }

        private static void SeedImages(NbaStoreDbContext context, ImageDto[] imagesDtos)
        {
            var imagesToCreate = new List<Image>();
            foreach (var imageDto in imagesDtos)
            {
                var product = context.Products.SingleOrDefault(x => x.Title == imageDto.ProductName);

                if (product != null)
                {
                    var productd = product.Id;
                    var image = new Image
                    {
                        Url=imageDto.Url,
                        ProductId=productd
                    };

                    imagesToCreate.Add(image);
                }
            }

            context.Images.AddRange(imagesToCreate);
            context.SaveChanges();
        }
    }
}
