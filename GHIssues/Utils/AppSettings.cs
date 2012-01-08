using System.ComponentModel;

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
            set { store.AuthInfo = value; }
        }
    }
}
