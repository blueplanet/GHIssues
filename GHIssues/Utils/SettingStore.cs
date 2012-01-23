using System.Collections.Generic;
using System.IO.IsolatedStorage;

namespace GHIssues.Utils
{
    public class SettingStore : ISettingStore
    {
        private const string USER_KEY = "User";
        private const string AUTH_INFO_KEY = "AuthInfo";

        private readonly IsolatedStorageSettings store = IsolatedStorageSettings.ApplicationSettings;

        public string User
        {
            get { return Get<string>(USER_KEY, string.Empty); }
            set { Set(USER_KEY, value); }
        }

        public string AuthInfo
        {
            get { return Get<string>(AUTH_INFO_KEY, string.Empty); }
            set { Set(AUTH_INFO_KEY, value); }
        }

        private T Get<T>(string key, T defaultValue)
        {
            T value;

            try
            {
                value = (T)store[key];
            }
            catch (KeyNotFoundException)
            {
                value = defaultValue;
            }

            return value;
        }

        private void Set(string key, object value)
        {
            bool valueChanged = false;

            try
            {
                if (store[key] != value)
                {
                    store[key] = value;
                    valueChanged = true;
                }
            }
            catch (KeyNotFoundException)
            {
                store.Add(key, value);
                valueChanged = true;
            }

            if (valueChanged)
            {
                store.Save();
            }
        }
    }
}
