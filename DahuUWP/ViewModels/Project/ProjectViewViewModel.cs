using DahuUWP.DahuTech.Inputs;
using DahuUWP.Models;
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
using Windows.UI.Xaml;

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
            InitView();
            InitManageProjectButtonBindings();
            InitJoinProjectButtonBindings();
            InitFollowProjectButtonBindings(); 
        }

        private async void InitView()
        {
            double amountGoal;
            double amountActual;
            

            
            if (String.IsNullOrWhiteSpace(Project.AmountGoal) || Project.AmountGoal == "0")
                ContributeBlockVisibility = Visibility.Collapsed;
            else
                ContributeBlockVisibility = Visibility.Visible;

            if (Double.TryParse(Project.AmountGoal, out amountGoal) && Double.TryParse(Project.AmountActual, out amountActual))
            {
                double perc = (double)100 / amountGoal;
                ProgressBarValue = perc * amountActual;
            }
            else
                ProgressBarValue = 0;
        }

            private void InitManageProjectButtonBindings()
        {
            ContributeWithMoneyLink = new DahuButtonBindings()
            {
                IsBusy = false,
                Name = "Gérer",
                RedirectedLink = typeof(Views.Project.Contribute.Contribute),
                Parameter = Project
            };
        }

        private void InitFollowProjectButtonBindings()
        {
            FollowProjectButtonBindings = new DahuButtonBindings()
            {
                IsBusy = false,
                FuncListener = FollowProject,
                Parameter = Project
            };
        }

        private void InitJoinProjectButtonBindings()
        {
            JoinProjectButtonBindings = new DahuButtonBindings()
            {
                IsBusy = false,
                Name = "Rejoindre le projet",
                FuncListener = JoinProject,
                Parameter = Project
            };
        }

        public async void FollowProject(object param)
        {
            ProjectManager projectManager = new ProjectManager();
            await projectManager.FollowUnFollowProject(Project.Uuid, AppStaticInfo.Account.Uuid);

        }

        public async void JoinProject(object param)
        {
            ProjectManager projectManager = new ProjectManager();
            await projectManager.JoinProject(Project.Uuid);
            //await projectManager.ChangeUserState(Project.Uuid, AppStaticInfo.Account.Uuid, "worker");
            //var res = new ResourceLoader();
            //var messageDialog = new MessageDialog("No internet connection has been found.");
            //await messageDialog.ShowAsync();

        }

        
        private Visibility _contributeBlockVisibility;
        public Visibility ContributeBlockVisibility
        {
            get { return _contributeBlockVisibility; }
            set
            {
                NotifyPropertyChanged(ref _contributeBlockVisibility, value);
            }
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

        private DahuButtonBindings _followProjectButtonBindings;
        public DahuButtonBindings FollowProjectButtonBindings
        {
            get { return _followProjectButtonBindings; }
            set
            {
                NotifyPropertyChanged(ref _followProjectButtonBindings, value);
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


        private double _progressBarValue;
        public double ProgressBarValue
        {
            get { return _progressBarValue; }
            set
            {
                NotifyPropertyChanged(ref _progressBarValue, value);
            }
        }
    }
}
