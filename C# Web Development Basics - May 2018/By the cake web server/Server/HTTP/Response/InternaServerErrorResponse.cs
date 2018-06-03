using System;
using WebServer.Server.Common;
using WebServer.Server.Enums;
using WebServer.Server.Http.Response;

namespace WebServer.Server.HTTP.Response
{
    public class InternaServerErrorResponse : ViewResponse
    {
        public InternaServerErrorResponse(Exception ex) 
            : base(HttpStatusCode.InternalServerError,new InterServerErrorView(ex))
        {
        }
    }
}
