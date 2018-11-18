using DahuUWP.DahuTech;
using DahuUWP.DahuTech.Inputs;
using DahuUWP.DahuTech.Selector;
using DahuUWP.Models;
using DahuUWP.Models.ModelManager;
using DahuUWP.Services;
using DahuUWP.Services.ModelManager;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DahuUWP.ViewModels.Project.Managing
{
    public class EditProjectCounterpartsViewModel : DahuViewModelBase
    {
        public ICommand OnPageLoadedCommand { get; private set; }


        public EditProjectCounterpartsViewModel(IDataService service)
        {
            dataService = service;
            OnPageLoadedCommand = new RelayCommand(OnPageLoaded);
        }

        private async void OnPageLoaded()
        {
            Counterpart = new Counterpart();
            Project = (DahuUWP.Models.Project)NavigationParam;
            CounterpartsManager counterpartManager = new CounterpartsManager();

            List<Counterpart> counterparts = await counterpartManager.ChargeCounterpartsOfProject(Project.Uuid);
            //List<FlatSelectorElem> flatSelectors = new List<FlatSelectorElem>();
            CounterpartList = new ObservableCollection<FlatSelectorElem>();
            foreach (Counterpart elem in counterparts)
            {
                string[] words = elem.Description.Split('.');
                if (words.Count() > 0)
                {
                    FlatSelectorElem flatElem = new FlatSelectorElem
                    {
                        Checked = Checked.Blocked,
                        ContentElem = elem.Description.Replace(words[0], ""),
                        Price = elem.Amount,
                        Title = words[0]
                    };
                    CounterpartList.Add(flatElem);
                } else
                {
                    FlatSelectorElem flatElem = new FlatSelectorElem
                    {
                        Checked = Checked.Blocked,
                        ContentElem = elem.Description,
                        Price = elem.Amount,
                        Title = ""
                    };
                    CounterpartList.Add(flatElem);
                }
            }
            
            AddCounterpartButtonBindings = new DahuButtonBindings
            {
                FuncListener = AddCounterpart
            };
            EditGlobalCounterpartButtonBindings = new DahuButtonBindings
            {
                FuncListener = EditGlobalCounterpart
            };
        }

        public async void EditGlobalCounterpart(object param)
        {
            ProjectManager projectManager = new ProjectManager();
            int n;
            if (!String.IsNullOrWhiteSpace(Project.AmountGoal) && int.TryParse(Project.AmountGoal, out n))
            {
                await projectManager.EditProject(Project, Project.Uuid);
            }
            else
                AppGeneral.UserInterfaceStatusDico["Incorrect amount."].Display();
        }

        public async void AddCounterpart(object param)
        {
            CounterpartsManager counterpartManager = new CounterpartsManager();
            int n;
            if (!String.IsNullOrWhiteSpace(Counterpart.Amount) && !String.IsNullOrWhiteSpace(Counterpart.Description) && !String.IsNullOrWhiteSpace(Counterpart.Title) 
                && int.TryParse(Counterpart.Amount, out n))
            {
                await counterpartManager.Create(Counterpart.Amount, Counterpart.Title + ". " + Counterpart.Description, Project.Uuid);
                FlatSelectorElem flatElem = new FlatSelectorElem
                {
                    Checked = Checked.Blocked,
                    ContentElem = Counterpart.Description,
                    Price = Counterpart.Amount,
                    Title = Counterpart.Title
                };
                CounterpartList.Add(flatElem);
            }
            else
                AppGeneral.UserInterfaceStatusDico["Amount or description is null."].Display();
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

        

        private DahuButtonBindings _editGlobalCounterpartButtonBindings;
        public DahuButtonBindings EditGlobalCounterpartButtonBindings
        {
            get { return _editGlobalCounterpartButtonBindings; }
            set
            {
                NotifyPropertyChanged(ref _editGlobalCounterpartButtonBindings, value);
            }
        }

        private DahuButtonBindings _addCounterpartButtonBindings;
        public DahuButtonBindings AddCounterpartButtonBindings
        {
            get { return _addCounterpartButtonBindings; }
            set
            {
                NotifyPropertyChanged(ref _addCounterpartButtonBindings, value);
            }
        }

        private DahuUWP.Models.Counterpart _counterpart;
        public DahuUWP.Models.Counterpart Counterpart
        {
            get { return _counterpart; }
            set
            {
                NotifyPropertyChanged(ref _counterpart, value);
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
    }
}
