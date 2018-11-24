using DahuUWP.DahuTech.Selector;
using DahuUWP.Services;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DahuUWP.ViewModels.Project.Contribute
{
    public class ContributeSuccessPaymentViewModel : DahuViewModelBase
    {
        public ICommand OnPageLoadedCommand { get; private set; }

        public ContributeSuccessPaymentViewModel(IDataService service)
        {
            dataService = service;
            OnPageLoadedCommand = new RelayCommand(OnPageLoaded);
        }

        private async void OnPageLoaded()
        {
            if (NavigationParam is Models.Contribute)
                Contribute = (Models.Contribute)NavigationParam;
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
    }
}
