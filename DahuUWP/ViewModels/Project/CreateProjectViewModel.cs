using DahuUWP.DahuTech;
using DahuUWP.DahuTech.Inputs;
using DahuUWP.Models.ModelManager;
using DahuUWP.Services;
using DahuUWP.Views.Project;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DahuUWP.ViewModels.Project
{
    public class CreateProjectViewModel : DahuViewModelBase
    {
        public ICommand OnPageLoadedCommand { get; private set; }

        public ICommand CreateProjectCommand { get; set; }

        public CreateProjectViewModel(IDataService service)
        {
            dataService = service;
            CreateProjectCommand = new RelayCommand<object>(CreateProject);
            InitPageButtons();
            OnPageLoadedCommand = new RelayCommand(OnPageLoaded);
        }


        private async void OnPageLoaded()
        {
            ((HomePageViewModel)ViewModelLocator.HomePageViewModel).DahuSpecMenuOptions.SwitchOrActiveCurrentTopBarNodeMenu(typeof(CreateProject));
        }

        private void InitPageButtons()
        {
            CreateProjectButtonBindings = new DahuButtonBindings
            {
                IsBusy = false,
                FuncListener = CreateProject
            };
        }

        private string _projectName;
        public string ProjectName
        {
            get { return _projectName; }
            set
            {
                NotifyPropertyChanged(ref _projectName, value);
            }
        }

        private string _projectDescription;
        public string ProjectDescription
        {
            get { return _projectDescription; }
            set
            {
                NotifyPropertyChanged(ref _projectDescription, value);
            }
        }

        private DahuButtonBindings _createProjectButtonBindings;
        public DahuButtonBindings CreateProjectButtonBindings
        {
            get { return _createProjectButtonBindings; }
            set
            {
                NotifyPropertyChanged(ref _createProjectButtonBindings, value);
            }
        }

        private bool NotifyPropertyChanged<T>(ref T variable, T valeur, [CallerMemberName] string nomPropriete = null)
        {
            if (object.Equals(variable, valeur)) return false;

            variable = valeur;
            RaisePropertyChanged(nomPropriete);
            return true;
        }

        private async void CreateProject(object pararm)
        {
            CreateProjectButtonBindings.IsBusy = true;
            DahuUWP.Models.Project project = new DahuUWP.Models.Project();
            ProjectManager projectManager = (ProjectManager)dataService.GetProjectManager();

            project.Name = ProjectName;
            project.Description = ProjectDescription;
            if (String.IsNullOrEmpty(project.Name))
                AppGeneral.UserInterfaceStatusDico["Empty name."].Display();
            else if (String.IsNullOrEmpty(project.Description))
                AppGeneral.UserInterfaceStatusDico["Empty description."].Display();
            else
                projectManager.Create(project);
            CreateProjectButtonBindings.IsBusy = false;
        }
    }
}
