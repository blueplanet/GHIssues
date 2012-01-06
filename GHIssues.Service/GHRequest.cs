using System;
using System.Net;

namespace GHIssues.Service
{
    public static class GHRequest
    {
        public static HttpWebRequest Create(ResourceType type, string basicAuthInfo)
        {
            Uri uri = GHUri.Build(type);

            HttpWebRequest request = HttpWebRequest.CreateHttp(uri);
            request.Headers[HttpRequestHeader.Authorization] = "Basic " + basicAuthInfo;

            return request;
        }
    }
}
