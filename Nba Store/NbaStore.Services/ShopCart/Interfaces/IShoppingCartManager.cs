using NbaStore.Common.ViewModels.ShoppingCart;
using System;
using System.Collections.Generic;
using System.Text;

namespace NbaStore.Services.ShopCart.Interfaces
{
    public interface IShoppingCartManager
    {
        void AddToCart(string id, int productId, int quantity, string size);
            
        void RemoveFromCart(string id,int productId,string size);

        IEnumerable<CartItem> GetItems(string id);

        void Clear(string id);
    }
}
