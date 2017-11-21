using DahuUWP.Models;
using DahuUWP.Models.ModelManager;
using DahuUWP.Services;
using DahuUWP.Utils.Verif;
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
        private readonly IDataService dataService;

        private List<Project> listeClients;
        public List<Project> ListeClients
        {
            get { return listeClients; }
            set { NotifyPropertyChanged(ref listeClients, value); }
        }

        private bool NotifyPropertyChanged<T>(ref T variable, T valeur, [CallerMemberName] string nomPropriete = null)
        {
            if (object.Equals(variable, valeur)) return false;

            variable = valeur;
            RaisePropertyChanged(nomPropriete);
            return true;
        }


        

        //private Account userAccount;
        //public Account UserAccount
        //{
        //    get { return userAccount; }
        //    set { NotifyPropertyChanged(ref userAccount, value); }
        //}




        public ICommand QuiSuisJeCommand { get; set; }

        public ICommand ConnectionCommand { get; set; }

        public ConnectionViewModel(IDataService service)
        {
            dataService = service;
            IModelManager projectManager = (IModelManager)dataService.GetProjectManager();
            listeClients = projectManager.Charge().Select(s => (Project)s).ToList();
            QuiSuisJeCommand = new RelayCommand<Project>(QuiSuisJe);
            ConnectionCommand = new RelayCommand(Connection);
            UserAccount = new Account();
            //serviceClient = service;
            //Project client = serviceClient.Charger();
            //Prenom = client.Prenom;
            //Age = client.Age;
        }
        
        private Account userAccount;
        public Account UserAccount { get; set; }

        private string mail;
        public string Mail
        {
            get { return mail; }
            set { NotifyPropertyChanged(ref mail, value); }
        }

        private void Connection()
        {
            //https://docs.microsoft.com/en-us/aspnet/web-api/overview/advanced/calling-a-web-api-from-a-net-client
            //HttpClient client = new HttpClient();

            //client.BaseAddress = new Uri("http://fncs.eu/api/forward/auth");
            //client.DefaultRequestHeaders.Accept.Clear();
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //object obj = new
            //{
            //    mail = "thomasoi@hotmail.fr",
            //    password = "123456"
            //};

            //var myContent = "{\"data\":" + JsonConvert.SerializeObject(obj) + "}";
            //var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            //var byteContent = new ByteArrayContent(buffer);
            //byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            //HttpResponseMessage result = client.PostAsync("", byteContent).Result;
            //result.EnsureSuccessStatusCode();



            //string responseBody = result.Content.ReadAsStringAsync().Result;
            //HttpResponseMessage response =  client.PostAsync("auth", byteContent);
            // response.EnsureSuccessStatusCode();

            //APIService service = new APIService();

            //object connection = new
            //{
            //    Mail = "thomasoi@hotmail.fr",
            //    Password = "123456"
            //};
            //service.AddToRoute("auth");
            //service.JsonBodyContent(connection);
            //await service.PostAsync();




            //bool test = service.PostAsync().Result;




            if (FieldsVerif.EmailIsValid(UserAccount.Mail)
                && !String.IsNullOrEmpty(UserAccount.Password))
            {
                AccountDataService accounDataService = new AccountDataService();

                AppStaticInfo.Account = UserAccount;
                accounDataService.Connect();
            }
        }

        private void QuiSuisJe(Project client)
        {
            string titi = "Je suis " + UserAccount.Mail;
            string tata = "Je suis " + client.Prenom;
        }
    }
}
