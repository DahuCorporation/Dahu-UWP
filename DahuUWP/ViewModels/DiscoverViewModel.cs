using DahuUWP.Models;
using DahuUWP.Models.ModelManager;
using DahuUWP.Services;
using DahuUWP.Views.Components.Inputs;
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

        public ObservableCollection<DahuUWP.Models.Project> Projects { get; set; }


        public void ActionMethod(bool res)
        {
            string toto = "zfezfe";
        }


        private Action<bool> _onAction;
        public Action<bool> OnAction
        {
            get { return _onAction; }
            set
            {
                NotifyPropertyChanged(ref _onAction, value);
            }
        }

        private string _onActionString;
        public string OnActionString
        {
            get { return _onActionString; }
            set
            {
                NotifyPropertyChanged(ref _onActionString, value);
            }
        }

        private bool NotifyPropertyChanged<T>(ref T variable, T valeur, [CallerMemberName] string nomPropriete = null)
        {
            if (object.Equals(variable, valeur)) return false;

            variable = valeur;
            RaisePropertyChanged(nomPropriete);
            return true;
        }


        private DahuInputText3 _tet;
        public DahuInputText3 Tet
        {
            get { return _tet; }
            set
            {
                NotifyPropertyChanged(ref _tet, value);
            }
        }

        //public DahuInputText3 tet2 = new DahuInputText3();

        public DiscoverViewModel(IDataService service)
        {
            Tet = new DahuInputText3();
            Tet.OnAction = ActionMethod;
            Tet.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            OnAction = ActionMethod;
            OnActionString = "test";
            dataService = service;

            ProjectManager projectManager = (ProjectManager)dataService.GetProjectManager();
            Projects = new ObservableCollection<DahuUWP.Models.Project>(projectManager.Charge(null).Cast<DahuUWP.Models.Project>().ToList());
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
