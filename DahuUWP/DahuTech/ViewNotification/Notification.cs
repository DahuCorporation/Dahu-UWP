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


        /// <summary>
        /// Display on homepageview => normaly every page is herrited from it
        /// </summary>
        public void Display()
        {
            ViewModelLocator.HomePageViewModel.Notification = this;
        }

        /// <summary>
        /// Display on homepageview => normaly every page is herrited from it
        /// With params in msg
        /// </summary>
        public void Display(params object[] args)
        {
            string saveValue = Value;
            Value = String.Format(Value, args);
            ViewModelLocator.HomePageViewModel.Notification = this;
            Value = saveValue;
        }

        /// <summary>
        /// Display on the current view and not on homepageview, the component notif must be in the current view to work
        /// </summary>
        public void DisplayCurrentView()
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
