namespace WebServer.Application.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using WebServer.Application.Data;
    using WebServer.Application.Data.Models;
    using WebServer.Application.Models;
    using WebServer.Application.Views;
    using WebServer.Server.Enums;
    using WebServer.Server.Http.Contracts;
    using WebServer.Server.Http.Response;
    using WebServer.Server.HTTP;

    public class CartController:Controller
    {
        private readonly OrderService orderService;

        public CartController()
        {
            this.orderService = new OrderService();
        }

        public IHttpResponse CartGet(IHttpRequest req)
        {
            var cart = req.Session.Get<Cart>(SessionStore.CurrentCartKey);
            var cartProducts = cart.GetAllProducts();
            var cartTotalPrice = cart.CalculateTotalCostPrice().ToString();
            
            var sb = new StringBuilder();
            var productsAsHtml=this.GenerateProductsAsHtml(cartProducts, sb);

            this.ViewData["<!--Products-->"] = productsAsHtml;
            if (cart.GetProductsCount() == 0)
            {
                this.ViewData["<!--Products-->"] = "<div>No items in your cart</div>";
            }
            this.ViewData[" <!--TotalCost-->"] = $"<div>Total Cost: {cartTotalPrice:f2}</div>";

            return new ViewResponse(HttpStatusCode.Ok, new HtmlView(@"cart\cart", this.ViewData));
        }
        
        public IHttpResponse CartPost(IHttpRequest req)
        {
            var cart = req.Session.Get<Cart>(SessionStore.CurrentCartKey);

            if (req.FormData.ContainsKey("deleteCakeId"))
            {
                var cakeToRemoveId = int.Parse(req.FormData["deleteCakeId"]);
                
                cart.Remove(cakeToRemoveId);
                var cartTotalPrice = cart.CalculateTotalCostPrice();
                var cartProducts = cart.GetAllProducts();
                
                var sb = new StringBuilder();
                var productsAsHtml = this.GenerateProductsAsHtml(cartProducts, sb);
                
                this.ViewData["<!--Products-->"] = productsAsHtml;
                if (cart.GetProductsCount() == 0)
                {
                    this.ViewData["<!--Products-->"] = "<div>No items in your cart</div>";
                }
                this.ViewData[" <!--TotalCost-->"] = $"<div>Total Cost: {cartTotalPrice:f2}</div>";

                return new ViewResponse(HttpStatusCode.Ok, new HtmlView(@"cart\cart", this.ViewData));
            }

            return new RedirectResponse($"/successorder");
        }

        public IHttpResponse SuccessOrder(IHttpRequest req)
        {
            var cart = req.Session.Get<Cart>(SessionStore.CurrentCartKey);

            var username = req.Session.Get(SessionStore.CurrentUserKey).ToString();
            var cartProducts = cart.GetAllProducts();
            orderService.SendOrder(username, cartProducts);
            cart.Clear();

            return new ViewResponse(HttpStatusCode.Ok, new HtmlView(@"cart\successorder", this.ViewData));
        }

        public IHttpResponse Order(IHttpRequest req)
        {
            var username = req.Session.Get(SessionStore.CurrentUserKey).ToString();
            var sb = new StringBuilder();

            using (var db = new ByTheCakeDbContext())
            {
                var user = db.Users.SingleOrDefault(x => x.Username == username);
                
                var orders = db.Orders
                    .Where(x=>x.UserId==user.Id)
                    .Select(x => new
                    {
                        OrderId = x.Id,
                        CreatedOn = x.DateOfCreation,
                        Sum = x.OrderProducts.Sum(p => p.Product.Price)
                    })
                    .ToList();
                
                foreach(var order in orders)
                {
                    sb.AppendLine($"<tr>" +
                        $"<th><a href=\"/orderDetails/{order.OrderId}\">{order.OrderId}</a></t>" +
                        $"<th>{order.CreatedOn}</th>" +
                        $"<th>{order.Sum:F2}</th>" +
                        $"</tr>");
                }
            }

            this.ViewData[" <!--OrdersInformation-->"] = sb.ToString().Trim();

            return new ViewResponse(HttpStatusCode.Ok, new HtmlView(@"cart\order", this.ViewData));
        }

        public IHttpResponse OrderDetails(IHttpRequest req)
        {
            var orderId = int.Parse(req.UrlParameters["id"]);

            this.ViewData["<!--OrderId-->"] = $"<h1>Order {orderId}</h1>";

            var products = orderService.GetProductsByGivenOrderId(orderId);

            var sb = new StringBuilder();
            foreach(var product in products)
            {
                sb.AppendLine($"<tr>" +
                        $"<th><a href=\"/cakeDetails/{product.Id}\">{product.Name}</a></t>" +
                        $"<th>{product.Price}</th>" +
                        $"</tr>");
            }

            this.ViewData["<!--ProductInformation-->"] = sb.ToString().Trim();

            var dateOfCreation = orderService.GetDateOfCreation(orderId);
            this.ViewData["<!--Date-->"] = $"<div>Created On: {dateOfCreation}</div>";

            return new ViewResponse(HttpStatusCode.Ok, new HtmlView(@"cart\orderdetail", this.ViewData));
        }

        private string GenerateProductsAsHtml(Dictionary<int, List<Product>> cartProducts, StringBuilder sb)
        {
            foreach (var kvp in cartProducts)
            {
                var sameProductCount = kvp.Value.Count;
                var product = kvp.Value.First();
                var productId = kvp.Key;
                var productPrice = kvp.Value.Sum(p => p.Price);

                var deleteForm = $"<form method=\"post\">" +
                                $"<p>Name: {product.Name} Price: $ {productPrice} Count:{sameProductCount}</p>" +
                                $"<input name=\"deleteCakeId\" value=\"{productId}\"type=\"hidden\"  />" +
                            "<input type=\"submit\" value=\"Remove\" />" +
                            "</form>";
                sb.AppendLine($"{deleteForm}");
            }

            return sb.ToString().Trim();
        }
    }
}
