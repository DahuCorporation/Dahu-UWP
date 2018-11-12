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
            ScrumBoardList = null;
            ScrumBoardColumns = null;
            InitLists();
            Project = (DahuUWP.Models.Project)ViewModelLocator.HomePageViewModel.NavigationParam;
            if (Project == null)
            {
                Project = (DahuUWP.Models.Project)NavigationParam;
            }
            ShowScrumBoardList();

            AddScrumBoardButtonBindings = new DahuButtonBindings()
            {
                Name = "Ajouter un scrum board",
                FuncListener = AddScrumBoard
            };
            DahuSpecSplitMenuButtonBindings = new DahuButtonBindings()
            {
                Name = "OK"
            };
        }

        private void InitLists()
        {
            ScrumBoardList = new ObservableCollection<NodeMenu>();
        }

        private void ScrumBoardNodeTapped(object param)
        {
            NodeMenu selectedNodeMenu = param as NodeMenu;
            NodeMenu activeLink = ScrumBoardList.FirstOrDefault(w => w.Active == true);
            if (activeLink != selectedNodeMenu)
            {
                foreach (NodeMenu nodeMenu in ScrumBoardList)
                {
                    if (nodeMenu.Active && nodeMenu != selectedNodeMenu)
                        nodeMenu.Active = false;
                }
                selectedNodeMenu.Active = true;
                selectedNodeMenu.LinkIt();
            }
        }

        private async void ShowScrumBoardColumns(string scrumBoardUuid)
        {
            ScrumBoardManager scrumBoardManager = new ScrumBoardManager();

            List<ScrumBoardColumn> scrumBoardColumns = await scrumBoardManager.ChargeAllColumnsOfScrumBoard(scrumBoardUuid);
            ScrumBoardColumns = new ObservableCollection<ScrumBoardColumn>(scrumBoardColumns);
            if (scrumBoardColumns.Count != 0)
            {
                foreach (ScrumBoardColumn elem in ScrumBoardColumns)
                {
                    elem.ScrumBoardUuid = scrumBoardUuid;
                }
            }
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
                    Parameter = elem.Uuid,
                    AddColumnButtonBindings = new DahuButtonBindings()
                    {
                        Name = "Ajouter une colonne",
                        FuncListener = AddColumn
                    },
                    RenameScrumBoardButtonBindings = new DahuButtonBindings()
                    {
                        Name = "Renommer un board",
                        FuncListener = RenameColumn
                    },
                    DeleteScrumBoardButtonBindings = new DahuButtonBindings()
                    {
                        Name = "Suppromer un board",
                        FuncListener = DeleteColumn
                    }
            };
                ScrumBoardList.Add(node);
            }
            if (ScrumBoardList.Count > 0)
            {
                ScrumBoardList[0].Active = true;
                ShowScrumBoardColumns(ScrumBoards[0].Uuid);
            }
        }

        private void ChangeScrumBoard(object parameter)
        {
            ShowScrumBoardColumns(parameter.ToString());
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
                    ScrumBoards.Add(scrumBoard);
                    NodeMenu node = new NodeMenu()
                    {
                        Title = scrumBoard.Name,
                        NodeTheme = Theme.Clear,
                        Active = false,
                        FuncListener = ChangeScrumBoard,
                        Parameter = scrumBoard.Uuid,
                        AddColumnButtonBindings = new DahuButtonBindings()
                        {
                            Name = "Ajouter une colonne",
                            FuncListener = AddColumn
                        },
                        RenameScrumBoardButtonBindings = new DahuButtonBindings()
                        {
                            Name = "Renommer un board",
                            FuncListener = RenameColumn
                        },
                        DeleteScrumBoardButtonBindings = new DahuButtonBindings()
                        {
                            Name = "Supprimer un board",
                            FuncListener = DeleteColumn
                        }
                    };
                    ScrumBoardList.Add(node);
                }

            }
        }
        public async void RenameColumn(object param)
        {
            var res = new ResourceLoader();
            InputStringDialog dialog = new InputStringDialog();
            string rename = await dialog.InputStringDialogAsync("Renommer le scrum board", param.ToString(), res.GetString("Rename"), res.GetString("Cancel"));
            if (!String.IsNullOrEmpty(rename) && rename != param.ToString())
            {
                ScrumBoardManager scrumBoardManager = new ScrumBoardManager();
                Models.ScrumBoard board = ScrumBoards.First(x => x.Name == param.ToString());
                await scrumBoardManager.EditBoard(rename, board.Uuid);
                NodeMenu node = ScrumBoardList.First(x => x.Title == param.ToString());
                node.Title = rename;

            }
        }
        public async void DeleteColumn(object param)
        {
            var res = new ResourceLoader();
            InputStringDialog dialog = new InputStringDialog();
            bool delete = await dialog.AskDialogAsync("Supprimer le scrum board ?", "", res.GetString("Delete"), res.GetString("Cancel"));
            if (delete)
            {
                ScrumBoardManager scrumBoardManager = new ScrumBoardManager();
                Models.ScrumBoard board = ScrumBoards.First(x => x.Name == param.ToString());
                await scrumBoardManager.DeleteScrumBoard(board.Uuid);
                NodeMenu node = ScrumBoardList.First(x => x.Title == param.ToString());
                ScrumBoardList.Remove(node);
            }
        }
        public async void AddColumn(object param)
        {
            var res = new ResourceLoader();
            InputStringDialog dialog = new InputStringDialog();
            string name = await dialog.InputStringDialogAsync(res.GetString("AddColumn"), res.GetString("EnterAColumnName"), res.GetString("Add"), res.GetString("Cancel"));
            if (!String.IsNullOrWhiteSpace(name))
            {
                NodeMenu node = ScrumBoardList.First(x => x.Active == true);
                if (node != null && ScrumBoards.Count > 0)
                {
                    ScrumBoardManager scrumBoardManager = new ScrumBoardManager();
                    Models.ScrumBoard board = ScrumBoards.FirstOrDefault(x => x.Name == param.ToString());
                    ScrumBoardColumn column = await scrumBoardManager.CreateAColumn(name, board.Uuid, ScrumBoards.Count);

                    ScrumBoardColumns.Add(column);
                }
                //await scrumBoardManager.CreateAColumn(name, scrumBoardId);
                //ScrumBoardColumns.Add(sbc2);
            }
            else
            {
                AppGeneral.UserInterfaceStatusDico["Scrum board name incorrect."].Display();
            }

        }

        private DahuButtonBindings _dahuSpecSplitMenuButtonBindings;
        public DahuButtonBindings DahuSpecSplitMenuButtonBindings
        {
            get { return _dahuSpecSplitMenuButtonBindings; }
            set
            {
                NotifyPropertyChanged(ref _dahuSpecSplitMenuButtonBindings, value);
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
