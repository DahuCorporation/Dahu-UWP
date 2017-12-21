using DahuUWP.DahuTech.ViewNotification;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DahuUWP.ViewModels
{
    public abstract class DahuViewModelBase : ViewModelBase
    {


        private string _mail;
        public string MailBase
        {
            get { return _mail; }
            set
            {
                NotifyPropertyChanged(ref _mail, value);
            }
        }

        public Dictionary<string, Notification> _notifications;
        public Dictionary<string, Notification> Notifications
        {
            get { return _notifications; }

            set
            {
                NotifyPropertyChanged(ref _notifications, value);
            }
        }

        private bool NotifyPropertyChanged<T>(ref T variable, T valeur, [CallerMemberName] string nomPropriete = null)
        {
            if (object.Equals(variable, valeur)) return false;

            variable = valeur;
            RaisePropertyChanged(nomPropriete);
            return true;
        }

        /// <summary>
        /// Display a toast notification on the active view
        /// </summary>
        public void DisplayToastNotification(Dictionary<string, Notification> notifications)
        {
            Notifications = notifications;
            MailBase = "gros caca";
        }
    }
}
