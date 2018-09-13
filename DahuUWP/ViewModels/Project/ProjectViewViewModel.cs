using DahuUWP.DahuTech.Inputs;
using DahuUWP.Services;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DahuUWP.ViewModels.Project
{
    public class ProjectViewViewModel : DahuViewModelBase
    {
        public ICommand OnPageLoadedCommand { get; private set; }

        public ProjectViewViewModel(IDataService service)
        {
            dataService = service;
            OnPageLoadedCommand = new RelayCommand(OnPageLoaded);
        }

        private async void OnPageLoaded()
        {
            Project = (DahuUWP.Models.Project)NavigationParam;
            InitManageProjectButtonBindings();
        }

        private void InitManageProjectButtonBindings()
        {
            ContributeWithMoneyLink = new DahuButtonBindings()
            {
                IsBusy = false,
                Name = "Gérer",
                RedirectedLink = typeof(Views.Project.Contribute.Contribute)
            };
        }

        private DahuButtonBindings _contributeWithMoneyLink;
        public DahuButtonBindings ContributeWithMoneyLink
        {
            get { return _contributeWithMoneyLink; }
            set
            {
                NotifyPropertyChanged(ref _contributeWithMoneyLink, value);
            }
        }

        private DahuUWP.Models.Project _project;
        public DahuUWP.Models.Project Project
        {
            get { return _project; }
            set
            {
                NotifyPropertyChanged(ref _project, value);
            }
        }
    }
}
