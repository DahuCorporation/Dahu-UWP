using DahuUWP.Models;
using DahuUWP.Models.ModelManager;
using DahuUWP.Services;
using DahuUWP.Utils;
using DahuUWP.Utils.StoringData;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DahuUWP.ViewModels
{
    public class ConnectionViewModel : ViewModelBase
    {
        private string resourceName = "DahuUWP";
        private readonly IDataService dataService;

        private List<Project> listeClients;
        public List<Project> ListeClients
        {
            get { return listeClients; }
            set { NotifyPropertyChanged(ref listeClients, value); }
        }

        public Account UserAccount { get; set; }

        private string mail;
        public string Mail
        {
            get { return mail; }
            set { NotifyPropertyChanged(ref mail, value); }
        }

        private bool NotifyPropertyChanged<T>(ref T variable, T valeur, [CallerMemberName] string nomPropriete = null)
        {
            if (object.Equals(variable, valeur)) return false;

            variable = valeur;
            RaisePropertyChanged(nomPropriete);
            return true;
        }



        public ICommand QuiSuisJeCommand { get; set; }

        public ICommand ConnectionCommand { get; set; }

        public ConnectionViewModel(IDataService service)
        {
            dataService = service;
            IModelManager projectManager = (IModelManager)dataService.GetProjectManager();
            listeClients = projectManager.Charge(null).Select(s => (Project)s).ToList();
            QuiSuisJeCommand = new RelayCommand<Project>(QuiSuisJe);
            ConnectionCommand = new RelayCommand(Connection);
            UserAccount = new Account();
            ConnectKeepConnectionUser();
            //TemporaryAppData tempAppData = new TemporaryAppData();
            //string userData = tempAppData.Read("UserData").Result;

            //// if user already connected with a email
            //if (!String.IsNullOrEmpty(userData))
            //{
            //    userData = "gros kk";
            //}
            //tempAppData.Write("UserData", "thomasoi@hotmail.fr");

        }

        /// <summary>
        /// Connect the user how wanted to keep connection after closing app
        /// </summary>
        private void ConnectKeepConnectionUser()
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
                accounDataService.Connect(connectionInfo);
                // TODO rediriger vers la page d'accueil
            }
            //Stay on connection page
        }

        private void Connection()
        {
            if (StringUtils.EmailIsValid(UserAccount.Mail)
                && !String.IsNullOrEmpty(UserAccount.Password))
            {
                AccountDataService accounDataService = new AccountDataService();

                AppStaticInfo.Account = UserAccount;
                if (accounDataService.Connect() == true)
                {
                    // TODO garder la connexion après fermeture est activé par default il faudra le changer
                    var vault = new Windows.Security.Credentials.PasswordVault();
                    vault.Add(new Windows.Security.Credentials.PasswordCredential(
                        resourceName, UserAccount.Mail, UserAccount.Password));
                    // Reset to empty for the security
                    UserAccount.Password = "";
                }

            }
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
            } catch (Exception ex)
            {
                System.Diagnostics.Debug.Fail(ex.ToString());
                return null;
            }
            
        }

        private void QuiSuisJe(Project client)
        {
            string titi = "Je suis " + UserAccount.Mail;
            string tata = "Je suis " + client.Prenom;
        }
    }
}
