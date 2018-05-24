namespace WebServer.Application.Controllers
{
    using WebServer.Application.Models;
    using WebServer.Application.Views;
    using WebServer.Server.Enums;
    using WebServer.Server.Http.Contracts;
    using WebServer.Server.Http.Response;
    using WebServer.Server.HTTP;

    public class CartController:Controller
    {
        public IHttpResponse CartGet(IHttpRequest req)
        {
            var cart = req.Session.Get<Cart>(SessionStore.CurrentCartKey);
            var cartProducts = cart.GetAllProductsAsHtml();
            var cartTotalPrice = cart.CalculateTotalCostPrice();

            this.ViewData["<!--Products-->"] = cartProducts;
            this.ViewData[" <!--TotalCost-->"] = cartTotalPrice;

            return new ViewResponse(HttpStatusCode.Ok, new HtmlView("cart", this.ViewData));
        }

        public IHttpResponse CartPost(IHttpRequest req)
        {
            var cart = req.Session.Get<Cart>(SessionStore.CurrentCartKey);
            
            if (req.FormData.ContainsKey("deleteCake"))
            {
                var cakeToRemove = req.FormData["deleteCake"];
                
                cart.Remove(cakeToRemove);
                
                var cartProducts = cart.GetAllProductsAsHtml();
                var cartTotalPrice = cart.CalculateTotalCostPrice();

                this.ViewData["<!--Products-->"] = cartProducts;
                this.ViewData[" <!--TotalCost-->"] = cartTotalPrice;

                return new ViewResponse(HttpStatusCode.Ok, new HtmlView("cart", this.ViewData));
            }

            return new RedirectResponse($"/successorder");
        }

        public IHttpResponse SuccessOrder(IHttpRequest req)
        {
            req.Session.Get<Cart>(SessionStore.CurrentCartKey).Clear();

            return new ViewResponse(HttpStatusCode.Ok, new HtmlView("successorder", this.ViewData));
        }
    }
}
