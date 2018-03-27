using DahuUWP.Services;
using DahuUWP.Views.Project.Managing;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DahuUWP.ViewModels.Project.Managing
{
    public class ManageProjectViewModel : DahuViewModelBase
    {
        public ICommand OnPageLoadedCommand { get; private set; }

        public ManageProjectViewModel(IDataService service)
        {
            dataService = service;
            OnPageLoadedCommand = new RelayCommand(OnPageLoaded);
        }


        private async void OnPageLoaded()
        {
            ((HomePageViewModel)ViewModelLocator.HomePageViewModel).SwitchOrActiveCurrentTopBarNodeMenu(typeof(ManageProject));
        }
    }
}
