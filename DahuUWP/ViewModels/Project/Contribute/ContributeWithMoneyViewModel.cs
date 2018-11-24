using DahuUWP.DahuTech;
using DahuUWP.DahuTech.Inputs;
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
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DahuUWP.ViewModels.Project.Contribute
{
    public class ContributeWithMoneyViewModel : DahuViewModelBase
    {
        public ICommand OnPageLoadedCommand { get; private set; }

        public ContributeWithMoneyViewModel(IDataService service)
        {
            dataService = service;
            OnPageLoadedCommand = new RelayCommand(OnPageLoaded);
            //ChargeCounterparts();
        }

        private async void OnPageLoaded()
        {
            Project = (DahuUWP.Models.Project)NavigationParam;
            if (Project == null)
            {
                Project = ((FlatSelectorElem)NavigationParam).Project;
            }
            CounterpartList = new ObservableCollection<FlatSelectorElem>();
            foreach (Counterpart elem in Project.Counterparts)
            {
                string[] words = elem.Description.Split('.');
                if (words.Count() > 0)
                {
                    FlatSelectorElem flatElem = new FlatSelectorElem
                    {
                        Checked = Checked.False,
                        ContentElem = elem.Description.Replace(words[0], ""),
                        Price = elem.Amount,
                        Title = words[0],
                        Project = Project
                    };
                    CounterpartList.Add(flatElem);
                }
                else
                {
                    FlatSelectorElem flatElem = new FlatSelectorElem
                    {
                        Checked = Checked.Blocked,
                        ContentElem = elem.Description,
                        Price = elem.Amount,
                        Title = "",
                        Project = Project
                    };
                    CounterpartList.Add(flatElem);
                }
            }
            BackToProjectButtonBindings = new DahuButtonBindings
            {
                RedirectedLink = typeof(ProjectView),
                Parameter = Project
            };
            NextStepAdressButtonBindings = new DahuButtonBindings
            {
                FuncListener = LinkToAdress
            };
        }

        private async void LinkToAdress(object param)
        {
            ProjectManager projectManager = new ProjectManager();
            
            bool ok = false;
            foreach (FlatSelectorElem elem in CounterpartList)
            {
                if (elem.Checked == Checked.True)
                {
                    Models.Contribute contribute = new Models.Contribute
                    {
                        Amount = "",
                        FlatSelectorElem = elem,
                        Project = Project
                    };
                    ok = true;
                    HomePage.DahuFrame.Navigate(typeof(ContributeAdressCounterparty), contribute);
                }
            }
            try
            {
                if (!String.IsNullOrWhiteSpace(Amount))
                {
                    int a = Convert.ToInt32(Amount);
                    if (a > 0)
                    {
                        Models.Contribute contribute = new Models.Contribute
                        {
                            Amount = Amount,
                            FlatSelectorElem = null,
                            Project = Project
                        };
                        HomePage.DahuFrame.Navigate(typeof(ContributeAdressCounterparty), contribute);
                    }
                        
                    ok = true;
                }
            }
            catch (Exception ex)
            {
                AppGeneral.UserInterfaceStatusDico["Your amount is not a number."].Display();
            }
            if (!ok)
                AppGeneral.UserInterfaceStatusDico["You have not selected anything."].Display();
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

        private DahuButtonBindings _nextStepAdressButtonBindings;
        public DahuButtonBindings NextStepAdressButtonBindings
        {
            get { return _nextStepAdressButtonBindings; }
            set
            {
                NotifyPropertyChanged(ref _nextStepAdressButtonBindings, value);
            }
        }

        private ObservableCollection<FlatSelectorElem> _counterpartList;
        public ObservableCollection<FlatSelectorElem> CounterpartList
        {
            get { return _counterpartList; }
            set
            {
                NotifyPropertyChanged(ref _counterpartList, value);
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

        private string _amount;
        public string Amount
        {
            get { return _amount; }
            set
            {
                NotifyPropertyChanged(ref _amount, value);
            }
        }
    }
}
