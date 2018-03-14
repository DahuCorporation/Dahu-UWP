using DahuUWP.DahuTech.Inputs;
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
        public ICommand OnPageLoadedCommand { get; private set; }

        public PrivateProfilMainInformationViewModel(IDataService service)
        {
            dataService = service;
            OnPageLoadedCommand = new RelayCommand(OnPageLoaded);
            FillDataView();
        }

        private async void OnPageLoaded()
        {
            UpdateProfilMainInformation = new DahuButtonBindings
            {
                IsBusy = false,
                TappedFuncListener = UpdateMainInformation
            };
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

        private DahuButtonBindings _updateProfilMainInformation;
        public DahuButtonBindings UpdateProfilMainInformation
        {
            get { return _updateProfilMainInformation; }
            set
            {
                NotifyPropertyChanged(ref _updateProfilMainInformation, value);
            }
        }

        private async void FillDataView()
        {
            UserManager userManager = (UserManager)dataService.GetUserManager();

            Dictionary<string, object> userDicoCharge = new Dictionary<string, object>
            {
                { "_token", AppStaticInfo.Account.Token }
            };
            User user = await userManager.Charge(AppStaticInfo.Account.Uuid, userDicoCharge);
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

        private async void UpdateMainInformation()
        {
            UpdateProfilMainInformation.IsBusy = true;
            UserManager userManager = (UserManager)dataService.GetUserManager();
            await userManager.Edit(RecupUserMainInformation());
            UpdateProfilMainInformation.IsBusy = false;
        }
    }
}
