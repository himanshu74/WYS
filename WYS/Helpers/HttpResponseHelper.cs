using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WYS.Helpers
{
    public static class HttpResponseHelper
    {

        public static HttpResponseException GetInternalServerErrorResponse(Exception exception)
        {
            var errorMesg = new HttpResponseMessage(HttpStatusCode.InternalServerError) 
            { Content = new StringContent(exception.Message) };

             throw new HttpResponseException(errorMesg);
        }

    }
}