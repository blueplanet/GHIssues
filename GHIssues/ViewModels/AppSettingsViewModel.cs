using GHIssues.Utils;
using Microsoft.Practices.Prism.Commands;

namespace GHIssues.ViewModels
{
    public class AppSettingsViewModel : ViewModel
    {
        public AppSettingsViewModel()
            : base()
        {
            this.SaveCommand = new DelegateCommand(this.Save);
            this.CancelCommand = new DelegateCommand(this.Cancel);
        }

        protected override void LoadData()
        {
            this.user = AppSettings.User;
            this.password = AppSettings.AuthInfo;
        }

        private string user;
        public string User
        {
            get { return this.user; }
            set
            {
                if (this.user == value)
                {
                    return;
                }

                this.user = value;
                RaisePropertyChanged(() => this.User);
            }
        }

        private string password;
        public string Password
        {
            get { return this.password; }
            set
            {
                if (this.password == value)
                {
                    return;
                }

                this.password = value;
                RaisePropertyChanged(() => this.Password);
            }
        }

        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand CancelCommand { get; set; }

        private void Save()
        {
            AppSettings.User = this.User;
            AppSettings.SetAuthInfo(this.User, this.Password);

            GoBack();
        }

        private bool CanSave()
        {
            return !string.IsNullOrEmpty(this.User) && !string.IsNullOrEmpty(this.Password);
        }

        private void Cancel()
        {
            GoBack();
        }

        private bool CanCancel()
        {
            return !string.Equals(this.User, AppSettings.User);
        }

        private void GoBack()
        {
            if (this.Frame.CanGoBack)
            {
                this.Frame.GoBack();
            }
        }
    }
}
