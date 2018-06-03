using System;
using System.Collections.Generic;
using System.Text;
using WebServer.GameStoreApplication.Helpers;
using WebServer.GameStoreApplication.Models;
using WebServer.Server.Contracts;
using WebServer.Server.Http.Contracts;
using WebServer.Server.HTTP;

namespace WebServer.GameStoreApplication.Views
{
    public class HtmlView : IView
    {
        private const string ReplacementNav= "<!--Nav-->";
        private const string ReplacementHtml= "<!--Html-->";
        private const string ReplacementFooter = "<!--Footer-->";
        private string html;
        private Dictionary<string, string> replaceData;
        IHttpRequest req;

        public HtmlView(string html, Dictionary<string, string> replaceData, IHttpRequest req)
        {
            this.html = html;
            this.replaceData = replaceData;
            this.req = req;
        }

        public string View()
        {
            var layout = GetFileFromDirectory.GetFileAllTextByCurrentName(@"home\layout", "html");
            var resultHtml = string.Empty;

            if (html != @"home\layout")
            {
                resultHtml = GetFileFromDirectory.GetFileAllTextByCurrentName(html, "html");

                foreach (var data in this.replaceData)
                {
                    var replacementShip = data.Key;
                    var htmlToReplace = data.Value;

                    resultHtml = resultHtml.Replace(replacementShip, htmlToReplace);
                }
            }
            
            var resultNav = string.Empty;
            if (req.Session.Contains(SessionStore.CurrentAdminKey))
            {
                resultNav = GetFileFromDirectory.GetFileAllTextByCurrentName(@"navs\adminnav", "html");
            }
            else if (req.Session.Contains(SessionStore.CurrentUserKey))
            {
                resultNav = GetFileFromDirectory.GetFileAllTextByCurrentName(@"navs\usernav", "html");
            }
            else
            {
                resultNav = GetFileFromDirectory.GetFileAllTextByCurrentName(@"navs\guestnav", "html");
            }

            var footer= GetFileFromDirectory.GetFileAllTextByCurrentName(@"footer\footer", "html");

            layout = layout.Replace(ReplacementNav, resultNav);
            layout = layout.Replace(ReplacementHtml, resultHtml);
            layout = layout.Replace(ReplacementFooter, footer);

            return layout;
        }
    }
}
