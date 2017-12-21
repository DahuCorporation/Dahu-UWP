using DahuUWP.DahuTech;
using DahuUWP.DahuTech.ViewNotification;
using DahuUWP.Models;
using DahuUWP.Models.ModelManager;
using DahuUWP.Services;
using DahuUWP.Utils;
using DahuUWP.Utils.StoringData;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml;
using System.Xml.Linq;
using Windows.ApplicationModel;
using Windows.UI.Xaml;

namespace DahuUWP.ViewModels
{
    public class ConnectionViewModel : DahuViewModelBase
    {
        private string resourceName = "DahuUWP";
        private readonly IDataService dataService;

        private List<Project> _listeClients;
        public List<Project> ListeClients
        {
            get { return _listeClients; }
            set { NotifyPropertyChanged(ref _listeClients, value); }
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



        private bool NotifyPropertyChanged<T>(ref T variable, T valeur, [CallerMemberName] string nomPropriete = null)
        {
            if (object.Equals(variable, valeur)) return false;

            variable = valeur;
            RaisePropertyChanged(nomPropriete);
            return true;
        }

        private bool NotifyPropertyChanged2(ref string variable, string valeur, string nomPropriete)
        {
            if (object.Equals(variable, valeur)) return false;

            variable = valeur;
            RaisePropertyChanged(nomPropriete);
            return true;
        }

        public ICommand QuiSuisJeCommand { get; set; }

        public ICommand ConnectionCommand { get; set; }







        //http://www.java2s.com/Tutorials/CSharp/System.Reflection/FieldInfo/C_FieldInfo_GetValue.htm
        public ConnectionViewModel(IDataService service)
        {



            //Type thisType = this.GetType();
            //MethodInfo theMethod = thisType.GetMethod("Error", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            //object titi = "efeo";
            //object[] tata = new object[1];

            //tata[0] = titi;

            //theMethod.Invoke(this, tata);



            //fonctionnel!!!!!!!!!!!!!!!
            //FieldInfo field_info = this.GetType().GetField("_error",
            //BindingFlags.Instance |
            //BindingFlags.NonPublic |
            //BindingFlags.Public);
            //object obj = "Je test le gros test";
            //field_info.SetValue(this, obj);
            Dictionary<string, Notification> notifications = new Dictionary<string, Notification>();
            Notification blu = new Notification
            {
                Value = "coucou"
            };
            notifications.Add("key1", blu);
            DisplayToastNotification(notifications);
            //   FieldInfo field_info = this.GetType().GetField("_error",
            //BindingFlags.Instance |
            //BindingFlags.NonPublic |
            //BindingFlags.Public);
            //   object obj = "Je test le gros test";
            //   object test = this;
            //   field_info.SetValue(test, obj);
            //   string toto = _error;







            dataService = service;
            IModelManager projectManager = (IModelManager)dataService.GetProjectManager();
            _listeClients = projectManager.Charge(null).Select(s => (Project)s).ToList();
            QuiSuisJeCommand = new RelayCommand<Project>(QuiSuisJe);
            ConnectionCommand = new RelayCommand(Connection);
            UserAccount = new Account();
            RecoveringLastUser();
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
        private void RecoveringLastUser()
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
            FieldInfo field_info = this.GetType().GetField("_error",
            BindingFlags.Instance |
            BindingFlags.NonPublic |
            BindingFlags.Public);
            object obj = "Je test le gros test !!!!!!!!";
            field_info.SetValue(this, obj);
            if (StringUtils.EmailIsValid(UserAccount.Mail)
                && !String.IsNullOrEmpty(UserAccount.Password))
            {
                AccountDataService accounDataService = new AccountDataService();

                AppStaticInfo.Account = UserAccount;
                if (accounDataService.Connect())
                {
                    // TODO garder la connexion après fermeture est activé par default il faudra le changer
                    var vault = new Windows.Security.Credentials.PasswordVault();
                    vault.Add(new Windows.Security.Credentials.PasswordCredential(
                        resourceName, UserAccount.Mail, UserAccount.Password));
                    // Reset to empty for the security
                    UserAccount.Password = "";
                }
                else
                {
                    try
                    {
                        List<string> apiToUserMsgList = AppGeneral.ApiToUserMsg;

                        foreach (string msg in apiToUserMsgList)
                        {
                            //UserInterfaceStatus uis = AppGeneral.UserInterfaceStatusDico[msg];
                            //FieldInfo field_info = this.GetType().GetField(uis.VarName,
                            //    BindingFlags.Instance |
                            //    BindingFlags.NonPublic |
                            //    BindingFlags.Public);
                            //object obj = uis.Value;
                            //field_info.SetValue(this, obj);
                            //string titi = Error;
                        }
                    } catch (Exception ex)
                    {
                        // erreur si jamais il n'est pas présent dans le dico
                        System.Diagnostics.Debug.Fail("[Key could be not present in dico]" + ex.ToString());
                    }
                    

                    //ici lier les bons messages d'erreur avec le composant de la vue
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
            }
            catch (Exception ex)
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
