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

        private int selectedIndex;
        public int SelectedIndex
        {
            get { return this.selectedIndex; }
            set
            {
                if (this.selectedIndex == value)
                {
                    return;
                }

                this.selectedIndex = value;
                RaisePropertyChanged(() => this.SelectedIndex);
            }
        }

        public bool SettingAreNotConfigured
        {
            get 
            {
                return string.IsNullOrEmpty(AppSettings.AuthInfo);
            }
        }

        public ReposViewModel()
        {
            this.SelectedIndex = -1;
            this.Items = new ObservableCollection<Repository>();

            this.AppSettingsCommand = new DelegateCommand(
                () => this.Frame.Navigate(new Uri("/Views/AppSettingsView.xaml", UriKind.Relative))
            );

            this.LoadDataCommand = new DelegateCommand(
                () =>
                {
                    this.RaisePropertyChanged(() => this.SettingAreNotConfigured);
                    this.LoadData();
                }
            );

            this.DisplayIssuesCommand = new DelegateCommand(
                () =>
                {
                    Repository repo = this.Items[this.SelectedIndex];
                    this.Frame.Navigate(new Uri("/Views/IssuesView.xaml?repo=" + repo.name, UriKind.Relative));
                    this.SelectedIndex = -1;
                },
                () =>
                {
                    return this.SelectedIndex != -1;
                }
            );
        }

        public DelegateCommand AppSettingsCommand { get; set; }
        public DelegateCommand LoadDataCommand { get; set; }
        public DelegateCommand DisplayIssuesCommand { get; set; }

        protected void LoadData()
        {
            if (this.SettingAreNotConfigured)
            {
                return;
            }

            this.IsProgress = true;

            HttpWebRequest req = GHRequest.Create(AppSettings.AuthInfo, ResourceType.User);

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
                },
                e => { }
                );
        }
    }
}
