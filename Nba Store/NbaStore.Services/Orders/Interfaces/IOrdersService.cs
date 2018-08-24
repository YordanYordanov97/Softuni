using NbaStore.Common.ViewModels.Orders;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NbaStore.Services.Orders.Interfaces
{
    public interface IOrdersService
    {
        IEnumerable<UserOrdersViewModel> GetUserOrders(string userId);

        IEnumerable<OrderProductsViewModel> GetOrderProducts(int orderId);
    }
}
