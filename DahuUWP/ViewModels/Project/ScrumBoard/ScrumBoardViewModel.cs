using DahuUWP.DahuTech;
using DahuUWP.DahuTech.Inputs;
using DahuUWP.DahuTech.Menu;
using DahuUWP.Models;
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
using Windows.ApplicationModel.Resources;

namespace DahuUWP.ViewModels.Project.ScrumBoard
{
    public class ScrumBoardViewModel : DahuViewModelBase
    {
        public ICommand OnPageLoadedCommand { get; private set; }

        public ICommand ScrumBoardNodeTappedCommand { get; set; }

        private List<Models.ScrumBoard> ScrumBoards;

        public ScrumBoardViewModel(IDataService service)
        {
            dataService = service;

            OnPageLoadedCommand = new RelayCommand(OnPageLoaded);
            ScrumBoardNodeTappedCommand = new RelayCommand<object>(ScrumBoardNodeTapped);
        }

        private async void OnPageLoaded()
        {
            InitLists();
            Project = (DahuUWP.Models.Project)ViewModelLocator.HomePageViewModel.NavigationParam;
            if (Project == null)
            {
                Project = (DahuUWP.Models.Project)NavigationParam;
            }
            ShowScrumBoardList();
            AddColumnButtonBindings = new DahuButtonBindings();
            AddColumnButtonBindings.Name = "Ajouter une colonne";
            AddColumnButtonBindings.FuncListener = AddColumn;

            AddScrumBoardButtonBindings = new DahuButtonBindings()
            {
                Name = "Ajouter un scrum board",
                FuncListener = AddScrumBoard
            };


            //List<ScrumBoardTask> items1 = new List<ScrumBoardTask>
            //{
            //    new ScrumBoardTask
            //    {
            //        Uuid = "1",
            //        Name = "Item 1.1"
            //    },
            //    new ScrumBoardTask
            //    {
            //        Uuid = "2",
            //        Name = "Item 1.2"
            //    },
            //    new ScrumBoardTask
            //    {
            //        Uuid = "3",
            //        Name = "Item 1.3"
            //    }
            //};

            //List<ScrumBoardTask> items2 = new List<ScrumBoardTask>
            //{
            //    new ScrumBoardTask
            //    {
            //        Uuid = "1",
            //        Name = "Item 2.1"
            //    },
            //    new ScrumBoardTask
            //    {
            //        Uuid = "2",
            //        Name = "Item 2.2"
            //    },
            //    new ScrumBoardTask
            //    {
            //        Uuid = "3",
            //        Name = "Item 2.3"
            //    }
            //};
            //ScrumBoardColumns = new ObservableCollection<ScrumBoardColumn>();
            //ScrumBoardColumn sbc = new ScrumBoardColumn()
            //{
            //    Uuid = "0",
            //    Name = "To do",
            //    Tasks = new ObservableCollection<ScrumBoardTask>(items1)
            //};
            //ScrumBoardColumns.Add(sbc);
            //ScrumBoardColumn sbc2 = new ScrumBoardColumn()
            //{
            //    Uuid = "0",
            //    Name = "In progress",
            //    Tasks = new ObservableCollection<ScrumBoardTask>(items2)
            //};
            //ScrumBoardColumns.Add(sbc2);
        }

        private void InitLists()
        {
            ScrumBoardList = new ObservableCollection<NodeMenu>();
        }

        private void ScrumBoardNodeTapped(object param)
        {
            NodeMenu selectedNodeMenu = param as NodeMenu;
            foreach (NodeMenu nodeMenu in ScrumBoardList)
            {
                if (nodeMenu.Active && nodeMenu != selectedNodeMenu)
                    nodeMenu.Active = false;
            }
            selectedNodeMenu.Active = true;
            selectedNodeMenu.LinkIt();
        }

        private async void ShowScrumBoardColumns(Models.ScrumBoard scrumBoard)
        {
            ScrumBoardManager scrumBoardManager = new ScrumBoardManager();

            List<ScrumBoardColumn> scrumBoardColumns = await scrumBoardManager.ChargeAllColumnsOfScrumBoard(scrumBoard.Uuid);
            ScrumBoardColumns = new ObservableCollection<ScrumBoardColumn>(scrumBoardColumns);
            if (scrumBoardColumns.Count == 0)
                return;
            //foreach (ScrumBoardColumn elem in ScrumBoardColumns)
            //{
            //    List<ScrumBoardTask> tasks = await scrumBoardManager.ChargeAllTaskOfColumn(elem.Uuid);
            //    if (tasks != null)
            //        elem.Tasks = new ObservableCollection<ScrumBoardTask>(tasks);
            //    //add task to each elem
            //}
        }

