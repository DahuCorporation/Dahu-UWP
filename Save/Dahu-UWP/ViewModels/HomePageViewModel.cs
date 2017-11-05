using Dahu_UWP.Models;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Dahu_UWP.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
        private readonly IServiceClient serviceClient;

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

        public HomePageViewModel(IServiceClient service)
        {
            serviceClient = service;

            Project client = serviceClient.Charger();
            Prenom = "zefzfe";
            Age = client.Age;
        }
    }
}
