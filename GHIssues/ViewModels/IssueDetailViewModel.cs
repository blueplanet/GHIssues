using System.Net;
using System.Runtime.Serialization.Json;
using GHIssues.Service;
using GHIssues.Service.Models;
using GHIssues.Utils;
using Microsoft.Phone.Reactive;

namespace GHIssues.ViewModels
{
    public class IssueDetailViewModel : ViewModel
    {
        #region Property
        private string title;
        public string Title
        {
            get { return this.title; }
            set
            {
                if (this.title == value)
                {
                    return;
                }

                this.title = value;
                RaisePropertyChanged(() => this.Title);
            }
        }

        private string state;
        public string State
        {
            get { return this.state; }
            set
            {
                if (this.state == value)
                {
                    return;
                }

                this.state = value;
                RaisePropertyChanged(() => this.State);
            }
        }

        private string body;
        public string Body
        {
            get { return this.body; }
            set
            {
                if (this.body == value)
                {
                    return;
                }

                this.body = value;
                RaisePropertyChanged(() => this.Body);
            }
        }
        #endregion
       
        public void LoadIssue(string repo, int id)
        {
            WebRequest req = GHRequest.Create(AppSettings.AuthInfo, ResourceType.IssueDetail, AppSettings.User, repo, id.ToString());
            Observable.FromAsyncPattern<WebResponse>(req.BeginGetResponse, req.EndGetResponse)()
                .Select(r =>
                {
                    using (var stream = r.GetResponseStream())
                    {
                        var serialzer = new DataContractJsonSerializer(typeof(Issue));
                        return (Issue)serialzer.ReadObject(stream);
                    }
                })
                .ObserveOnDispatcher()
                .Subscribe(
                i =>
                {
                    this.IsProgress = false;

                    this.Title = i.title;
                    this.State = i.state;
                    this.Body = i.body;
                    
                },
                ex =>
                {
                }
                );
        }
    }
}
