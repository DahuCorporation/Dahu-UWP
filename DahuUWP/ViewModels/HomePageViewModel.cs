using DahuUWP.DahuTech;
using DahuUWP.DahuTech.Menu;
using DahuUWP.Models;
using DahuUWP.Models.ModelManager;
using DahuUWP.Services;
using DahuUWP.Views;
using DahuUWP.Views.Component;
using DahuUWP.Views.Project;
using DahuUWP.Views.Project.Managing;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace DahuUWP.ViewModels
{
    public class HomePageViewModel : DahuViewModelBase
    {
        private readonly IDataService serviceClient;

        private Visibility _modulNonConnected;
        public Visibility ModulNonConnected
        {
            get { return _modulNonConnected; }
            set
            {
                NotifyPropertyChanged(ref _modulNonConnected, value);
            }
        }

        private Visibility _modulConnected;
        public Visibility ModulConnected
        {
            get { return _modulConnected; }
            set
            {
                NotifyPropertyChanged(ref _modulConnected, value);
            }
        }

        private DahuSpecMenuOptions _dahuSpecMenuOptions;
        public DahuSpecMenuOptions DahuSpecMenuOptions
        {
            get { return _dahuSpecMenuOptions; }
            set
            {
                NotifyPropertyChanged(ref _dahuSpecMenuOptions, value);
            }
        }





        //private Visibility _topBarConnected;
        //public Visibility TopBarConnected
        //{
        //    get { return _topBarConnected; }
        //    set {
        //        NotifyPropertyChanged(ref _topBarConnected, value);
        //    }
        //}


        public void Connected(bool connected)
        {
            if (connected)
            {
                ModulNonConnected = Visibility.Collapsed;
                ModulConnected = Visibility.Visible;
            }
            else
            {
                ModulNonConnected = Visibility.Visible;
                ModulConnected = Visibility.Collapsed;
            }
        }

        private void InitDahuSpecMenuOptions()
        {
            List<TopBarNodeMenu> listNodes = new List<TopBarNodeMenu>
            {
                new TopBarNodeMenu{ Title = "Découvrir", PageLink = typeof(Discover)},
                new TopBarNodeMenu{ Title = "Mes projets", PageLink = typeof(MyProjects)},
                new TopBarNodeMenu{ Title = "Creer un projet", PageLink = typeof(CreateProject)}
            };
            DahuSpecMenuOptions = new DahuSpecMenuOptions(listNodes);
        }

        public HomePageViewModel(IDataService service)
        {
            ViewModelLocator.HomePageViewModel = this;
            InitDahuSpecMenuOptions();
            //Init the static class
            AppGeneral.ActiveIt();
            Connected(false);
        }
    }
}
