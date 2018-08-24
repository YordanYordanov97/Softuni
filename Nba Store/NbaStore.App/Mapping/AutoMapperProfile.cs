using AutoMapper;
using NbaStore.Common.BindingModels.Admin.Images;
using NbaStore.Common.BindingModels.Admin.Products;
using NbaStore.Common.BindingModels.Admin.Teams;
using NbaStore.Common.ViewModels.Admin.Products;
using NbaStore.Common.ViewModels.Admin.Teams;
using NbaStore.Common.ViewModels.Admin.Users;
using NbaStore.Common.ViewModels.Orders;
using NbaStore.Common.ViewModels.ShoppingCart;
using NbaStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NbaStore.App.Mapping
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            this.CreateMap<User, UserIndexViewModel>();

            this.CreateMap<User, UserDetailsViewModel>()
                .ForMember(dto => dto.Orders, dest => dest.MapFrom(o=>o.Orders));

            this.CreateMap<Team, TeamIndexViewModel>()
                .ForMember(dto => dto.ProductsCount, dest => dest.MapFrom(p => p.Products.Count));

            this.CreateMap<Team, TeamDetailsViewModel>();

            this.CreateMap<TeamBindingModel, Team>();

            this.CreateMap<Team, TeamBindingModel>();

            this.CreateMap<Product, ProductIndexViewModel>();

            this.CreateMap<ProductBindingModel, Product>();

            this.CreateMap<Product,ProductBindingModel>();

            this.CreateMap<Product, ProductDetailsViewModel>()
                .ForMember(dto=>dto.Team,dest=>dest.MapFrom(t=>t.Team))
                .ForMember(dto => dto.Images, dest => dest.MapFrom(im => im.Images));

            this.CreateMap<ImageBindingModel, Image>();

            this.CreateMap<Image, ImageBindingModel>();

            this.CreateMap<Product, ProductCartViewModel>();

            this.CreateMap<Order, UserOrdersViewModel>()
                .ForMember(dto => dto.OrderProductsCount, dest => dest.MapFrom(op => op.OrderProducts.Count));

            this.CreateMap<OrderProduct, OrderProductsViewModel>();
        }
    }
}
