using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Runtime.Serialization.Json;
using GHIssues.Service;
using GHIssues.Service.Models;
using GHIssues.Utils;
using Microsoft.Phone.Reactive;
using Microsoft.Practices.Prism.Commands;

namespace GHIssues.ViewModels
{
    public class ReposViewModel : ViewModel
    {
        public ObservableCollection<Repository> Items { get; private set; }

        public bool SettingAreNotConfigured
        {
            get 
            {
                return string.IsNullOrEmpty(AppSettings.User);
            }
        }

        public ReposViewModel()
            : base()
        {
            this.Items = new ObservableCollection<Repository>();

            this.AppSettingsCommand = new DelegateCommand(
                () => this.Frame.Navigate(new Uri("/Views/AppSettingsView.xaml", UriKind.Relative))
            );
        }

        public DelegateCommand AppSettingsCommand { get; set; }

        protected override void LoadData()
        {
            if (this.SettingAreNotConfigured)
            {
                return;
            }

            this.IsProgress = true;

            HttpWebRequest req = GHRequest.Create(ResourceType.User, AppSettings.AuthInfo);

            Observable.FromAsyncPattern<WebResponse>(req.BeginGetResponse, req.EndGetResponse)()
                .Select(r =>
                {
                    using (var stream = r.GetResponseStream())
                    {
                        var serializer = new DataContractJsonSerializer(typeof(Repository[]));
                        return (Repository[])serializer.ReadObject(stream);
                    }
                })
                .SelectMany(x => x)
                .ObserveOnDispatcher()
                .Subscribe(i =>
                {
                    this.IsProgress = false;
                    this.Items.Add(i);
                    this.RaisePropertyChanged(() => this.Items);
                });
        }
    }
}
