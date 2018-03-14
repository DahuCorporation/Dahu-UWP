using DahuUWP.Services;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DahuUWP.ViewModels.Search
{
    public class MainResearchViewModel : DahuViewModelBase
    {
        public ICommand OnPageLoadedCommand { get; private set; }

        public MainResearchViewModel(IDataService service)
        {
            dataService = service;

            OnPageLoadedCommand = new RelayCommand(OnPageLoaded);
            DahuUWP.Models.Project p = new DahuUWP.Models.Project()
            {
                Name = "Prject1",
                Description = "Description1"
            };
            List<DahuUWP.Models.Project> plist = new List<DahuUWP.Models.Project>();
            plist.Add(p);
            plist.Add(p);
            plist.Add(p);
            plist.Add(p);
            ProjectResults = new ObservableCollection<DahuUWP.Models.Project>(plist);
        }

        private async void OnPageLoaded()
        {
        }

        private ObservableCollection<DahuUWP.Models.Project> _projectResults;
        public ObservableCollection<DahuUWP.Models.Project> ProjectResults
        {
            get { return _projectResults; }
            set
            {
                NotifyPropertyChanged(ref _projectResults, value);
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
