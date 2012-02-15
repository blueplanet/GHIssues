using System.Windows.Navigation;
using GHIssues.ViewModels;
using Microsoft.Phone.Controls;

namespace GHIssues.Views
{
    public partial class IssueDetailView : PhoneApplicationPage
    {
        public IssueDetailView()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (NavigationContext.QueryString.ContainsKey("repo") &&
                NavigationContext.QueryString.ContainsKey("issue"))
            {
                string repo = NavigationContext.QueryString["repo"];
                int issue = int.Parse(NavigationContext.QueryString["issue"]);

                IssueDetailViewModel vm = new IssueDetailViewModel();

                vm.LoadIssue(repo, issue);

                this.DataContext = vm;
            }
        }
    }
}