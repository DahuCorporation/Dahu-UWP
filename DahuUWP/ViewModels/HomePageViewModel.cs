using DahuUWP.Models;
using DahuUWP.Models.ModelManager;
using DahuUWP.Services;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DahuUWP.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
        private readonly IDataService serviceClient;

        private string prenom;
        public string Prenom
        {
            get { return prenom; }
            set { NotifyPropertyChanged(ref prenom, value); }
        }


        private int age;
        public int Age
        {
            get { return age; }
            set { NotifyPropertyChanged(ref age, value); }
        }

        private bool NotifyPropertyChanged<T>(ref T variable, T valeur, [CallerMemberName] string nomPropriete = null)
        {
            if (object.Equals(variable, valeur)) return false;

            variable = valeur;
            RaisePropertyChanged(nomPropriete);
            return true;
        }

        

        public HomePageViewModel(IDataService service)
        {
            IModelManager projectManager = (IModelManager)service.GetProjectManager();
            bool val = projectManager.Delete(10);
            if (val)
            {
                Prenom = "mode design frrrr";
            } else
            {
                Prenom = "mode pas design fr";
            }
            //serviceClient = service;
            //Project client = serviceClient.Charger();
            //Prenom = client.Prenom;
            //Age = client.Age;
        }
    }
}
