using DahuUWP.Models;
using DahuUWP.ViewModels;
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
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            if (ViewModelBase.IsInDesignModeStatic)
                SimpleIoc.Default.Register<IServiceClient, DesignServiceClient>();
            else
                SimpleIoc.Default.Register<IServiceClient, ServiceClient>();

            SimpleIoc.Default.Register<HomePageViewModel>();
        }

        public HomePageViewModel HomePageVM
        {
            get { return ServiceLocator.Current.GetInstance<HomePageViewModel>(); }
        }
    }
}
