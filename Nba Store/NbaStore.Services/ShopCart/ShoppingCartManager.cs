using AutoMapper;
using NbaStore.Common.ViewModels.ShoppingCart;
using NbaStore.Data;
using NbaStore.Services.ShopCart.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NbaStore.Services.ShopCart
{
    public class ShoppingCartManager :IShoppingCartManager
    {
        private readonly ConcurrentDictionary<string, ShoppingCart> carts;
        
        public ShoppingCartManager() 
        {
            this.carts = new ConcurrentDictionary<string, ShoppingCart>();
        }

        public void AddToCart(string id, int productId, int quantity, string size)
        {
            var shopingCart = this.GetShoppingCart(id);

            shopingCart.AddToCart(productId, quantity,size);
        }
        
        public void RemoveFromCart(string id, int productId,string size)
        {
            var shopingCart = this.GetShoppingCart(id);

            shopingCart.RemoveFromCart(productId,size);
        }

        public void Clear(string id)
        {
            this.GetShoppingCart(id).Clear();
        }

        public IEnumerable<CartItem> GetItems(string id)
        {
            var shopingCart = this.GetShoppingCart(id);
            
            var cartItems= new List<CartItem>(shopingCart.Items);

            return cartItems;
        }
        
        private ShoppingCart GetShoppingCart(string id)
        {
            return this.carts.GetOrAdd(id, new ShoppingCart());
        }
        
    }
}
