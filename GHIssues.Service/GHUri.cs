using System;

namespace GHIssues.Service
{
    public static class GHUri
    {
        private const string BASE_URL = "https://api.github.com";

        public const string METHOD_GET = "GET";

        public static Uri Build(ResourceType type, params string[] info)
        {
            Uri uri = null;

            switch (type)
            {
                case ResourceType.User:
                    uri = new Uri(string.Format("{0}/user/repos", BASE_URL));
                    break;
                case ResourceType.Repository:
                    break;
                case ResourceType.IssueDetail:
                    uri = new Uri(string.Format("{0}/repos/{1}/{2}/issues/{3}", BASE_URL, info[0], info[1], info[2]));
                    break;
                case ResourceType.Issues:
                    uri = new Uri(string.Format("{0}/repos/{1}/{2}/issues", BASE_URL, info[0], info[1]));
                    break;
                case ResourceType.Comment:
                    break;
                default:
                    throw new ArgumentException("Resource Type is not support.");
            }

            return uri;
        }
    }

    public enum ResourceType
    {
        User = 0,
        Repository,
        IssueDetail,
        Issues,
        Comment
    }

}
