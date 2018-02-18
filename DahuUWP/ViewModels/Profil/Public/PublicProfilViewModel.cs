using DahuUWP.Models;
using DahuUWP.Models.ModelManager;
using DahuUWP.Services;
using DahuUWP.Views;
using DahuUWP.Views.Profil.Private;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DahuUWP.ViewModels.Profil.Public
{
    public class PublicProfilViewModel : DahuViewModelBase
    {
        public ICommand ProfilSettingsLinkCommand { get; set; }

        public ICommand OnPageLoadedCommand { get; private set; }

        public PublicProfilViewModel(IDataService service)
        {
            dataService = service;
            ProfilSettingsLinkCommand = new RelayCommand(ProfilSettingsLink);
            OnPageLoadedCommand = new RelayCommand(OnPageLoaded);
        }

        private async void OnPageLoaded()
        {
            UserManager userManager = (UserManager)dataService.GetUserManager();
            Dictionary<string, object> userDicoCharge = new Dictionary<string, object>
            {
                { "_token", AppStaticInfo.Account.Token }
            };
            User user = userManager.Charge(userDicoCharge);
            UserFullName = user.FirstName + " " + user.LastName;
            UserBiography = user.Biography;
        }

        private string _userFullName;
        public string UserFullName
        {
            get { return _userFullName; }
            set
            {
                NotifyPropertyChanged(ref _userFullName, value);
            }
        }

        private string _userBiography;
        public string UserBiography
        {
            get { return _userBiography; }
            set
            {
                NotifyPropertyChanged(ref _userBiography, value);
            }
        }

        private bool NotifyPropertyChanged<T>(ref T variable, T valeur, [CallerMemberName] string nomPropriete = null)
        {
            if (object.Equals(variable, valeur)) return false;

            variable = valeur;
            RaisePropertyChanged(nomPropriete);
            return true;
        }

        private void ProfilSettingsLink()
        {
            HomePage.DahuFrame.Navigate(typeof(PrivateProfil));
        }
    }
}
