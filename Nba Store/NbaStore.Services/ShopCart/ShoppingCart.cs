using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NbaStore.Services.ShopCart
{
    public class ShoppingCart
    {
        private readonly IList<CartItem> items;
        
        public ShoppingCart()
        {
            this.items = new List<CartItem>();
        }
        
        public IEnumerable<CartItem> Items => new List<CartItem>(this.items);

        public void AddToCart(int productId,int quantity,string size)
        {
            var cartItem = this.items.SingleOrDefault(x => x.ProductId == productId && x.Size==size);
            if (cartItem == null)
            {
                cartItem = new CartItem()
                {
                    ProductId = productId,
                    Quantity = quantity,
                    Size=size
                };

                this.items.Add(cartItem);
            }
            else
            {
                cartItem.Quantity += quantity;
            }
        }

        public void RemoveFromCart(int productId,string size)
        {
            var cartItem = this.items
                .FirstOrDefault(x => x.ProductId == productId && x.Size==size);

            if (cartItem != null)
            {
                cartItem.Quantity--;
                if (cartItem.Quantity <= 0)
                {
                    this.items.Remove(cartItem);
                }
            }
        }

        public void Clear()
        {
            this.items.Clear();
        }
    }
}
