using DahuUWP.DahuTech.ViewNotification;
using DahuUWP.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;

namespace DahuUWP.ViewModels
{
    public abstract class DahuViewModelBase : ViewModelBase
    {
        public ICommand OnPageLoadedBaseCommand { get; private set; }
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

        protected bool NotifyPropertyChanged<T>(ref T variable, T valeur, [CallerMemberName] string nomPropriete = null)
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

        public DahuViewModelBase()
        {
            OnPageLoadedBaseCommand = new RelayCommand(OnPageLoaded);
        }

        private async void OnPageLoaded()
        {
            try
            {
                // reinitialise le menu a chaque page pour le pas avoir la ligne blanche sur une page qui n'est pas le menu
                ((HomePageViewModel)ViewModelLocator.HomePageViewModel).DahuSpecMenuOptions.SwitchOrActiveCurrentTopBarNodeMenu(null);
            }
            catch
            {

            }
        }
    }

}
