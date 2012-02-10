using System.Windows.Navigation;
using Microsoft.Phone.Controls;

namespace GHIssues.Views
{
    public partial class IssuesView : PhoneApplicationPage
    {
        public IssuesView()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (NavigationContext.QueryString.ContainsKey("repo"))
            {
                string repo = NavigationContext.QueryString["repo"];
                this.ViewModel.LoadIssues(repo);
            }
        }
    }
}