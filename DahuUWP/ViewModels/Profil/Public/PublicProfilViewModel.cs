using DahuUWP.Models;
using DahuUWP.Models.ModelManager;
using DahuUWP.Services;
using DahuUWP.Views;
using DahuUWP.Views.Profil.Private;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DahuUWP.ViewModels.Profil.Public
{
    public class PublicProfilViewModel : DahuViewModelBase
    {
        public ICommand ProfilSettingsLinkCommand { get; set; }

        public PublicProfilViewModel(IDataService service)
        {
            dataService = service;
            ProfilSettingsLinkCommand = new RelayCommand(ProfilSettingsLink);
            UserManager userManager = (UserManager)dataService.GetUserManager();

            Dictionary<string, object> userDicoCharge = new Dictionary<string, object>
            {
                { "_token", AppStaticInfo.Account.Token }
            };
            User user = userManager.Charge(userDicoCharge);
            UserFullName = user.FirstName + " " + user.LastName;
            UserBiography = user.Biography;
        }

        public string UserFullName { get; set; }

        public string UserBiography { get; set; }

        private void ProfilSettingsLink()
        {
            HomePage.DahuFrame.Navigate(typeof(PrivateProfil));
        }
    }
}
