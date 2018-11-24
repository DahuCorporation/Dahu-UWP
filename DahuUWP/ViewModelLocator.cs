using CommonServiceLocator;
using DahuUWP.Models;
using DahuUWP.Services;
using DahuUWP.ViewModels;
using DahuUWP.ViewModels.Component;
using DahuUWP.ViewModels.Profil;
using DahuUWP.ViewModels.Profil.Private;
using DahuUWP.ViewModels.Profil.Public;
using DahuUWP.ViewModels.Project;
using DahuUWP.ViewModels.Project.Chat;
using DahuUWP.ViewModels.Project.Contribute;
using DahuUWP.ViewModels.Project.Forum;
using DahuUWP.ViewModels.Project.Managing;
using DahuUWP.ViewModels.Project.ScrumBoard;
using DahuUWP.ViewModels.Project.Team;
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
            SimpleIoc.Default.Register<EditProjectCounterpartsViewModel>();
            SimpleIoc.Default.Register<EditProjectPrincipalInformationViewModel>();
            SimpleIoc.Default.Register<EditProjectParametersViewModel>();
            SimpleIoc.Default.Register<ManageProjectViewModel>();
            SimpleIoc.Default.Register<CreateProjectViewModel>(); 
            SimpleIoc.Default.Register<MyProjectsViewModel>();
            SimpleIoc.Default.Register<ForumViewModel>();
            SimpleIoc.Default.Register<ScrumBoardViewModel>();
            SimpleIoc.Default.Register<ProjectViewViewModel>();
            SimpleIoc.Default.Register<ContributeViewModel>();
            SimpleIoc.Default.Register<ContributeAdressCounterpartyViewModel>();
            SimpleIoc.Default.Register<ContributePayementViewModel>();
            SimpleIoc.Default.Register<ContributeSuccessPaymentViewModel>();
            SimpleIoc.Default.Register<ContributeWithMoneyViewModel>();
            SimpleIoc.Default.Register<ChatViewModel>();
            SimpleIoc.Default.Register<TeamViewModel>();


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

        public TeamViewModel TeamVM
        {
            get { CurrentViewModel = ServiceLocator.Current.GetInstance<TeamViewModel>(); return (TeamViewModel)CurrentViewModel; }
        }

        public ContributePayementViewModel ContributePayementVM
        {
            get { CurrentViewModel = ServiceLocator.Current.GetInstance<ContributePayementViewModel>(); return (ContributePayementViewModel)CurrentViewModel; }
        }

        public EditProjectCounterpartsViewModel EditProjectCounterpartsVM
        {
            get { CurrentViewModel = ServiceLocator.Current.GetInstance<EditProjectCounterpartsViewModel>(); return (EditProjectCounterpartsViewModel)CurrentViewModel; }
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
        public ScrumBoardViewModel ScrumBoardVM
        {
            get { CurrentViewModel = ServiceLocator.Current.GetInstance<ScrumBoardViewModel>(); return (ScrumBoardViewModel)CurrentViewModel; }
        }

        public ProjectViewViewModel ProjectViewVM
        {
            get { CurrentViewModel = ServiceLocator.Current.GetInstance<ProjectViewViewModel>(); return (ProjectViewViewModel)CurrentViewModel; }
        }

        public ContributeViewModel ContributeVM
        {
            get { CurrentViewModel = ServiceLocator.Current.GetInstance<ContributeViewModel>(); return (ContributeViewModel)CurrentViewModel; }
        }

        public ContributeAdressCounterpartyViewModel ContributeAdressCounterpartyVM
        {
            get { CurrentViewModel = ServiceLocator.Current.GetInstance<ContributeAdressCounterpartyViewModel>(); return (ContributeAdressCounterpartyViewModel)CurrentViewModel; }
        }

        public ContributeSuccessPaymentViewModel ContributeSuccessPaymentVM
        {
            get { CurrentViewModel = ServiceLocator.Current.GetInstance<ContributeSuccessPaymentViewModel>(); return (ContributeSuccessPaymentViewModel)CurrentViewModel; }
        }

        public ContributeWithMoneyViewModel ContributeWithMoneyVM
        {
            get { CurrentViewModel = ServiceLocator.Current.GetInstance<ContributeWithMoneyViewModel>(); return (ContributeWithMoneyViewModel)CurrentViewModel; }
        }

        public ChatViewModel ChatVM
        {
            get { CurrentViewModel = ServiceLocator.Current.GetInstance<ChatViewModel>(); return (ChatViewModel)CurrentViewModel; }
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
