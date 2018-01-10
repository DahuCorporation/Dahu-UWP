using DahuUWP.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DahuUWP.DahuTech.ViewNotification
{
    public enum NotificationType
    {
            Positive,
            Negative,
            Info,
            Warning
    };

    public class DahuNotification
    {
        public string Value { get; set; }
        public string PropertyName { get; set; }
        public NotificationType Type { get; set; }

        public void Display()
        {
            ViewModelLocator.CurrentViewModel.Notification = this;
        }

        public void DisplayToast()
        {
            ConnectionViewModel dahuViewModelBase = (ConnectionViewModel)ViewModelLocator.CurrentViewModel;

            //research done:
            //https://stackoverflow.com/questions/42709379/how-to-display-user-control-within-the-main-window-in-wpf-using-mvvm
            //https://stackoverflow.com/questions/4983951/how-do-i-bind-a-listcustomobject-to-a-wpf-datagrid
        }
    }
}
