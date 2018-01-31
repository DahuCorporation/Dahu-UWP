using DahuUWP.Models;
using DahuUWP.Models.ModelManager;
using DahuUWP.Services;
using DahuUWP.Views;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DahuUWP.ViewModels
{
    public class RegisterViewModel : DahuViewModelBase
    {
        public ICommand RegisterUserCommand { get; set; }

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

        public RegisterViewModel(IDataService service)
        {
            dataService = service;
            RegisterUserCommand = new RelayCommand(RegisterUser);
        }

        private object RecupUserRegistrationInformation()
        {
            //var user = new
            //{
            //    first_name = UserFirstName,
            //    last_name = UserName,
            //    mail = UserMailAdress,
            //    biography = "test",
            //    address = "zef",
            //    birthdate = UserBirthdate,
            //    city = "test",
            //    country = "test",
            //    gender = Utils.Enum.Gender.Sir,
            //    phone = "0633936156",
            //    postal_code = "03320",
            //    password = UserPassword,
            //    type = "user"
            //};
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

        private void RegisterUser()
        {
            UserManager userManager = (UserManager)dataService.GetUserManager();

            if (userManager.Create(RecupUserRegistrationInformation()))
            {
                HomePage.DahuFrame.Navigate(typeof(Connection));
            }
        }
    }
}
