using DahuUWP.DahuTech.Inputs;
using DahuUWP.Models;
using DahuUWP.Models.ModelManager;
using DahuUWP.Services;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DahuUWP.ViewModels.Project.Team
{
    public class TeamViewModel : DahuViewModelBase
    {
        public ICommand OnPageLoadedCommand { get; private set; }

        public TeamViewModel(IDataService service)
        {
            dataService = service;

            OnPageLoadedCommand = new RelayCommand(OnPageLoaded);
        }

        private async void OnPageLoaded()
        {
            AcceptUserButtonBindings = new DahuButtonBindings
            {
                FuncListener = AcceptUser
            };
            ProjectManager projectManager = new ProjectManager();
            
            Project = await projectManager.ChargeOneProject(((DahuUWP.Models.Project)ViewModelLocator.HomePageViewModel.NavigationParam).Uuid);
            if (Project == null)
            {
                Project = (DahuUWP.Models.Project)NavigationParam;
            }
            List<User> result = Project.Members.FindAll(x => x.Status == "join");
            UsersWaitingAcceptationList = new ObservableCollection<User>(result);

        }

        private async void AcceptUser(object param)
        {
            ProjectManager projectManager = new ProjectManager();
            await projectManager.ChangeUserState(Project.Uuid, ((User)param).Uuid, "worker");
        }

        private ObservableCollection<User> _usersWaitingAcceptationList;
        public ObservableCollection<User> UsersWaitingAcceptationList
        {
            get { return _usersWaitingAcceptationList; }
            set
            {
                NotifyPropertyChanged(ref _usersWaitingAcceptationList, value);
            }
        }

        private DahuButtonBindings _acceptUserButtonBindings;
        public DahuButtonBindings AcceptUserButtonBindings
        {
            get { return _acceptUserButtonBindings; }
            set
            {
                NotifyPropertyChanged(ref _acceptUserButtonBindings, value);
            }
        }


        private bool NotifyPropertyChanged<T>(ref T variable, T valeur, [CallerMemberName] string nomPropriete = null)
        {
            if (object.Equals(variable, valeur)) return false;

            variable = valeur;
            RaisePropertyChanged(nomPropriete);
            return true;
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
