namespace WebServer.Application.Controllers
{
    using System.Text;
    using WebServer.Application.Models;
    using WebServer.Application.Views.ShoppingCart;
    using WebServer.Server.Enums;
    using WebServer.Server.Http.Contracts;
    using WebServer.Server.Http.Response;
    using WebServer.Server.HTTP;

    public class CartController
    {
        public IHttpResponse CartGet(IHttpRequest req)
        {
            var cart = req.Session.Get<Cart>(SessionStore.CurrentCartKey);
            var cartProducts = cart.GetAllProductsAsHtml();
            var cartTotalPrice = cart.CalculateTotalCostPrice();
            
            return new ViewResponse(HttpStatusCode.Ok, new CartView(cartProducts, cartTotalPrice));
        }

        public IHttpResponse CartPost(IHttpRequest req)
        {
            var cart = req.Session.Get<Cart>(SessionStore.CurrentCartKey);
            
            if (req.FormData.ContainsKey("deleteCake"))
            {
                var cakeToRemove = req.FormData["deleteCake"];
                
                cart.Remove(cakeToRemove);
                req.Session.Add(SessionStore.CurrentCartKey, cart);

                var cartProducts = cart.GetAllProductsAsHtml();
                var cartTotalPrice = cart.CalculateTotalCostPrice();

                return new ViewResponse(HttpStatusCode.Ok, new CartView(cartProducts, cartTotalPrice));
            }

            return new RedirectResponse($"/successorder");
        }

        public IHttpResponse SuccessOrder(IHttpRequest req)
        {
            var cart = req.Session.Get<Cart>(SessionStore.CurrentCartKey);
            cart.Clear();
            req.Session.Add(SessionStore.CurrentCartKey, cart);
            return new ViewResponse(HttpStatusCode.Ok, new SuccessOrderView());
        }
    }
}
