using DahuUWP.DahuTech;
using DahuUWP.Models;
using DahuUWP.Models.ModelManager;
using DahuUWP.Services;
using DahuUWP.Utils;
using DahuUWP.Views;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;

namespace DahuUWP.ViewModels
{
    public class RegisterViewModel : DahuViewModelBase
    {
        public ICommand RegisterUserCommand { get; set; }
        public ICommand OnPageLoadedCommand { get; private set; }

        public RegisterViewModel(IDataService service)
        {
            dataService = service;
            RegisterUserCommand = new RelayCommand(RegisterUser);
            OnPageLoadedCommand = new RelayCommand(OnPageLoaded);
        }

        private async void OnPageLoaded()
        {
            ((HomePageViewModel)ViewModelLocator.HomePageViewModel).DahuSpecMenuOptions.DahuSpecMenuVisibility = Visibility.Collapsed;
        }

        private User RecupUserRegistrationInformation()
        {
            User user = new User
            {
                FirstName = UserFirstName,
                LastName = UserName,
                Mail = UserMailAdress,
                Biography = "test",
                Address = "test",
                Birthdate = UserBirthdate,
                City = "test",
                Country = "test",
                Gender = Utils.Enum.Gender.Sir,
                Phone = "0633936156",
                PostalCode = "03320",
                Account = new Account
                {
                    Type = "user",
                    Password = UserPassword
                }
            };
            return user;
        }

        /// <summary>
        /// Fields verification
        /// </summary>
        /// <returns></returns>
        private bool FieldsVerif(User user)
        {
            if (String.IsNullOrEmpty(user.Address) || String.IsNullOrEmpty(user.Biography) ||
                String.IsNullOrEmpty(user.Birthdate) || String.IsNullOrEmpty(user.City) ||
                String.IsNullOrEmpty(user.Country) || String.IsNullOrEmpty(user.FirstName)
                || String.IsNullOrEmpty(user.LastName) || String.IsNullOrEmpty(user.Phone) || String.IsNullOrEmpty(user.PostalCode))
            {
                AppGeneral.UserInterfaceStatusDico["Information missing."].Display();
                return false;
            }
            if (String.IsNullOrEmpty(user.Mail))
            {
                AppGeneral.UserInterfaceStatusDico["Empty mail."].Display();
                return false;
            }
            else if (!StringUtils.EmailIsValid(user.Mail))
            {
                AppGeneral.UserInterfaceStatusDico["Invalid mail."].Display();
                return false;
            }
            if (String.IsNullOrEmpty(user.Account.Password))
            {
                AppGeneral.UserInterfaceStatusDico["Empty password."].Display();
                return false;
            }
            
            return true;
        }

        private async void RegisterUser()
        {
            UserManager userManager = (UserManager)dataService.GetUserManager();
            User user = RecupUserRegistrationInformation();
            if (!FieldsVerif(user))
                return;
            if (await userManager.Create((user)))
            {
                HomePage.DahuFrame.Navigate(typeof(Connection));
            }
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

        private string _userFirstName;
        public string UserFirstName
        {
            get { return _userFirstName; }
            set
            {
                NotifyPropertyChanged(ref _userFirstName, value);
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

        private string _userBirthdate;
        public string UserBirthdate
        {
            get { return _userBirthdate; }
            set
            {
                NotifyPropertyChanged(ref _userBirthdate, value);
            }
        }

        private string _userPassword;
        public string UserPassword
        {
            get { return _userPassword; }
            set
            {
                NotifyPropertyChanged(ref _userPassword, value);
            }
        }

        private string _userRepeatedPassword;
        public string UserRepeatedPassword
        {
            get { return _userRepeatedPassword; }
            set
            {
                NotifyPropertyChanged(ref _userRepeatedPassword, value);
            }
        }

        private bool NotifyPropertyChanged<T>(ref T variable, T valeur, [CallerMemberName] string nomPropriete = null)
        {
            if (object.Equals(variable, valeur)) return false;

            variable = valeur;
            RaisePropertyChanged(nomPropriete);
            return true;
        }
    }
}
