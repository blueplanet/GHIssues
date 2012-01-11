using System;
using System.ComponentModel;
using System.Text;

namespace GHIssues.Utils
{
    public static class AppSettings
    {
        private static readonly ISettingStore store = DesignerProperties.IsInDesignTool ? (ISettingStore)new DesignSettingStore() : new SettingStore();

        public static string User
        {
            get { return store.User; }
            set { store.User = value; }
        }

        public static string AuthInfo
        {
            get { return store.AuthInfo; }
        }

        public static void SetAuthInfo(string user, string password)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(string.Format("{0}:{1}", user, password));
            store.AuthInfo = Convert.ToBase64String(bytes);
        }
    }
}
