using DahuUWP.Models;
using DahuUWP.Models.ModelManager;
using DahuUWP.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DahuUWP.ViewModels
{
    public class DiscoverViewModel : DahuViewModelBase
    {
        //private List<Project> Projects = new List<Project>();

        public ObservableCollection<Project> Projects { get; set; }


        private bool NotifyPropertyChanged<T>(ref T variable, T valeur, [CallerMemberName] string nomPropriete = null)
        {
            if (object.Equals(variable, valeur)) return false;

            variable = valeur;
            RaisePropertyChanged(nomPropriete);
            return true;
        }

        public DiscoverViewModel(IDataService service)
        {
            
            dataService = service;
            Project project = new Project
            {
                AccountId = 1,
                BannerPicture = "bp",
                Description = "description",
                Name = "Nom",
                ProfilePicture = "pp",
                Uuid = "efz"
            };
            ProjectManager projectManager = (ProjectManager)dataService.GetProjectManager();
            Projects = new ObservableCollection<Project>(projectManager.Charge(null).Cast<Project>().ToList());
            //Projects.Add(project);
            //Projects.Add(project);
            //List<Project> Projects2 = new List<Project>();
            //Project project = new Project();
            //project.Name = "Project name";
            //project.Description = "Project description";
            //Projects2.Add(project);
            //Projects = Projects2;
        }
    }
}
