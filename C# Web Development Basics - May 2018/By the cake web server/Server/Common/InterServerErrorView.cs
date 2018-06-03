using System;
using WebServer.Server.Contracts;

namespace WebServer.Server.Common
{
    public class InterServerErrorView:IView
    {
        private readonly Exception exception;

        public InterServerErrorView(Exception exception)
        {
            this.exception = exception;
        }

        public string View()
        {
            return $"<h1>{this.exception}</h1><h2>{this.exception.StackTrace}</h2>";
        }
    }
}
