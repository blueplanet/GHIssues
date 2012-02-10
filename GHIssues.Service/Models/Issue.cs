
namespace GHIssues.Service.Models
{
    public class Issue
    {
        public string state { get; set; }
        public string title { get; set; }
        public string body { get; set; }
        public int commits { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
    }
}
