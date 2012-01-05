using System.Collections.Generic;
using System.IO.IsolatedStorage;

namespace GHIssues.Utils
{
    public static class AppSettings
    {
        private const string USER_KEY = "User";
        private const string AUTH_INFO_KEY = "AuthInfo";

        private static readonly IsolatedStorageSettings store = IsolatedStorageSettings.ApplicationSettings;

        public static string User
        {
            get { return Get<string>(USER_KEY, string.Empty); }
            set { Set(USER_KEY, value); }
        }

        public static string AuthInfo
        {
            get { return Get<string>(AUTH_INFO_KEY, string.Empty); }
            set { Set(AUTH_INFO_KEY, value); }
        }

        private static T Get<T>(string key, T defaultValue)
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

        private static void Set(string key, object value)
        {
            bool valueChanged = false;

            try
            {
                if (!store[key].Equals(value))
                {
                    store[key] = value;
                    valueChanged = true;
                }
            }
            catch (KeyNotFoundException)
            {
                store.Add(key, valueChanged);
                valueChanged = true;
            }

            if (valueChanged)
            {
                store.Save(); 
            }
        }
    }
}
