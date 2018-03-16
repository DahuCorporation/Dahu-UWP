using DahuUWP.DahuTech;
using DahuUWP.DahuTech.Inputs;
using DahuUWP.Models;
using DahuUWP.Models.ModelManager;
using DahuUWP.Services;
using DahuUWP.Views;
using DahuUWP.Views.Search;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Navigation;

namespace DahuUWP.ViewModels.Component
{


    public class DahuSpecMenuViewModel : DahuViewModelBase
    {
        public ICommand OnPageLoadedCommand { get; private set; }

        public DahuSpecMenuViewModel(IDataService service)
        {
            dataService = service;

            ResearchButtonBindings = new DahuButtonBindings
            {
                IsBusy = false,
                TappedFuncListener = Research
            };
            OnPageLoadedCommand = new RelayCommand(OnPageLoaded);
        }


        private async void OnPageLoaded()
        {
        }

        public async void Research()
        {
            ResearchButtonBindings.IsBusy = true;
            AppGeneral.NavigatePageParam = ResearchValue;
            AppGeneral.NavigateTo = typeof(MainResearch);
            HomePage.DahuFrame.Navigate(typeof(MainResearch));
            ResearchButtonBindings.IsBusy = false;
        }


        private DahuButtonBindings _researchButtonBindings;
        public DahuButtonBindings ResearchButtonBindings
        {
            get { return _researchButtonBindings; }
            set
            {
                NotifyPropertyChanged(ref _researchButtonBindings, value);
            }
        }

        private string _researchValue;
        public string ResearchValue
        {
            get { return _researchValue; }
            set
            {
                NotifyPropertyChanged(ref _researchValue, value);
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
