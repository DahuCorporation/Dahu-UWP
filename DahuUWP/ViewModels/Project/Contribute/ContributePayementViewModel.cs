using DahuUWP.DahuTech.Inputs;
using DahuUWP.DahuTech.Selector;
using DahuUWP.Models;
using DahuUWP.Services;
using DahuUWP.Services.ModelManager;
using DahuUWP.Views;
using DahuUWP.Views.Project;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DahuUWP.ViewModels.Project.Contribute
{
    public class ContributePayementViewModel : DahuViewModelBase
    {
        public ICommand OnPageLoadedCommand { get; private set; }

        public ContributePayementViewModel(IDataService service)
        {
            dataService = service;
            OnPageLoadedCommand = new RelayCommand(OnPageLoaded);
            //ChargeCounterparts();
        }

        private async void OnPageLoaded()
        {
            CardCharge = new CardCharge();
            if (NavigationParam is Models.Contribute)
                Contribute = (Models.Contribute)NavigationParam;
            ContributeMoneyButtonBindings = new DahuButtonBindings
            {
                FuncListener = ContributeMoney
            };
            BackToProjectButtonBindings = new DahuButtonBindings
            {
                RedirectedLink = typeof(ProjectView),
                Parameter = Contribute.Project
            };
        }

        private async void ContributeMoney(object param)
        {
            CounterpartsManager counterpartsManager = new CounterpartsManager();
            if (Contribute.FlatSelectorElem != null)
                CardCharge.Amount = Contribute.FlatSelectorElem.Price;
            else
                CardCharge.Amount = Contribute.Amount;
            if (await counterpartsManager.CreateCharge(CardCharge, Contribute.Project.Uuid))
                HomePage.DahuFrame.Navigate(typeof(ProjectView), Contribute.Project);
        }

        private DahuButtonBindings _backToProjectButtonBindings;
        public DahuButtonBindings BackToProjectButtonBindings
        {
            get { return _backToProjectButtonBindings; }
            set
            {
                NotifyPropertyChanged(ref _backToProjectButtonBindings, value);
            }
        }

        private DahuButtonBindings _contributeMoneyButtonBindings;
        public DahuButtonBindings ContributeMoneyButtonBindings
        {
            get { return _contributeMoneyButtonBindings; }
            set
            {
                NotifyPropertyChanged(ref _contributeMoneyButtonBindings, value);
            }
        }

        private Models.Contribute _contribute;
        public Models.Contribute Contribute
        {
            get { return _contribute; }
            set
            {
                NotifyPropertyChanged(ref _contribute, value);
            }
        }

        private CardCharge _cardCharge;
        public CardCharge CardCharge
        {
            get { return _cardCharge; }
            set
            {
                NotifyPropertyChanged(ref _cardCharge, value);
            }
        }
    }
}
