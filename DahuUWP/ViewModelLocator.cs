using DahuUWP.Models;
using DahuUWP.Services;
using DahuUWP.ViewModels;
using DahuUWP.ViewModels.Profil;
using DahuUWP.ViewModels.Profil.Private;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DahuUWP
{
    public class ViewModelLocator
    {
        static public DahuViewModelBase CurrentViewModel { get; set; }

        static ViewModelLocator()
        {
            
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            if (ViewModelBase.IsInDesignModeStatic)
            {
                SimpleIoc.Default.Register<IDataService, DesignDataService>();
                //SimpleIoc.Default.Register<IServiceConnection, DesignServiceClient>();
            }
            else
            {
                SimpleIoc.Default.Register<IDataService, DataService>();
                //SimpleIoc.Default.Register<IServiceConnection, ServiceClient>();
            }
            
            SimpleIoc.Default.Register<HomePageViewModel>();
            SimpleIoc.Default.Register<ConnectionViewModel>();
            SimpleIoc.Default.Register<PrivateProfilMainInformationViewModel>();
        }

        public HomePageViewModel HomePageVM
        {
            get { CurrentViewModel = ServiceLocator.Current.GetInstance<HomePageViewModel>(); return (HomePageViewModel)CurrentViewModel; }
        }

        public ConnectionViewModel ConnectionVM
        {
            get { CurrentViewModel = ServiceLocator.Current.GetInstance<ConnectionViewModel>();  return (ConnectionViewModel)CurrentViewModel; }
        }

        public PrivateProfilMainInformationViewModel PrivateProfilMainInformationVM
        {
            get { CurrentViewModel = ServiceLocator.Current.GetInstance<PrivateProfilMainInformationViewModel>(); return (PrivateProfilMainInformationViewModel)CurrentViewModel; }
        }
    }
}
