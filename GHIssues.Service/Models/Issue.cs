using System;

namespace GHIssues.Service.Models
{
    public class Issue
    {
        public string state { get; set; }
        public string title { get; set; }
        public string body { get; set; }
        public int commits { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }
}
