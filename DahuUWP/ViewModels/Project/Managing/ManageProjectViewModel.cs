﻿using DahuUWP.DahuTech.Menu;
using DahuUWP.Services;
using DahuUWP.ViewModels.Search;
using DahuUWP.Views;
using DahuUWP.Views.Project.Managing;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;

namespace DahuUWP.ViewModels.Project.Managing
{
    public class ManageProjectViewModel : DahuViewModelBase
    {
        public ICommand OnPageLoadedCommand { get; private set; }

        public ICommand EditProjectCommand { get; set; }

        public ManageProjectViewModel(IDataService service)
        {
            dataService = service;
            EditProjectCommand = new RelayCommand(EditProjectRedirect);
            OnPageLoadedCommand = new RelayCommand(OnPageLoaded);
            FillFullHorizontalMenu();
        }

        private void FillFullHorizontalMenu()
        {
            FullHorizontalMenu = new Menu();
            NodeMenu nodeMenu = new NodeMenu()
            {
                Active = true,
                Title = "Scrum board",
                FuncListener = FullHorizontalMenuNodeClicked,
                Parameter = typeof(Views.Project.ScrumBoard.ScrumBoard)
            };
            FullHorizontalMenu.Nodes.Add(nodeMenu);

            NodeMenu nodeMenu2 = new NodeMenu()
            {
                Active = false,
                Title = "Forum",
                FuncListener = FullHorizontalMenuNodeClicked,
                Parameter = typeof(Views.Project.CreateProject)
            };
            FullHorizontalMenu.Nodes.Add(nodeMenu2);

            NodeMenu nodeMenu3 = new NodeMenu()
            {
                Active = false,
                Title = "Chat",
                FuncListener = FullHorizontalMenuNodeClicked,
                Parameter = typeof(Views.Project.Forum.Forum)
            };
            FullHorizontalMenu.Nodes.Add(nodeMenu3);

            NodeMenu nodeMenu4 = new NodeMenu()
            {
                Active = false,
                Title = "Equipe",
                FuncListener = FullHorizontalMenuNodeClicked,
                Parameter = typeof(Views.Project.Forum.Forum)
            };
            FullHorizontalMenu.Nodes.Add(nodeMenu4);
        }

        private async void EditProjectRedirect()
        {
            //HomePage.DahuFrame.Navigate(typeof(MainResearch), ResearchValue);
            HomePage.DahuFrame.Navigate(typeof(EditProject), Project);
        }

        private void FullHorizontalMenuNodeClicked(object parameter)
        {
            CurrentProjManagingPage = (Type)parameter;
        }

        private async void OnPageLoaded()
        {
            Project = (DahuUWP.Models.Project)NavigationParam;
            CurrentProjManagingPage = typeof(Views.Project.ScrumBoard.ScrumBoard);
        }

        private Type _currentProjManagingPage;
        public Type CurrentProjManagingPage
        {
            get { return _currentProjManagingPage; }
            set
            {
                NotifyPropertyChanged(ref _currentProjManagingPage, value);
            }
        }


        private DahuUWP.Models.Project _project;
        public DahuUWP.Models.Project Project
        {
            get { return _project; }
            set
            {
                NotifyPropertyChanged(ref _project, value);
            }
        }

        private Menu _fullHorizontalMenu;
        public Menu FullHorizontalMenu
        {
            get { return _fullHorizontalMenu; }
            set
            {
                NotifyPropertyChanged(ref _fullHorizontalMenu, value);
            }
        }
    }
}
