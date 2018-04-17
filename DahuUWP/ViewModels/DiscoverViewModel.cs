using DahuUWP.DahuTech.Inputs;
using DahuUWP.DahuTech.Project;
using DahuUWP.Models;
using DahuUWP.Models.ModelManager;
using DahuUWP.Services;
using DahuUWP.Services.ModelManager;
using DahuUWP.Views;
using DahuUWP.Views.Components.Inputs;
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
using Windows.UI.Xaml;

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
            HomePage.DahuFrame.Navigate(typeof(ManageProject));
            ((HomePageViewModel)ViewModelLocator.HomePageViewModel).DahuSpecMenuOptions.SwitchOrActiveCurrentTopBarNodeMenu(typeof(Discover));
            ((HomePageViewModel)ViewModelLocator.HomePageViewModel).DahuSpecMenuOptions.ReasearchVisibility = Visibility.Visible;
            LoadProjects();
        }

        private async void LoadProjects()
        {
            InitKnowMoreProjectButtonBindings();
            // J'ai fais ça car au départ je faisais ça:
            //<container:MediumProjectContainer ButtonBindings="{Binding ElementName=projectsItemsControl, Path=DataContext.KnowMoreProjectButtonBindings, UpdateSourceTrigger=PropertyChanged}"
            // Mais ça ne fonctionné pas le binding était en retard par rapport à l'objet en lui même pas possible de set l'uuid
            ProjectManager projectManager = (ProjectManager)dataService.GetProjectManager();
            List< DahuUWP.Models.Project> projects = (await projectManager.Charge(null)).Cast<DahuUWP.Models.Project>().ToList();
            List<MediumProjectContainer> mediumProjectContainerList = new List<MediumProjectContainer>();
            foreach (DahuUWP.Models.Project project in projects)
            {
                KnowMoreProjectButtonBindings.Parameter = project.Uuid;
                MediumProjectContainer mediumProjectContainer = new MediumProjectContainer
                {
                    Project = project,
                    ButtonBindings = KnowMoreProjectButtonBindings
                };
                mediumProjectContainerList.Add(mediumProjectContainer);
            }
            MediumProjectContainerList = new ObservableCollection<MediumProjectContainer>(mediumProjectContainerList);

        }

        private void InitKnowMoreProjectButtonBindings()
        {
            KnowMoreProjectButtonBindings = new DahuButtonBindings()
            {
                IsBusy = false,
                Name = "En savoir plus",
                RedirectedLink = typeof(Discover)
            };
        }

        private DahuButtonBindings _knowMoreProjectButtonBindings;
        public DahuButtonBindings KnowMoreProjectButtonBindings
        {
            get { return _knowMoreProjectButtonBindings; }
            set
            {
                NotifyPropertyChanged(ref _knowMoreProjectButtonBindings, value);
            }
        }

        private ObservableCollection<MediumProjectContainer> _mediumProjectContainerList;
        public ObservableCollection<MediumProjectContainer> MediumProjectContainerList
        {
            get { return _mediumProjectContainerList; }
            set
            {
                NotifyPropertyChanged(ref _mediumProjectContainerList, value);
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
