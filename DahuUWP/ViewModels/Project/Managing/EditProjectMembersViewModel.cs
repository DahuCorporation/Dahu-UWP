using DahuUWP.DahuTech;
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
