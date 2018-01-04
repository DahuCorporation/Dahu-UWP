using DahuUWP.Models;
using DahuUWP.Models.ModelManager;
using DahuUWP.Services;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DahuUWP.ViewModels.Profil.Private
{
    public class PrivateProfilMainInformationViewModel : DahuViewModelBase
    {
        private readonly IDataService dataService;

        public PrivateProfilMainInformationViewModel(IDataService service)
        {
            dataService = service;
            IModelManager userManager = (IModelManager)dataService.GetUserManager();

            Dictionary<string, object> userInfoParams = new Dictionary<string, object>
            {
                { "_token", AppStaticInfo.Account.Token }
            };
            userManager.Charge(userInfoParams);
        }
    }
}
