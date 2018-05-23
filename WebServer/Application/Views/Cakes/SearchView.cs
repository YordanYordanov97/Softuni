namespace WebServer.Application.Views.Cakes
{
    using System.IO;
    using WebServer.Application.Models;
    using WebServer.Server.Contracts;

    public class SearchView:IView
    {
        private const string ReplacementSnip = "<!--replace-->";
        private const string ReplacementSnipProduct = "<!--productCount-->";
        private const string ReplacementSnipForm= "<!--form-->";
       
        private string cakeHtmlReplace;
        private int productCountReplace;
        private string formValue;

        public SearchView(string cakeHtmlReplace, int productCountReplace,string formValue)
        {
            this.cakeHtmlReplace = cakeHtmlReplace;
            this.productCountReplace = productCountReplace;
            this.formValue = formValue;
        }
        public string View()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            string newPath = Path.GetFullPath(Path.Combine(currentDirectory, @"..\..\..\"));
            string html = File.ReadAllText(newPath + @".\Application\Resources\search.html");
            
            var productsCount = string.Empty;
            if (productCountReplace > 1)
            {
                productsCount = $"<p><a href=\"/cart\">Your Cart:</a> {productCountReplace} products</p>";
            }
            else if (productCountReplace == 1)
            {
                productsCount = $"<p><a href=\"/cart\">Your Cart:</a> {productCountReplace} product</p>";
            }
            
            var form =$"<input name=\"name\" type=\"text\" placeholder=\"Enter Cake name\" value=\"{formValue}\" />";

            html = html.Replace(ReplacementSnipForm, form);
            html = html.Replace(ReplacementSnip, cakeHtmlReplace);
            html = html.Replace(ReplacementSnipProduct, productsCount);


            return html;
        }
    }
}
