using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NbaStore.Common.ViewModels.Orders;
using NbaStore.Data;
using NbaStore.Models;
using NbaStore.Services.Orders.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NbaStore.Services.Orders
{
    public class OrdersService : BaseEFService, IOrdersService
    {
        public OrdersService(NbaStoreDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }
        
        public  IEnumerable<UserOrdersViewModel> GetUserOrders(string userId)
        {
            var userOrders = this.DbContext.Orders
                .Include(o => o.OrderProducts)
                .Where(u => u.UserId == userId)
                .ToList();

            return this.Mapper.Map<IEnumerable<UserOrdersViewModel>>(userOrders);
        }

        public IEnumerable<OrderProductsViewModel> GetOrderProducts(int orderId)
        {
            var orderProducts = this.DbContext.OrderProducts
                .Where(o=>o.OrderId==orderId)
                .ToList();

            return this.Mapper.Map<IEnumerable<OrderProductsViewModel>>(orderProducts);
        }
    }
}
