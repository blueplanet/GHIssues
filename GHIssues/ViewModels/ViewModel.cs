using System.Windows;
using Microsoft.Phone.Controls;
using Microsoft.Practices.Prism.ViewModel;

namespace GHIssues.ViewModels
{
    public abstract class ViewModel : NotificationObject
    {
        private PhoneApplicationFrame frame; 

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

        protected PhoneApplicationFrame Frame
        {
            get
            {
                if (frame == null)
                {
                    frame = ((App)Application.Current).RootFrame;
                }

                return frame;
            }
        }
    }
}
