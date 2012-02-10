using System.Net;
using System.Windows;
using GHIssues.Service;
using GHIssues.Utils;
using Microsoft.Phone.Reactive;
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
            AppSettings.SetAuthInfo(this.User, this.Password);

            CheckAuthInfo();
        }

        private void CheckAuthInfo()
        {
            this.IsProgress = true;

            HttpWebRequest req = GHRequest.Create(AppSettings.AuthInfo, ResourceType.User);
            Observable.FromAsyncPattern<WebResponse>(req.BeginGetResponse, req.EndGetResponse)()
                .Select(res => res)
                .ObserveOnDispatcher()
                .Subscribe(status =>
                {
                    this.IsProgress = false;

                    bool isOk = ((HttpWebResponse)status).StatusCode == HttpStatusCode.OK;
                    if (isOk)
                    {
                        GoBack();
                    }
                    else
                    {
                        ShowError();
                    }
                },
                e =>
                {
                    this.IsProgress = false;
                    ShowError();
                });
        }

        private void ShowError()
        {
            MessageBox.Show("ユーザまたはパスワードが間違いました。ご確認ください。");
            AppSettings.SetAuthInfo(this.user, string.Empty);
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
