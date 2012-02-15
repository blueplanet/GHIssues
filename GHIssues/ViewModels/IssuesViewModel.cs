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
    public class IssuesViewModel : ViewModel
    {
        private string repo;

        public ObservableCollection<Issue> Items { get;private set; }

        public DelegateCommand SelectionChangedCommand { get; set; }

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

        public IssuesViewModel()
        {
            this.SelectedIndex = -1;
            this.Items = new ObservableCollection<Issue>();
            this.SelectionChangedCommand = new DelegateCommand(
                () =>
                {
                    Issue curr = (Issue)this.Items[this.SelectedIndex];
                    string url = string.Format("/Views/IssueDetailView.xaml?repo={0}&issue={1}", this.repo, curr.number);
                    this.Frame.Navigate(new Uri(url, UriKind.Relative));
                    this.SelectedIndex = -1;
                },
                () => 
                {
                    return this.SelectedIndex != -1;
                }
            );
        }

        public void LoadIssues(string _repo)
        {
            this.repo = _repo;
            WebRequest req = GHRequest.Create(AppSettings.AuthInfo, ResourceType.Issues, AppSettings.User, this.repo);
            Observable.FromAsyncPattern<WebResponse>(req.BeginGetResponse, req.EndGetResponse)()
                .Select(r =>
                {
                    using (var stream = r.GetResponseStream())
                    {
                        var serialzer = new DataContractJsonSerializer(typeof(Issue[]));
                        return (Issue[])serialzer.ReadObject(stream);
                    }
                })
                .SelectMany(x => x)
                .ObserveOnDispatcher()
                .Subscribe(
                i =>
                    {
                        this.IsProgress = false;
                        this.Items.Add(i);
                        this.RaisePropertyChanged(() => this.Items);
                    },
                ex =>
                    {
                    }
                );
        }
    }
}
