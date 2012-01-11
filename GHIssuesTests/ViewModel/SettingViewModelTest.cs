using GHIssues.Utils;
using GHIssues.ViewModels;
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GHIssuesTests.ViewModel
{
    [TestClass]
    public class SettingViewModelTest : SilverlightTest
    {
        [TestMethod]
        public void CommandTest()
        {
            var vm = new AppSettingsViewModel();

            vm.User.IsNull();
            vm.Password.IsNull();
            vm.SaveCommand.CanExecute().Is(false);
            vm.CancelCommand.CanExecute().Is(false);

            vm.User = "test";
            vm.SaveCommand.CanExecute().Is(false);

            vm.Password = "aaaa";

            vm.User.Is("test");
            vm.Password.Is("aaaa");
            vm.SaveCommand.CanExecute().Is(true);

            AppSettings.User.Is("test");
            AppSettings.AuthInfo.IsNotNull();
        }
    }
}
