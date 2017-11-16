using DahuUWP.Models;
using DahuUWP.Models.ModelManager;
using DahuUWP.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public ICommand QuiSuisJeCommand { get; set; }

        public ConnectionViewModel(IDataService service)
        {
            dataService = service;
            IModelManager projectManager = (IModelManager)dataService.GetProjectManager();
            listeClients = projectManager.Charge().Select(s => (Project)s).ToList();

            QuiSuisJeCommand = new RelayCommand<Project>(QuiSuisJe);
            //serviceClient = service;
            //Project client = serviceClient.Charger();
            //Prenom = client.Prenom;
            //Age = client.Age;
        }


        private void QuiSuisJe(Project client)
        {
            string titi = "Je suis ";
            string tata = "Je suis " + client.Prenom;
        }
    }
}
