using DahuUWP.DahuTech;
using DahuUWP.Models.ModelManager;
using DahuUWP.Services;
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
        public ICommand CreateProjectCommand { get; set; }

        public CreateProjectViewModel(IDataService service)
        {
            dataService = service;
            CreateProjectCommand = new RelayCommand(CreateProject);
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

        private bool NotifyPropertyChanged<T>(ref T variable, T valeur, [CallerMemberName] string nomPropriete = null)
        {
            if (object.Equals(variable, valeur)) return false;

            variable = valeur;
            RaisePropertyChanged(nomPropriete);
            return true;
        }

        private void CreateProject()
        {
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
        }
    }
}
