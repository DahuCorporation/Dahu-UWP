using DahuUWP.Models;
using DahuUWP.Models.ModelManager;
using DahuUWP.Services;
using DahuUWP.Services.ModelManager;
using DahuUWP.Views;
using DahuUWP.Views.Profil.Private;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DahuUWP.ViewModels.Profil.Public
{
    public class PublicProfilViewModel : DahuViewModelBase
    {
        public ICommand OnPageLoadedCommand { get; private set; }
        public ICommand ProfilSettingsLinkCommand { get; set; }

        public PublicProfilViewModel(IDataService service)
        {
            dataService = service;
            ProfilSettingsLinkCommand = new RelayCommand(ProfilSettingsLink);
            OnPageLoadedCommand = new RelayCommand(OnPageLoaded);
        }

        private async void OnPageLoaded()
        {
            LoadUserInfos();
            LoadUserSkills();
        }

        private async void LoadUserInfos()
        {
            UserManager userManager = (UserManager)dataService.GetUserManager();

            User user = await userManager.Charge(AppStaticInfo.Account.Uuid);
            UserFullName = user.FirstName + " " + user.LastName;
            UserBiography = user.Biography;

            if (user.Skills != null)
                Skills = new ObservableCollection<Skill>(user.Skills);

            List<Models.Project> projectList = await userManager.ChargeProjects(AppStaticInfo.Account.Uuid, null);
            if (projectList != null)
                UserProjects = new ObservableCollection<Models.Project>(projectList);
        }

        private async void LoadUserSkills()
        {
            UserManager userManager = (UserManager)dataService.GetUserManager();
            User user = await userManager.Charge(AppStaticInfo.Account.Uuid);
            string tit = "zef";
            //List<object> userSkillList = await skillManager.Charge(skillChargeParams);
            //if (userSkillList != null)
            //    Skills = new ObservableCollection<Skill>(userSkillList.Cast<Skill>().ToList());

            //SkillManager skillManager = (SkillManager)dataService.GetSkillManager();
            //Dictionary<string, object> skillChargeParams = new Dictionary<string, object>();
            //skillChargeParams.Add("mail", AppStaticInfo.Account.Mail);
            //List<object> userSkillList = await skillManager.Charge(skillChargeParams);
            //if (userSkillList != null)
            //    Skills = new ObservableCollection<Skill>(userSkillList.Cast<Skill>().ToList());
        }

        private ObservableCollection<Skill> _skills;
        public ObservableCollection<Skill> Skills
        {
            get { return _skills; }
            set
            {
                NotifyPropertyChanged(ref _skills, value);
            }
        }

        private ObservableCollection<DahuUWP.Models.Project> _userProjects;
        public ObservableCollection<DahuUWP.Models.Project> UserProjects
        {
            get { return _userProjects; }
            set
            {
                NotifyPropertyChanged(ref _userProjects, value);
            }
        }

        private string _userFullName;
        public string UserFullName
        {
            get { return _userFullName; }
            set
            {
                NotifyPropertyChanged(ref _userFullName, value);
            }
        }

        private string _userBiography;
        public string UserBiography
        {
            get { return _userBiography; }
            set
            {
                NotifyPropertyChanged(ref _userBiography, value);
            }
        }

        private bool NotifyPropertyChanged<T>(ref T variable, T valeur, [CallerMemberName] string nomPropriete = null)
        {
            if (object.Equals(variable, valeur)) return false;

            variable = valeur;
            RaisePropertyChanged(nomPropriete);
            return true;
        }

        private void ProfilSettingsLink()
        {
            HomePage.DahuFrame.Navigate(typeof(PrivateProfil));
        }
    }
}
