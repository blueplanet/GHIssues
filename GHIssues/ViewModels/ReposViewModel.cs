using System.Collections.ObjectModel;
using System.Net;
using System.Runtime.Serialization.Json;
using GHIssues.Service;
using GHIssues.Service.Models;
using GHIssues.Utils;
using Microsoft.Phone.Reactive;

namespace GHIssues.ViewModels
{
    public class ReposViewModel : ViewModel
    {
        public ObservableCollection<Repository> Items { get; private set; }

        protected override void LoadData()
        {
            this.IsProgress = true;

            HttpWebRequest req = GHRequest.Create(GHUri.Build(ResourceType.User), AppSettings.AuthInfo);
            Observable.FromAsyncPattern<WebResponse>(req.BeginGetResponse, req.EndGetResponse)()
                .ObserveOnDispatcher()
                .Subscribe(res => DisplayData(res));
        }

        private void DisplayData(WebResponse res)
        {
            using (var stream = res.GetResponseStream())
            {
                var serializer = new DataContractJsonSerializer(typeof(Repository[]));
                this.Items = new ObservableCollection<Repository>((Repository[])serializer.ReadObject(stream));
            }

            this.IsProgress = false;
        }
    }
}
