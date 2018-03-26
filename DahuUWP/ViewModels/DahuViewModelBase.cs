using DahuUWP.DahuTech.ViewNotification;
using DahuUWP.Services;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace DahuUWP.ViewModels
{
    public abstract class DahuViewModelBase : ViewModelBase
    {
        public object NavigationParam { get; set; }
        public IDataService dataService;

        private DahuNotification _notification;
        public DahuNotification Notification
        {
            get { return _notification; }

            set
            {
                NotifyPropertyChanged(ref _notification, value);
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
        public void DisplayToastNotification(Dictionary<string, DahuNotification> notifications)
        {
        }
    }

}
