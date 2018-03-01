﻿using DahuUWP.DahuTech;
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
            AddSkillCommand = new RelayCommand(AddSkill);
            OnPageLoadedCommand = new RelayCommand(OnPageLoaded);
        }

        private async void OnPageLoaded()
        {
            LoadUserSkills();
        }

        private void LoadUserSkills()
        {
            SkillManager skillManager = (SkillManager)dataService.GetSkillManager();
            Dictionary<string, object> skillChargeParams = new Dictionary<string, object>();
            skillChargeParams.Add("mail", AppStaticInfo.Account.Mail);
            Skills = new ObservableCollection<Skill>(skillManager.Charge(skillChargeParams).Cast<Skill>().ToList());
        }

        private void AddSkill()
        {
            if (!VerifAddSkillFields())
                return;
            SkillManager skillManager = (SkillManager)dataService.GetSkillManager();
            UserManager userManager = (UserManager)dataService.GetUserManager();

            Skill skillToSave = new Skill
            {
                Name = SkillName,
                Description = SkillDescription
            };
            if (skillManager.Create(skillToSave))
            {
                Dictionary<string, object> chargeOneSkillParams = new Dictionary<string, object>
                {
                    { "name", skillToSave.Name }
                };
                Skill skillSaved = (Skill)skillManager.ChargeOneSkill(chargeOneSkillParams);
                userManager.AddSkillToUser(skillSaved.Uuid, AppStaticInfo.Account.Uuid);
                LoadUserSkills();
            }
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
