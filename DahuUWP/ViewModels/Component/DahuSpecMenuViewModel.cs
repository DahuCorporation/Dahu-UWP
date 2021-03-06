﻿using CommonServiceLocator;
using DahuUWP.DahuTech;
using DahuUWP.DahuTech.Inputs;
using DahuUWP.DahuTech.Menu;
using DahuUWP.Models;
using DahuUWP.Models.ModelManager;
using DahuUWP.Services;
using DahuUWP.Views;
using DahuUWP.Views.Search;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
                FuncListener = Research
            };
            OnPageLoadedCommand = new RelayCommand(OnPageLoaded);
        }


        private async void OnPageLoaded()
        {
            ((HomePageViewModel)ViewModelLocator.HomePageViewModel).DahuSpecMenuOptions.SwitchOrActiveCurrentTopBarNodeMenu(typeof(Discover));
        }

        public async void Research(object param)
        {
            ResearchButtonBindings.IsBusy = true;
            HomePage.DahuFrame.Navigate(typeof(MainResearch), ResearchValue);
            ResearchButtonBindings.IsBusy = false;
        }

        private ObservableCollection<TopBarNodeMenu> _nodesTopBarMenu;
        public ObservableCollection<TopBarNodeMenu> NodesTopBarMenu
        {
            get { return _nodesTopBarMenu; }
            set
            {
                NotifyPropertyChanged(ref _nodesTopBarMenu, value);
            }
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
