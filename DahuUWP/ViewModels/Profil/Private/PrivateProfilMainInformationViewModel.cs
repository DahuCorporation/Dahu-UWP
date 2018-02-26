using DahuUWP.Models;
using DahuUWP.Models.ModelManager;
using DahuUWP.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DahuUWP.ViewModels.Profil.Private
{
    public class PrivateProfilMainInformationViewModel : DahuViewModelBase
    {

        public ICommand SaveMainInformationCommand { get; set; }

        public PrivateProfilMainInformationViewModel(IDataService service)
        {
            dataService = service;
            SaveMainInformationCommand = new RelayCommand(SaveMainInformation);
            FillDataView();
        }

        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set
            {
                NotifyPropertyChanged(ref _userName, value);
            }
        }

        private string _userMailAdress;
        public string UserMailAdress
        {
            get { return _userMailAdress; }
            set
            {
                NotifyPropertyChanged(ref _userMailAdress, value);
            }
        }

        private string _userFirstName;
        public string UserFirstName
        {
            get { return _userFirstName; }
            set
            {
                NotifyPropertyChanged(ref _userFirstName, value);
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

        private string _userAddress;
        public string UserAddress
        {
            get { return _userAddress; }
            set
            {
                NotifyPropertyChanged(ref _userAddress, value);
            }
        }

        private bool NotifyPropertyChanged<T>(ref T variable, T valeur, [CallerMemberName] string nomPropriete = null)
        {
            if (object.Equals(variable, valeur)) return false;

            variable = valeur;
            RaisePropertyChanged(nomPropriete);
            return true;
        }

        private void FillDataView()
        {
            UserManager userManager = (UserManager)dataService.GetUserManager();

            Dictionary<string, object> userDicoCharge = new Dictionary<string, object>
            {
                { "_token", AppStaticInfo.Account.Token }
            };
            User user = userManager.Charge(AppStaticInfo.Account.Uuid, userDicoCharge);
            UserFirstName = user.FirstName;
            UserName = user.LastName;
            UserMailAdress = user.Mail;
            UserBiography = user.Biography;
            UserAddress = user.Address;
        }
        
        private User RecupUserMainInformation()
        {
            User user = new User
            {
                FirstName = UserFirstName,
                LastName = UserName,
                Mail = UserMailAdress,
                Biography = UserBiography,
                Address = UserAddress,
                Birthdate = "test",
                City = "test",
                Country = "test",
                Gender = Utils.Enum.Gender.Sir,
                Phone = "0633936156",
                PostalCode = "03320"
            };
            return user;
        }

        private void SaveMainInformation()
        {
            UserManager userManager = (UserManager)dataService.GetUserManager();
            userManager.Edit(RecupUserMainInformation());
        }
    }
}
