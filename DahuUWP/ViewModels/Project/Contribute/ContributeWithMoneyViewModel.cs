using DahuUWP.DahuTech.Selector;
using DahuUWP.Services;
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
        }

        private async void OnPageLoaded()
        {
            
            FlatSelectorElem counterpartElem1 = new FlatSelectorElem()
            {
                Title = "Une place de film",
                ContentElem = "Un grand merci pour votre participation ! Vous recevrez une place pour aller voir le film près de chez vous.",
                Price = "10"
            };
            FlatSelectorElem counterpartElem2 = new FlatSelectorElem()
            {
                Title = "Sac à main",
                ContentElem = "Un magnifique sac \"C'est Ma Cabane !\" (en exclusivité dans cette contrepartie) + un badge \"C'est Ma Cabane !\"",
                Price = "20"
            };
            FlatSelectorElem counterpartElem3 = new FlatSelectorElem()
            {
                Title = "Visiter une cabane",
                ContentElem = "Venez visiter une de nos construction et découvrez comment nous mettons en place nos cabanes. Un diner vous est offert.",
                Price = "45"
            };
            List<FlatSelectorElem> tempFlatSelectorElemList = new List<FlatSelectorElem>();
            tempFlatSelectorElemList.Add(counterpartElem1);
            tempFlatSelectorElemList.Add(counterpartElem2);
            tempFlatSelectorElemList.Add(counterpartElem3);
            CounterpartList = new ObservableCollection<FlatSelectorElem>(tempFlatSelectorElemList);
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
    }
}
