using GHIssues.Utils;
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GHIssuesTests.TestSupport
{
    [TestClass]
    public class BaseTest : SilverlightTest
    {
        [TestInitialize]
        public void Setup()
        {
            // githubのユーザ情報を設定する
            AppSettings.User = "";
            AppSettings.SetAuthInfo("", "");
        }
    }
}
