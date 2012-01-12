using System.Linq;
using GHIssues.Service.Models;
using GHIssues.ViewModels;
using GHIssuesTests.TestSupport;
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GHIssuesTests.ViewModel
{
    [TestClass]
    public class ReposViewModelTest : BaseTest
    {
        [TestMethod]
        [Asynchronous]
        public void GetReposList()
        {
            ReposViewModel vm = new ReposViewModel();
            vm.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName != "Items")
                {
                    return;
                }
                
                vm.Items.Is(items => items.Count > 0);
                
                Repository repo = vm.Items.Last();
                repo.name.IsNotNull();
                repo.description.IsNotNull();

                EnqueueTestComplete();
            };
        }

    } 
}
