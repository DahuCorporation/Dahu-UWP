using DahuUWP.DahuTech;
using DahuUWP.DahuTech.Inputs;
using DahuUWP.Models;
using DahuUWP.Models.ModelManager;
using DahuUWP.Services;
using DahuUWP.Services.ModelManager;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DahuUWP.ViewModels.Profil.Private
{
    public class PrivateProfilSkillsViewModel : DahuViewModelBase
    {
        public ICommand OnPageLoadedCommand { get; private set; }
        public ICommand AddSkillCommand { get; set; }
        
        public PrivateProfilSkillsViewModel(IDataService service)
        {
            dataService = service;
            AddSkillCommand = new RelayCommand<object>(AddSkill);
            OnPageLoadedCommand = new RelayCommand(OnPageLoaded);
            AddSkillButtonBindings = new DahuButtonBindings
            {
                IsBusy = false,
                FuncListener = AddSkill
            };
        }

        private async void OnPageLoaded()
        {
            LoadUserSkills();
        }

        private async void LoadUserSkills()
        {
            UserManager userManager = (UserManager)dataService.GetUserManager();

            User user = await userManager.Charge(AppStaticInfo.Account.Uuid);
            if (user.Skills != null)
                Skills = new ObservableCollection<Skill>(user.Skills);
        }

        private async void AddSkill(object pararm)
        {
            
            if (!VerifAddSkillFields())
                return;
            AddSkillButtonBindings.IsBusy = true;
            SkillManager skillManager = (SkillManager)dataService.GetSkillManager();
            UserManager userManager = (UserManager)dataService.GetUserManager();

            List<Skill> skills = await skillManager.ChargeAllSkills();
            Skill newUserSkillAlreadyExists = skills.FirstOrDefault(it => it.Name == SkillName);
            if (newUserSkillAlreadyExists != null) //if exists
            {
                await userManager.AddSkillToUser(newUserSkillAlreadyExists.Uuid);
                LoadUserSkills();
                //add skill to user list
                // verif s'il n'est pas déjà dans la liste du user
                // update la list en front
            } else
            {
                Skill skillToSave = new Skill
                {
                    Name = SkillName,
                    Description = SkillDescription
                };
                Skill newSkill = await skillManager.Create(skillToSave);
                if (newSkill != null)
                    await userManager.AddSkillToUser(newSkill.Uuid);
                LoadUserSkills();
                //create list dans la bdd
                //add skill to user list
                // update la list en front
            }
            AddSkillButtonBindings.IsBusy = false;
        }

        private bool VerifAddSkillFields()
        {
            if (String.IsNullOrEmpty(SkillName))
            {
                AppGeneral.UserInterfaceStatusDico["Empty skill name."].Display();
                return false;
            }
            else if (String.IsNullOrEmpty(SkillDescription))
            {
                AppGeneral.UserInterfaceStatusDico["Empty skill description."].Display();
                return false;
            }
            return true;
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

        private ObservableCollection<Skill> _skills;
        public ObservableCollection<Skill> Skills
        {
            get { return _skills; }
            set
            {
                NotifyPropertyChanged(ref _skills, value);
            }
        }

        private string _skillName;
        public string SkillName
        {
            get { return _skillName; }
            set
            {
                NotifyPropertyChanged(ref _skillName, value);
            }
        }

        private string _skillDescription;
        public string SkillDescription
        {
            get { return _skillDescription; }
            set
            {
                NotifyPropertyChanged(ref _skillDescription, value);
            }
        }

        private bool NotifyPropertyChanged<T>(ref T variable, T valeur, [CallerMemberName] string nomPropriete = null)
        {
            if (object.Equals(variable, valeur)) return false;

            variable = valeur;
            RaisePropertyChanged(nomPropriete);
            return true;
        }
    }
}
