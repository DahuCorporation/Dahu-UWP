using CommonServiceLocator;
using DahuUWP.Models;
using DahuUWP.Services;
using DahuUWP.ViewModels;
using DahuUWP.ViewModels.Component;
using DahuUWP.ViewModels.Profil;
using DahuUWP.ViewModels.Profil.Private;
using DahuUWP.ViewModels.Profil.Public;
using DahuUWP.ViewModels.Project;
using DahuUWP.ViewModels.Project.Forum;
using DahuUWP.ViewModels.Project.Managing;
using DahuUWP.ViewModels.Search;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
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

        static public DahuViewModelBase HomePageViewModel { get; set; }

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
            SimpleIoc.Default.Register<RegisterViewModel>();
            SimpleIoc.Default.Register<PrivateProfilMainInformationViewModel>();
            SimpleIoc.Default.Register<PrivateProfilSkillsViewModel>();
            SimpleIoc.Default.Register<DiscoverViewModel>();
            SimpleIoc.Default.Register<PublicProfilViewModel>();

            //Project
            SimpleIoc.Default.Register<EditProjectMembersViewModel>();
            SimpleIoc.Default.Register<EditProjectPrincipalInformationViewModel>();
            SimpleIoc.Default.Register<EditProjectParametersViewModel>();
            SimpleIoc.Default.Register<ManageProjectViewModel>();
            SimpleIoc.Default.Register<CreateProjectViewModel>(); 
            SimpleIoc.Default.Register<MyProjectsViewModel>();
            SimpleIoc.Default.Register<ForumViewModel>();

            //Search
            SimpleIoc.Default.Register<MainResearchViewModel>();

            SimpleIoc.Default.Register<DahuSpecMenuViewModel>();

        }

        public HomePageViewModel HomePageVM
        {
            get { CurrentViewModel = ServiceLocator.Current.GetInstance<HomePageViewModel>(); HomePageViewModel = ServiceLocator.Current.GetInstance<HomePageViewModel>();  return (HomePageViewModel)CurrentViewModel; }
        }

        public ConnectionViewModel ConnectionVM
        {
            get { CurrentViewModel = ServiceLocator.Current.GetInstance<ConnectionViewModel>();  return (ConnectionViewModel)CurrentViewModel; }
        }

        public RegisterViewModel RegisterVM
        {
            get { CurrentViewModel = ServiceLocator.Current.GetInstance<RegisterViewModel>(); return (RegisterViewModel)CurrentViewModel; }
        }

        public PrivateProfilMainInformationViewModel PrivateProfilMainInformationVM
        {
            get { CurrentViewModel = ServiceLocator.Current.GetInstance<PrivateProfilMainInformationViewModel>(); return (PrivateProfilMainInformationViewModel)CurrentViewModel; }
        }

        public PrivateProfilSkillsViewModel PrivateProfilSkillsVM
        {
            get { CurrentViewModel = ServiceLocator.Current.GetInstance<PrivateProfilSkillsViewModel>(); return (PrivateProfilSkillsViewModel)CurrentViewModel; }
        }

        public DiscoverViewModel DiscoverVM
        {
            get { CurrentViewModel = ServiceLocator.Current.GetInstance<DiscoverViewModel>(); return (DiscoverViewModel)CurrentViewModel; }
        }

        public PublicProfilViewModel PublicProfilVM
        {
            get { CurrentViewModel = ServiceLocator.Current.GetInstance<PublicProfilViewModel>(); return (PublicProfilViewModel)CurrentViewModel; }
        }

        //Project
        public EditProjectMembersViewModel EditProjectMembersVM
        {
            get { CurrentViewModel = ServiceLocator.Current.GetInstance<EditProjectMembersViewModel>(); return (EditProjectMembersViewModel)CurrentViewModel; }
        }
        public EditProjectPrincipalInformationViewModel EditProjectPrincipalInformationVM
        {
            get { CurrentViewModel = ServiceLocator.Current.GetInstance<EditProjectPrincipalInformationViewModel>(); return (EditProjectPrincipalInformationViewModel)CurrentViewModel; }
        }
        public EditProjectParametersViewModel EditProjectParametersVM
        {
            get { CurrentViewModel = ServiceLocator.Current.GetInstance<EditProjectParametersViewModel>(); return (EditProjectParametersViewModel)CurrentViewModel; }
        }
        public ManageProjectViewModel ManageProjectVM
        {
            get { CurrentViewModel = ServiceLocator.Current.GetInstance<ManageProjectViewModel>(); return (ManageProjectViewModel)CurrentViewModel; }
        }
        public CreateProjectViewModel CreateProjectVM
        {
            get { CurrentViewModel = ServiceLocator.Current.GetInstance<CreateProjectViewModel>(); return (CreateProjectViewModel)CurrentViewModel; }
        }
        public MyProjectsViewModel MyProjectsVM
        {
            get { CurrentViewModel = ServiceLocator.Current.GetInstance<MyProjectsViewModel>(); return (MyProjectsViewModel)CurrentViewModel; }
        }
        public ForumViewModel ForumVM
        {
            get { CurrentViewModel = ServiceLocator.Current.GetInstance<ForumViewModel>(); return (ForumViewModel)CurrentViewModel; }
        }

        //Search
        public MainResearchViewModel MainResearchVM
        {
            get { CurrentViewModel = ServiceLocator.Current.GetInstance<MainResearchViewModel>(); return (MainResearchViewModel)CurrentViewModel; }
        }

        public DahuSpecMenuViewModel DahuSpecMenuVM
        {
            get { CurrentViewModel = ServiceLocator.Current.GetInstance<DahuSpecMenuViewModel>(); return (DahuSpecMenuViewModel)CurrentViewModel; }
        }
    }
}
