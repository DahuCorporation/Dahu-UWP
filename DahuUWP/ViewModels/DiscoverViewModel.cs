using DahuUWP.DahuTech.Inputs;
using DahuUWP.DahuTech.Project;
using DahuUWP.Models;
using DahuUWP.Models.ModelManager;
using DahuUWP.Services;
using DahuUWP.Services.ModelManager;
using DahuUWP.Utils.Converter;
using DahuUWP.Views;
using DahuUWP.Views.Components.Inputs;
using DahuUWP.Views.Project;
using DahuUWP.Views.Project.Forum;
using DahuUWP.Views.Project.Managing;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace DahuUWP.ViewModels
{
    public class DiscoverViewModel : DahuViewModelBase
    {
        public ICommand OnPageLoadedCommand { get; private set; }

        public DiscoverViewModel(IDataService service)
        {
            dataService = service;
            OnPageLoadedCommand = new RelayCommand(OnPageLoaded);

        }

        private async void OnPageLoaded()
        {
            
            ((HomePageViewModel)ViewModelLocator.HomePageViewModel).DahuSpecMenuOptions.SwitchOrActiveCurrentTopBarNodeMenu(typeof(Discover));
            ((HomePageViewModel)ViewModelLocator.HomePageViewModel).DahuSpecMenuOptions.ReasearchVisibility = Visibility.Visible;
            LoadProjects();
        }

        private async void LoadProjects()
        {
            InitViewProjectButtonBindings();
            ProjectManager projectManager = (ProjectManager)dataService.GetProjectManager();
            List<DahuUWP.Models.Project> projects = (await projectManager.Charge(null)).Cast<DahuUWP.Models.Project>().ToList();
            if (projects != null)
                ProjectContainerList = new ObservableCollection<Models.Project>(projects);
        }

        private void InitViewProjectButtonBindings()
        {
            ViewProjectButtonBindings = new DahuButtonBindings()
            {
                IsBusy = false,
                Name = "En savoir plus",
                RedirectedLink = typeof(ProjectView)
            };
        }

        private DahuButtonBindings _viewProjectButtonBindings;
        public DahuButtonBindings ViewProjectButtonBindings
        {
            get { return _viewProjectButtonBindings; }
            set
            {
                NotifyPropertyChanged(ref _viewProjectButtonBindings, value);
            }
        }

        //private DahuButtonBindings _knowMoreProjectButtonBindings;
        //public DahuButtonBindings KnowMoreProjectButtonBindings
        //{
        //    get { return _knowMoreProjectButtonBindings; }
        //    set
        //    {
        //        NotifyPropertyChanged(ref _knowMoreProjectButtonBindings, value);
        //    }
        //}

        private ObservableCollection<DahuUWP.Models.Project> _projectContainerList;
        public ObservableCollection<DahuUWP.Models.Project> ProjectContainerList
        {
            get { return _projectContainerList; }
            set
            {
                NotifyPropertyChanged(ref _projectContainerList, value);
            }
        }


        private async Task<string> TimeLooser()
        {
            SkillManager skillManager = (SkillManager)dataService.GetSkillManager();
            Dictionary<string, object> skillChargeParams = new Dictionary<string, object>();
            skillChargeParams.Add("mail", AppStaticInfo.Account.Mail);

            List<Object> skills = await skillManager.ChargeAsync(skillChargeParams);

            //await System.Threading.Tasks.Task.Delay(8000);
            return "toto";
        }
    }
}
