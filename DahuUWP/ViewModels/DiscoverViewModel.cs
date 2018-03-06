using DahuUWP.DahuTech.Inputs;
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
        public ICommand OnPageLoadedCommand { get; private set; }

        public ObservableCollection<DahuUWP.Models.Project> Projects { get; set; }

        public DiscoverViewModel(IDataService service)
        {
            dataService = service;

            OnPageLoadedCommand = new RelayCommand(OnPageLoaded);
        }

        private async void OnPageLoaded()
        {
            LoadProjects();
        }

        private async void LoadProjects()
        {
            ProjectManager projectManager = (ProjectManager)dataService.GetProjectManager();
            Projects = new ObservableCollection<DahuUWP.Models.Project>((await projectManager.Charge(null)).Cast<DahuUWP.Models.Project>().ToList());
            AddSkillButtonBindings = new DahuButtonBindings
            {
                IsBusy = false,
                TappedFuncListener = AddSkillTappedListener
            };
        }

        private DahuButtonBindings _addSkillButtonBindings;
        public DahuButtonBindings AddSkillButtonBindings
        {
            get { return _addSkillButtonBindings; }
            set
            {
                NotifyPropertyChanged(ref _addSkillButtonBindings, value);
            }
        }

        private bool NotifyPropertyChanged<T>(ref T variable, T valeur, [CallerMemberName] string nomPropriete = null)
        {
            if (object.Equals(variable, valeur)) return false;

            variable = valeur;
            RaisePropertyChanged(nomPropriete);
            return true;
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

        public async void AddSkillTappedListener()
        {
            AddSkillButtonBindings.IsBusy = true;
            string ttat = await TimeLooser();
            AddSkillButtonBindings.IsBusy = false;
            string titi = "ezf";
        }
    }
}
