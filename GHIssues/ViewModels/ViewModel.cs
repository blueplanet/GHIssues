using Microsoft.Practices.Prism.ViewModel;

namespace GHIssues.ViewModels
{
    public class ViewModel : NotificationObject
    {
        public ViewModel()
        {

            this.LoadData();
        }

        protected virtual void LoadData() { }

        private bool isProgress;
        public bool IsProgress
        {
            get { return this.isProgress; }
            set
            {
                if (this.isProgress == value)
                {
                    return;
                }

                this.isProgress = value;
                this.RaisePropertyChanged(() => this.IsProgress);
            }
        }
    }
}