        private async void ShowScrumBoardList()
        {
            ScrumBoardManager scrumBoardManager = new ScrumBoardManager();
            ScrumBoards = await scrumBoardManager.ChargeAllScrumBoardOfProject(Project.Uuid);
            if (ScrumBoards == null)
                return;
            foreach (Models.ScrumBoard elem in ScrumBoards)
            {
                NodeMenu node = new NodeMenu()
                {
                    Title = elem.Name,
                    NodeTheme = Theme.Clear,
                    Active = false,
                    FuncListener = ChangeScrumBoard,
                    Parameter = null
                };
                ScrumBoardList.Add(node);
            }
            if (ScrumBoardList.Count > 0)
            {
                ScrumBoardList[0].Active = true;
                ShowScrumBoardColumns(ScrumBoards[0]);
            }

            //NodeMenu node3 = new NodeMenu()
            //{
            //    Title = "User needs",
            //    NodeTheme = Theme.Clear,
            //    Active = true,
            //    FuncListener = ChangeScrumBoard,
            //    Parameter = "Cuu"
            //};
            //NodeMenu node4 = new NodeMenu()
            //{
            //    Title = "Blobuou",
            //    NodeTheme = Theme.Clear,
            //    Active = false,
            //    FuncListener = ChangeScrumBoard,
            //    Parameter = "Looo"
            //};
            //ScrumBoardList.Add(node3);
            //ScrumBoardList.Add(node4);
        }

        private void ChangeScrumBoard(object parameter)
        {
            string tit = " ezf";

        }

        public async void AddScrumBoard(object param)
        {
            var res = new ResourceLoader();
            InputStringDialog dialog = new InputStringDialog();
            string name = await dialog.InputStringDialogAsync(res.GetString("AddScrumBoard"), res.GetString("EnterAScrumBoardName"), res.GetString("Add"), res.GetString("Cancel"));
            if (!String.IsNullOrWhiteSpace(name))
            {
                ScrumBoardManager scrumBoardManager = new ScrumBoardManager();
                Models.ScrumBoard scrumBoard = new Models.ScrumBoard()
                {
                    Name = name
                };
                scrumBoard = await scrumBoardManager.CreateScrumBoard(scrumBoard, Project.Uuid);
                if (scrumBoard != null)
                {
                    NodeMenu node = new NodeMenu()
                    {
                        Title = scrumBoard.Name,
                        NodeTheme = Theme.Clear,
                        Active = false,
                        FuncListener = ChangeScrumBoard,
                        Parameter = null
                    };
                    ScrumBoardList.Add(node);
                }

            }
        }

        public async void AddColumn(object param)
        {
            var res = new ResourceLoader();
            InputStringDialog dialog = new InputStringDialog();
            string name = await dialog.InputStringDialogAsync(res.GetString("AddColumn"), res.GetString("EnterAColumnName"), res.GetString("Add"), res.GetString("Cancel"));
            if (!String.IsNullOrWhiteSpace(name))
            {
                List<ScrumBoardTask> items2 = new List<ScrumBoardTask>
            {
                new ScrumBoardTask
                {
                    Uuid = "1",
                    Name = "Item 2.1"
                },
                new ScrumBoardTask
                {
                    Uuid = "2",
                    Name = "Item 2.2"
                },
                new ScrumBoardTask
                {
                    Uuid = "3",
                    Name = "Item 2.3"
                }
            };
                ScrumBoardColumn sbc2 = new ScrumBoardColumn()
                {
                    Uuid = "3",
                    Name = name,
                    Tasks = new ObservableCollection<ScrumBoardTask>(items2)
                };
                ScrumBoardColumns.Add(sbc2);
            }
            else
            {
                AppGeneral.UserInterfaceStatusDico["Scrum board name incorrect."].Display();
            }

        }

        private DahuButtonBindings _addColumnButtonBindings;
        public DahuButtonBindings AddColumnButtonBindings
        {
            get { return _addColumnButtonBindings; }
            set
            {
                NotifyPropertyChanged(ref _addColumnButtonBindings, value);
            }
        }

        private DahuButtonBindings _addScrumBoardButtonBindings;
        public DahuButtonBindings AddScrumBoardButtonBindings
        {
            get { return _addScrumBoardButtonBindings; }
            set
            {
                NotifyPropertyChanged(ref _addScrumBoardButtonBindings, value);
            }
        }


        public ObservableCollection<NodeMenu> _scrumBoardList;
        public ObservableCollection<NodeMenu> ScrumBoardList
        {
            get { return _scrumBoardList; }
            set
            {
                NotifyPropertyChanged(ref _scrumBoardList, value);
            }
        }

        //private ObservableCollection<DahuUWP.DahuTech.ScrumBoard.ScrumBoard> _scrumBoards;
        //public ObservableCollection<DahuUWP.DahuTech.ScrumBoard.ScrumBoard> ScrumBoards
        //{
        //    get { return _scrumBoards; }
        //    set
        //    {
        //        NotifyPropertyChanged(ref _scrumBoards, value);
        //    }
        //}

        private ObservableCollection<ScrumBoardColumn> _scrumBoardColumns;
        public ObservableCollection<ScrumBoardColumn> ScrumBoardColumns
        {
            get { return _scrumBoardColumns; }
            set
            {
                NotifyPropertyChanged(ref _scrumBoardColumns, value);
            }
        }

        private ObservableCollection<ScrumBoardTask> _items2;
        public ObservableCollection<ScrumBoardTask> Items2
        {
            get { return _items2; }
            set
            {
                NotifyPropertyChanged(ref _items2, value);
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
