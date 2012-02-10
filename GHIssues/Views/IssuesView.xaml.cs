using System.Windows.Navigation;
using GHIssues.ViewModels;
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
                IssuesViewModel vm = new IssuesViewModel();

                vm.LoadIssues(repo);

                this.DataContext = vm;
            }
        }
    }
}