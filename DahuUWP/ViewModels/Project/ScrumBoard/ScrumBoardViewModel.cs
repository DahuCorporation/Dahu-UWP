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
            Items1 = new ObservableCollection<ScrumBoardTask>(items1);
            Items2 = new ObservableCollection<ScrumBoardTask>(items2);
        }

        private ObservableCollection<ScrumBoardTask> _items1;
        public ObservableCollection<ScrumBoardTask> Items1
        {
            get { return _items1; }
            set
            {
                NotifyPropertyChanged(ref _items1, value);
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
