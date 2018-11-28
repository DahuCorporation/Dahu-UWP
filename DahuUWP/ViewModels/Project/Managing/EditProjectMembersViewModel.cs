using DahuUWP.DahuTech;
using DahuUWP.DahuTech.Inputs;
using DahuUWP.Models;
using DahuUWP.Models.ModelManager;
using DahuUWP.Services;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DahuUWP.ViewModels.Project.Managing
{
    public class EditProjectMembersViewModel : DahuViewModelBase
    {
        public ICommand OnPageLoadedCommand { get; private set; }


        public EditProjectMembersViewModel(IDataService service)
        {
            dataService = service;
            OnPageLoadedCommand = new RelayCommand(OnPageLoaded);
        }

        private async void OnPageLoaded()
        {
            Project = (DahuUWP.Models.Project)NavigationParam;
            DeleteMemberButtonBindings = new DahuButtonBindings
            {
                FuncListener = DeleteMember
            };

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
            UsersWaitingAcceptationList.Remove((User)param);
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

        public async void DeleteMember(object param)
        {

            var user = param as User;
            if (user.Uuid != Project.OwnerUuid)
            {
                ProjectManager projectManager = new ProjectManager();
                await projectManager.DeleteUser(Project.Uuid, user.Uuid);
                Project.Members.Remove(user);
            } else
            {
                AppGeneral.UserInterfaceStatusDico["Cannot remove your self."].Display();
            }

        }

        private DahuButtonBindings _deleteMemberButtonBindings;
        public DahuButtonBindings DeleteMemberButtonBindings
        {
            get { return _deleteMemberButtonBindings; }
            set
            {
                NotifyPropertyChanged(ref _deleteMemberButtonBindings, value);
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
