using DahuUWP.DahuTech.Inputs;
using DahuUWP.Models.ModelManager;
using DahuUWP.Services;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.ApplicationModel.Resources;
using Windows.UI.Popups;

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
            ProjectManager projectManager = (ProjectManager)dataService.GetProjectManager();

            Project = await projectManager.ChargeOneProject(((DahuUWP.Models.Project)NavigationParam).Uuid);
            InitManageProjectButtonBindings();
            InitJoinProjectButtonBindings();
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

        private void InitJoinProjectButtonBindings()
        {
            JoinProjectButtonBindings = new DahuButtonBindings()
            {
                IsBusy = false,
                Name = "Rejoindre le projet",
                FuncListener = JoinProject
            };
        }
        public async void JoinProject(object param)
        {
            ProjectManager projectManager = new ProjectManager();
            await projectManager.JoinProject(Project.Uuid);
            //var res = new ResourceLoader();
            //var messageDialog = new MessageDialog("No internet connection has been found.");
            //await messageDialog.ShowAsync();

        }

        private DahuButtonBindings _joinProjectButtonBindings;
        public DahuButtonBindings JoinProjectButtonBindings
        {
            get { return _joinProjectButtonBindings; }
            set
            {
                NotifyPropertyChanged(ref _joinProjectButtonBindings, value);
            }
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
