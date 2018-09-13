using DahuUWP.DahuTech;
using DahuUWP.Models;
using DahuUWP.Models.ModelManager;
using DahuUWP.Services;
using DahuUWP.Utils;
using DahuUWP.Views;
using DahuUWP.Views.Components.Popups;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace DahuUWP.ViewModels
{
    public class ConnectionViewModel : DahuViewModelBase
    {
        private string resourceName = "DahuUWP";

        public ICommand ConnectionCommand { get; set; }

        public ICommand OnPageLoadedCommand { get; private set; }

        //http://www.java2s.com/Tutorials/CSharp/System.Reflection/FieldInfo/C_FieldInfo_GetValue.htm
        public ConnectionViewModel(IDataService service)
        {
            IsBusy = true;
            dataService = service;
            IModelManager projectManager = (IModelManager)dataService.GetProjectManager();
            ConnectionCommand = new RelayCommand(Connection);
            OnPageLoadedCommand = new RelayCommand(OnPageLoaded);
            UserAccount = new Account();
            DialogRecoverUser = new Dialog
            {
                OnAction = ActionDialogRecoverUser,
                Visibility = Windows.UI.Xaml.Visibility.Visible
            };
        }

        private async void OnPageLoaded()
        {
            ((HomePageViewModel)ViewModelLocator.HomePageViewModel).DahuSpecMenuOptions.DahuSpecMenuVisibility = Visibility.Collapsed;
            //RecoveringLastUser();
            IsBusy = false;
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                NotifyPropertyChanged(ref _isBusy, value);
            }
        }

        public Account UserAccount { get; set; }

        private string _mail;
        public string Mail
        {
            get { return _mail; }
            set {
                NotifyPropertyChanged(ref _mail, value);
            }
        }

        private Dialog _dialogRecoverUser;
        public Dialog DialogRecoverUser
        {
            get { return _dialogRecoverUser; }
            set
            {
                NotifyPropertyChanged(ref _dialogRecoverUser, value);
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
        /// Is the result of the shown dialog that ask user if he wants to be connected with last user
        /// res => true means yes, res => false means no
        /// </summary>
        /// <param name="res"></param>
        public void ActionDialogRecoverUser(bool res)
        {
            DialogRecoverUser.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            if (res)
                RecoveringLastUser();
        }

        /// <summary>
        /// Connect the user how wanted to keep connection after closing app
        /// </summary>
        private async void RecoveringLastUser()
        {
            var loginCredential = GetCredentialFromLocker();

            if (loginCredential != null)
            {
                AccountDataService accounDataService = new AccountDataService();

                loginCredential.RetrievePassword();
                object connectionInfo = new
                {
                    mail = loginCredential.UserName,
                    password = loginCredential.Password
                };
                UserAccount.Mail = loginCredential.UserName;
                AppStaticInfo.Account = UserAccount;
                if (await accounDataService.Connect(connectionInfo))
                    ConnectionSuccessful();
            }
            //Stay on connection page
        }

        /// <summary>
        /// Connection fields verification
        /// </summary>
        /// <returns></returns>
        private bool ConnectionFieldsVerif()
        {
            if (String.IsNullOrEmpty(UserAccount.Mail) && String.IsNullOrEmpty(UserAccount.Password))
            {
                AppGeneral.UserInterfaceStatusDico["Empty mail and password."].Display();
                return false;
            }
            if (String.IsNullOrEmpty(UserAccount.Mail))
            {
                AppGeneral.UserInterfaceStatusDico["Empty mail."].Display();
                return false;
            }
            if (String.IsNullOrEmpty(UserAccount.Password))
            {
                AppGeneral.UserInterfaceStatusDico["Empty password."].Display();
                return false;
            }
            if (!StringUtils.EmailIsValid(UserAccount.Mail)) {
                AppGeneral.UserInterfaceStatusDico["Invalid mail."].Display();
                return false;
            }
            return true;
        }

        private async void Connection()
        {
            if (!ConnectionFieldsVerif())
                return;
            IsBusy = true;
            AccountDataService accounDataService = new AccountDataService();

            AppStaticInfo.Account = UserAccount;
            if (await accounDataService.Connect())
            {
                // TODO garder la connexion après fermeture est activé par default il faudra le changer
                // TODO mettre le fait de resté connecté dans accountDataService et pareil pour vérifier si l'user était déjà connecté
                var vault = new Windows.Security.Credentials.PasswordVault();
                vault.Add(new Windows.Security.Credentials.PasswordCredential(
                    resourceName, UserAccount.Mail, UserAccount.Password));
                ConnectionSuccessful();
            }
            else
            {

            }
        }

        private void ConnectionSuccessful()
        {
            // Reset to empty for the security
            UserAccount.Password = "";
            IsBusy = false;
            HomePage.DahuFrame.Navigate(typeof(Discover));
            // Permet de changer la top bar en tant que connecté
            ((HomePageViewModel)ViewModelLocator.HomePageViewModel).Connected(true);
        }

        /// <summary>
        /// Get password of user how wanted to save his connection after closing the app
        /// </summary>
        /// <returns></returns>
        private Windows.Security.Credentials.PasswordCredential GetCredentialFromLocker()
        {
            try
            {
                Windows.Security.Credentials.PasswordCredential credential = null;

                var vault = new Windows.Security.Credentials.PasswordVault();
                var credentialList = vault.FindAllByResource(resourceName);
                if (credentialList.Count > 0)
                {
                    // There can be more than one user, for now only one is possible [0] > the first one
                    // https://docs.microsoft.com/en-us/windows/uwp/security/credential-locker > pour mettre en place le multit login
                    credential = credentialList[0];
                }

                return credential;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Fail(ex.ToString());
                return null;
            }

        }
    }
}
