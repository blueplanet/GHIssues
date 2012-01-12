using GHIssues.ViewModels;
using GHIssuesTests.TestSupport;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GHIssuesTests.ViewModel
{
    [TestClass]
    public class AppSettingsViewModelTest : BaseTest
    {
        [TestMethod]
        public void CommandTest()
        {
            var vm = new AppSettingsViewModel();

            vm.User.IsNull();
            vm.Password.IsNull();
            //vm.SaveCommand.CanExecute().Is(false);
            //vm.CancelCommand.CanExecute().Is(false);

            vm.User = "test";
            //vm.SaveCommand.CanExecute().Is(false);

            vm.Password = "aaaa";

            vm.User.Is("test");
            vm.Password.Is("aaaa");
            //vm.SaveCommand.CanExecute().Is(true);

            //vm.SaveCommand.Execute();
            //AppSettings.User.Is("test");
            //AppSettings.AuthInfo.IsNotNull();
        }
    }
}
