using System;
using System.Net;

namespace GHIssues.Service
{
    public static class GHRequest
    {
        public static HttpWebRequest Create(Uri uri, string basicAuthInfo)
        {
            HttpWebRequest request = HttpWebRequest.CreateHttp(uri);
            request.Headers[HttpRequestHeader.Authorization] = "Basic " + basicAuthInfo;

            return request;
        }
    }
}
