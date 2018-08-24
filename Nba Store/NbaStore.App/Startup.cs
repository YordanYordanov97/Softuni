using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NbaStore.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NbaStore.Models;
using NbaStore.App.Common;
using AutoMapper;
using Microsoft.AspNetCore.Identity.UI.Services;
using NbaStore.App.Areas.Identity.Services;
using NbaStore.Services.Admin.Interfaces;
using NbaStore.Services.Admin;
using NbaStore.Services.Products.Interfaces;
using NbaStore.Services.Products;
using NbaStore.Services.TeamProducts.Interfaces;
using NbaStore.Services.TeamProducts;
using NbaStore.App.Infrastructure;
using NbaStore.Services.ShopCart.Interfaces;
using NbaStore.Services.ShopCart;
using NbaStore.Services.Orders.Interfaces;
using NbaStore.Services.Orders;
using NbaStore.Services.Brands.Interfaces;
using NbaStore.Services.Brands;
using NbaStore.App.Filters;

namespace NbaStore.App
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<NbaStoreDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<User, IdentityRole>()
                .AddDefaultUI()
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<NbaStoreDbContext>();

            services.AddSingleton<IEmailSender,EmailSender>();
            services.AddSingleton<IShoppingCartManager, ShoppingCartManager>();
            services.AddSingleton(new ProductCountToBuy());

            services.AddAuthentication()
               .AddFacebook(option =>
               {
                   option.AppId = this.Configuration.GetSection("ExternalAuthentication:Facebook:AppId").Value;
                   option.AppSecret = this.Configuration.GetSection("ExternalAuthentication:Facebook:AppSecret").Value;
               })
               .AddGoogle(googleOptions =>
               {
                   googleOptions.ClientId = this.Configuration.GetSection("ExternalAuthentication:Google:AppId").Value;
                   googleOptions.ClientSecret = this.Configuration.GetSection("ExternalAuthentication:Google:AppSecret").Value;
               });
            
            services.Configure<IdentityOptions>(options =>
            {
                options.Password = new PasswordOptions()
                {
                    RequiredLength = 4,
                    RequiredUniqueChars = 1,
                    RequireDigit = true,
                    RequireLowercase = true,
                    RequireUppercase = false,
                    RequireNonAlphanumeric = false
                };

                options.SignIn.RequireConfirmedEmail = true;
            });
            
            services.AddAutoMapper();

            RegisterServiceLayer(services);

            services.AddSession();

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
            IHostingEnvironment env,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseSession();

            app.UseAuthentication();

            app.SeedDatabase();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                   name: "area",
                   template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private static void RegisterServiceLayer(IServiceCollection services)
        {
            services.AddScoped<IAdminUsersService, AdminUsersService>();
            services.AddScoped<IAdminTeamsService, AdminTeamsService>();
            services.AddScoped<IAdminProductsService, AdminProductsService>();
            services.AddScoped<IAdminImagesService, AdminImagesService>();
            services.AddScoped<IProductsService, ProductsService>();
            services.AddScoped<ITeamProductsServices, TeamProductsServices>();
            services.AddScoped<IOrdersService, OrdersService>();
            services.AddScoped<IBrandsService, BrandsService>();
        }
    }
}
