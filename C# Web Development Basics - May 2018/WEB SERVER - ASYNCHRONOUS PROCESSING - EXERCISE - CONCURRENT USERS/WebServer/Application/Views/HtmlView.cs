namespace WebServer.Application.Views
{
    using System.Collections.Generic;
    using WebServer.Application.Helpers;
    using WebServer.Server.Contracts;

    public class HtmlView:IView
    {
        private string html;
        private Dictionary<string, string> replaceData;

        public HtmlView(string html, Dictionary<string, string> replaceData)
        {
            this.html = html;
            this.replaceData = replaceData;
        }

        public string View()
        {
            var resultHtml= GetFileFromDirectory.GetFileAllTextByCurrentName(html, "html");

            foreach (var data in this.replaceData)
            {
                var replacementShip = data.Key;
                var htmlToReplace = data.Value;

                resultHtml = resultHtml.Replace(replacementShip, htmlToReplace);
            }
            
            return resultHtml;
        }
    }
}
