namespace WebServer.Application.Views.ShoppingCart
{
    using System.IO;
    using WebServer.Application.Models;
    using WebServer.Server.Contracts;

    public class CartView:IView
    {
        private const string ReplacementSnipProducts = "<!--Products-->";
        private const string ReplacementSnipTotalCost = " <!--TotalCost-->";

        private string cartProductsAsHtml;
        private string totalPriceAsHtml;

        public CartView(string cartProductsAsHtml, string totalPriceAsHtml)
        {
            this.cartProductsAsHtml = cartProductsAsHtml;
            this.totalPriceAsHtml = totalPriceAsHtml;
        }

        public string View()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            string newPath = Path.GetFullPath(Path.Combine(currentDirectory, @"..\..\..\"));
            string html = File.ReadAllText(newPath + @".\Application\Resources\cart.html");

            var products = cartProductsAsHtml;
            var totalCost = totalPriceAsHtml;

            html = html.Replace(ReplacementSnipProducts, products);
            html = html.Replace(ReplacementSnipTotalCost, totalCost);
            
            return html;
        }
    }
}
