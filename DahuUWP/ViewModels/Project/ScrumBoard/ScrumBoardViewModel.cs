using DahuUWP.DahuTech.Inputs;
using DahuUWP.DahuTech.ScrumBoard;
using DahuUWP.Services;
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

        public ScrumBoardViewModel(IDataService service)
        {
            dataService = service;

            OnPageLoadedCommand = new RelayCommand(OnPageLoaded);
        }

        private async void OnPageLoaded()
        {
            AddColumnButtonBindings = new DahuButtonBindings();
            AddColumnButtonBindings.Name = "Ajouter une colonne";
            AddColumnButtonBindings.TappedFuncListener = AddColumn;
            List<ScrumBoardTask> items1 = new List<ScrumBoardTask>
            {
                new ScrumBoardTask
                {
                    Id = 1,
                    Title = "Item 1.1"
                },
                new ScrumBoardTask
                {
                    Id = 2,
                    Title = "Item 1.2"
                },
                new ScrumBoardTask
                {
                    Id = 3,
                    Title = "Item 1.3"
                }
            };

            List<ScrumBoardTask> items2 = new List<ScrumBoardTask>
            {
                new ScrumBoardTask
                {
                    Id = 1,
                    Title = "Item 2.1"
                },
                new ScrumBoardTask
                {
                    Id = 2,
                    Title = "Item 2.2"
                },
                new ScrumBoardTask
                {
                    Id = 3,
                    Title = "Item 2.3"
                }
            };
            Columns = new ObservableCollection<ScrumBoardColumn>();
            ScrumBoardColumn sbc = new ScrumBoardColumn()
            {
                Id = 0,
                Title = "To do",
                Tasks = new ObservableCollection<ScrumBoardTask>(items1)
            };
            Columns.Add(sbc);
            ScrumBoardColumn sbc2 = new ScrumBoardColumn()
            {
                Id = 1,
                Title = "In progress",
                Tasks = new ObservableCollection<ScrumBoardTask>(items2)
            };
            Columns.Add(sbc2);
        }

        public async void AddColumn()
        {
            var res = new ResourceLoader();
            InputStringDialog dialog = new InputStringDialog();
            string name = await dialog.InputStringDialogAsync(res.GetString("AddColumn") , res.GetString("EnterAColumnName"), res.GetString("Add"), res.GetString("Cancel"));
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

        private ObservableCollection<ScrumBoardColumn> _columns;
        public ObservableCollection<ScrumBoardColumn> Columns
        {
            get { return _columns; }
            set
            {
                NotifyPropertyChanged(ref _columns, value);
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

    }
}
