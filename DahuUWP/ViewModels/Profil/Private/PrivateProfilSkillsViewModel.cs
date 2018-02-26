using DahuUWP.Models;
using DahuUWP.Models.ModelManager;
using DahuUWP.Services;
using DahuUWP.Services.ModelManager;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DahuUWP.ViewModels.Profil.Private
{
    public class PrivateProfilSkillsViewModel : DahuViewModelBase
    {
        public ICommand AddSkillCommand { get; set; }

        public PrivateProfilSkillsViewModel(IDataService service)
        {
            dataService = service;
            AddSkillCommand = new RelayCommand(AddSkill);
        }

        private void AddSkill()
        {
            SkillManager skillManager = (SkillManager)dataService.GetSkillManager();
            UserManager userManager = (UserManager)dataService.GetUserManager();

            Dictionary<string, object> chargeOneSkillParams = new Dictionary<string, object>();
            Skill skillToSave = new Skill
            {
                Name = SkillName,
                Description = SkillDescription
            };
            skillManager.Create(skillToSave);
            chargeOneSkillParams.Add("name", skillToSave.Name);
            Skill skillSaved = (Skill)skillManager.ChargeOneSkill(chargeOneSkillParams);
            userManager.AddSkillToUser(skillSaved.Uuid, AppStaticInfo.Account.Uuid);
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
