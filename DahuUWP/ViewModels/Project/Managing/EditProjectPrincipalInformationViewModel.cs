using DahuUWP.DahuTech;
using DahuUWP.DahuTech.Inputs;
using DahuUWP.Models;
using DahuUWP.Models.ModelManager;
using DahuUWP.Services;
using DahuUWP.Services.ModelManager;
using DahuUWP.Views;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DahuUWP.ViewModels.Project.Managing
{
    public class EditProjectPrincipalInformationViewModel : DahuViewModelBase
    {
        public ICommand OnPageLoadedCommand { get; private set; }

        public EditProjectPrincipalInformationViewModel(IDataService service)
        {
            dataService = service;
            OnPageLoadedCommand = new RelayCommand(OnPageLoaded);
        }

        private async void OnPageLoaded()
        {
            Project = (DahuUWP.Models.Project)NavigationParam;
            UpdateProjectMainInformation = new DahuButtonBindings
            {
                IsBusy = false,
                FuncListener = UpdateMainInformation
            };

            DeleteProjectBinding = new DahuButtonBindings
            {
                IsBusy = false,
                FuncListener = DeleteProject
            };
        }

        private async void UpdateMainInformation(object param)
        {
            if (verifProjectUpdate())
            {
                UpdateProjectMainInformation.IsBusy = true;
                ProjectManager projectManager = (ProjectManager)dataService.GetProjectManager();
                await projectManager.EditProject(Project, Project.Uuid);
                UpdateProjectMainInformation.IsBusy = false;
            }
        }

        private async void DeleteProject(object param)
        {
            //
            if (!DeleteProjectBinding.IsBusy)
            {
                DeleteProjectBinding.IsBusy = true;
                ProjectManager projectManager = (ProjectManager)dataService.GetProjectManager();
                await projectManager.DeleteProject(Project.Uuid);
                DeleteProjectBinding.IsBusy = false;
                HomePage.DahuFrame.Navigate(typeof(Discover));
            }
        }

        

        private bool verifProjectUpdate()
        {
            if (Project.Name.Length > 1 && Project.Description.Length > 5)
            {
                return true;
            }
            AppGeneral.UserInterfaceStatusDico["Project update lack characters."].Display();
            return false;
        }

        private DahuButtonBindings _updateProjectMainInformation;
        public DahuButtonBindings UpdateProjectMainInformation
        {
            get { return _updateProjectMainInformation; }
            set
            {
                NotifyPropertyChanged(ref _updateProjectMainInformation, value);
            }
        }

        private DahuButtonBindings _deleteProjectBinding;
        public DahuButtonBindings DeleteProjectBinding
        {
            get { return _deleteProjectBinding; }
            set
            {
                NotifyPropertyChanged(ref _deleteProjectBinding, value);
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
