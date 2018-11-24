using DahuUWP.DahuTech;
using DahuUWP.DahuTech.Inputs;
using DahuUWP.Models;
using DahuUWP.Models.ModelManager;
using DahuUWP.Services;
using DahuUWP.Views.Project;
using DahuUWP.Views.Search;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;

namespace DahuUWP.ViewModels.Search
{
    public class MainResearchViewModel : DahuViewModelBase
    {
        public ICommand OnPageLoadedCommand { get; private set; }

        public MainResearchViewModel(IDataService service)
        {
            dataService = service;

            OnPageLoadedCommand = new RelayCommand(OnPageLoaded);
        }

        private async void SearchResult()
        {
            ProjectResults = null;
            UserResults = null;
            string researchValue = (string)NavigationParam;
            if (!string.IsNullOrWhiteSpace(researchValue))
            {
                // [Project] search
                ProjectManager projectManager = (ProjectManager)dataService.GetProjectManager();
                List<DahuUWP.Models.Project> projects = (await projectManager.Charge(null)).Cast<DahuUWP.Models.Project>().ToList();
                List<DahuUWP.Models.Project> projectResult = FindProjectsMatching(projects, researchValue);
                ProjectResults = new ObservableCollection<DahuUWP.Models.Project>(projectResult);
                DahuAllInBtnProjectResultVisibility = (projectResult.Count() > 0) ? Visibility.Collapsed : Visibility.Visible;

                // [User] search
                UserManager userManager = (UserManager)dataService.GetUserManager();
                List<User> users = (await userManager.Charges(null)).Cast<User>().ToList();
                List<object> userResult = FindUsersMatching(users, researchValue);
                UserResults = new ObservableCollection<object>(userResult);
                DahuAllInBtnUserResultVisibility = (userResult.Count() > 0) ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        private async void OnPageLoaded()
        {
            ((HomePageViewModel)ViewModelLocator.HomePageViewModel).DahuSpecMenuOptions.ReasearchVisibility = Visibility.Visible;
            SearchResult();
            InitManageProjectButtonBindings();
        }

        private void InitManageProjectButtonBindings()
        {
            DiscoverProjectButtonBindings = new DahuButtonBindings()
            {
                IsBusy = false,
                Name = "Découvrir",
                RedirectedLink = typeof(ProjectView)
            };
        }

        private DahuButtonBindings _discoverProjectButtonBindings;
        public DahuButtonBindings DiscoverProjectButtonBindings
        {
            get { return _discoverProjectButtonBindings; }
            set
            {
                NotifyPropertyChanged(ref _discoverProjectButtonBindings, value);
            }
        }

        // TODO: guys !! do this in the API directly
        private List<DahuUWP.Models.Project> FindProjectsMatching(List<DahuUWP.Models.Project> projects, string research)
        {
            List<DahuUWP.Models.Project> projectResult = new List<DahuUWP.Models.Project>();

            foreach (DahuUWP.Models.Project project in projects)
            {
                if (project.Name.IndexOf(research, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    projectResult.Add(project);
                }
            }
            return projectResult;
        }

        private List<object> FindUsersMatching(List<User> users, string research)
        {
            List<object> userResult = new List<object>();
            foreach (User user in users)
            {
                if (user.FirstName.IndexOf(research, StringComparison.OrdinalIgnoreCase) >= 0
                    || user.LastName.IndexOf(research, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    var userR = new {
                        FullName = user.FirstName + " " + user.LastName,
                        Biography = user.Biography,
                        Media = user.Media
                    };
                    userResult.Add(userR);
                }
            }
            return userResult;
        }

        private ObservableCollection<DahuUWP.Models.Project> _projectResults;
        public ObservableCollection<DahuUWP.Models.Project> ProjectResults
        {
            get { return _projectResults; }
            set
            {   
                NotifyPropertyChanged(ref _projectResults, value);
            }
        }

        private ObservableCollection<object> _userResults;
        public ObservableCollection<object> UserResults
        {
            get { return _userResults; }
            set
            {
                NotifyPropertyChanged(ref _userResults, value);
            }
        }

        private Visibility _DahuAllInBtnProjectResultVisibility;
        public Visibility DahuAllInBtnProjectResultVisibility
        {
            get { return _DahuAllInBtnProjectResultVisibility; }
            set
            {
                NotifyPropertyChanged(ref _DahuAllInBtnProjectResultVisibility, value);
            }
        }

        private Visibility _DahuAllInBtnUserResultVisibility;
        public Visibility DahuAllInBtnUserResultVisibility
        {
            get { return _DahuAllInBtnUserResultVisibility; }
            set
            {
                NotifyPropertyChanged(ref _DahuAllInBtnUserResultVisibility, value);
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
