using DahuUWP.Models;
using DahuUWP.Models.ModelManager;
using DahuUWP.Services;
using DahuUWP.Services.ModelManager;
using DahuUWP.Views.Components.Inputs;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DahuUWP.ViewModels
{
    public class DiscoverViewModel : DahuViewModelBase
    {
        public ObservableCollection<DahuUWP.Models.Project> Projects { get; set; }

        public void ActionMethod(bool res)
        {
            string toto = "zfezfe";
        }

        private Action _addSkillButtonTapped;
        public Action AddSkillButtonTapped
        {
            get { return _addSkillButtonTapped; }
            set
            {
                NotifyPropertyChanged(ref _addSkillButtonTapped, value);
            }
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

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                NotifyPropertyChanged(ref _isBusy, value);
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

        private async Task<string> TimeLooser()
        {
            SkillManager skillManager = (SkillManager)dataService.GetSkillManager();
            Dictionary<string, object> skillChargeParams = new Dictionary<string, object>();
            skillChargeParams.Add("mail", AppStaticInfo.Account.Mail);

            //List<Object> skills = await skillManager.ChargeAsync(skillChargeParams);

            await System.Threading.Tasks.Task.Delay(8000);
            return "toto";
        }

        public async void ButtonFunc()
        {
            IsBusy = true;
            string ttat = await TimeLooser();
            IsBusy = false;
            string titi = "ezf";
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
            AddSkillButtonTapped = ButtonFunc;

        }
    }
}
