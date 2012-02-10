using System.Collections.ObjectModel;
using System.Net;
using System.Runtime.Serialization.Json;
using GHIssues.Service;
using GHIssues.Service.Models;
using GHIssues.Utils;
using Microsoft.Phone.Reactive;
using System.Windows;

namespace GHIssues.ViewModels
{
    public class IssuesViewModel : ViewModel
    {
        public ObservableCollection<Issue> Items { get;private set; }

        public IssuesViewModel()
        {
                this.Items = new ObservableCollection<Issue>();
            }

            public void LoadIssues(string repo)
            {
                WebRequest req = GHRequest.Create(AppSettings.AuthInfo, ResourceType.Issue, AppSettings.User, repo);
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
                    .Subscribe(i =>
                    {
                        this.IsProgress = false;
                        this.Items.Add(i);
                        this.RaisePropertyChanged(() => this.Items);
                    },
                    ex =>
                    {
                    } );
        }
    }
}
