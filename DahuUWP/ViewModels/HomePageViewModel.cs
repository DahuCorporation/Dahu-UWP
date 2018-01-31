using DahuUWP.Models;
using DahuUWP.Models.ModelManager;
using DahuUWP.Services;
using DahuUWP.Views.Component;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace DahuUWP.ViewModels
{
    public class HomePageViewModel : DahuViewModelBase
    {
        private readonly IDataService serviceClient;

        private Visibility _modulNonConnected;
        public Visibility ModulNonConnected
        {
            get { return _modulNonConnected; }
            set
            {
                NotifyPropertyChanged(ref _modulNonConnected, value);
            }
        }

        private Visibility _modulConnected;
        public Visibility ModulConnected
        {
            get { return _modulConnected; }
            set
            {
                NotifyPropertyChanged(ref _modulConnected, value);
            }
        }
        //private Visibility _topBarConnected;
        //public Visibility TopBarConnected
        //{
        //    get { return _topBarConnected; }
        //    set {
        //        NotifyPropertyChanged(ref _topBarConnected, value);
        //    }
        //}

        private bool NotifyPropertyChanged<T>(ref T variable, T valeur, [CallerMemberName] string nomPropriete = null)
        {
            if (object.Equals(variable, valeur)) return false;

            variable = valeur;
            RaisePropertyChanged(nomPropriete);
            return true;
        }

        public void Connected(bool connected)
        {
            if (connected)
            {
                ModulNonConnected = Visibility.Collapsed;
                ModulConnected = Visibility.Visible;
            }
            else
            {
                ModulNonConnected = Visibility.Visible;
                ModulConnected = Visibility.Collapsed;
            }
        }

        public HomePageViewModel(IDataService service)
        {
            ViewModelLocator.HomePageViewModel = this;
            Connected(false);
        }
    }
}
