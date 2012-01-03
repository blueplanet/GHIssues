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

        private bool isSynchronizing;
        public bool IsSynchronizing
        {
            get { return this.isSynchronizing; }
            set
            {
                if (this.isSynchronizing == value)
                {
                    return;
                }

                this.isSynchronizing = value;
                this.RaisePropertyChanged(() => this.isSynchronizing);
            }
        }
    }
}
