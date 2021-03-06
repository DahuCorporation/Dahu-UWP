﻿using DahuUWP.DahuTech.Inputs;
using DahuUWP.DahuTech.Selector;
using DahuUWP.Models;
using DahuUWP.Models.ModelManager;
using DahuUWP.Services;
using DahuUWP.Views;
using DahuUWP.Views.Project;
using DahuUWP.Views.Project.Contribute;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DahuUWP.ViewModels.Project.Contribute
{
    public class ContributeAdressCounterpartyViewModel : DahuViewModelBase
    {
        public ICommand OnPageLoadedCommand { get; private set; }

        public ContributeAdressCounterpartyViewModel(IDataService service)
        {
            dataService = service;
            OnPageLoadedCommand = new RelayCommand(OnPageLoaded);
        }

        private async void OnPageLoaded()
        {
            if (NavigationParam is Models.Contribute)
                Contribute = (Models.Contribute)NavigationParam;

            UserManager userManager = (UserManager)dataService.GetUserManager();

            Dictionary<string, object> userDicoCharge = new Dictionary<string, object>
            {
                { "_token", AppStaticInfo.Account.Token }
            };
            ContributeUser = await userManager.Charge(AppStaticInfo.Account.Uuid, userDicoCharge);
            SendToThisAdressBinding = new DahuButtonBindings
            {
                FuncListener = LinkToPayement
            };
            BackToProjectButtonBindings = new DahuButtonBindings
            {
                RedirectedLink = typeof(ProjectView),
                Parameter = Contribute.Project
            };
        }

        private void LinkToPayement(object param)
        {
            HomePage.DahuFrame.Navigate(typeof(ContributePayement), Contribute);
        }

        private DahuButtonBindings _backToProjectButtonBindings;
        public DahuButtonBindings BackToProjectButtonBindings
        {
            get { return _backToProjectButtonBindings; }
            set
            {
                NotifyPropertyChanged(ref _backToProjectButtonBindings, value);
            }
        }

        private User _contributeUser;
        public User ContributeUser
        {
            get { return _contributeUser; }
            set
            {
                NotifyPropertyChanged(ref _contributeUser, value);
            }
        }


        private DahuButtonBindings _sendToThisAdressBinding;
        public DahuButtonBindings SendToThisAdressBinding
        {
            get { return _sendToThisAdressBinding; }
            set
            {
                NotifyPropertyChanged(ref _sendToThisAdressBinding, value);
            }
        }



        private Models.Contribute _contribute;
        public Models.Contribute Contribute
        {
            get { return _contribute; }
            set
            {
                NotifyPropertyChanged(ref _contribute, value);
            }
        }
    }
}
