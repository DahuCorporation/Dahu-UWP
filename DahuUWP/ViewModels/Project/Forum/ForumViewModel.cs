using DahuUWP.DahuTech;
using DahuUWP.DahuTech.Menu;
using DahuUWP.DahuTech.Project.Forum;
using DahuUWP.Models;
using DahuUWP.Services;
using DahuUWP.Views.Components.Forum;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Input;

namespace DahuUWP.ViewModels.Project.Forum
{
    public class ForumViewModel : DahuViewModelBase
    {
        public ICommand OnPageLoadedCommand { get; private set; }

        public ForumViewModel(IDataService service)
        {
            dataService = service;

            OnPageLoadedCommand = new RelayCommand(OnPageLoaded);
        }

        private async void OnPageLoaded()
        {
            InitMenuWithSearch();
            GetMessageOfTopic(" Laviere");
        }

        private void MenuWithSearchNodeClicked(object parameter)
        {
            GetMessageOfTopic(" Rike");
        }

        private void GetMessageOfTopic(string test)
        {
            //TopicMessages.d
            TopicMessages = new ObservableCollection<DahuTech.Project.Forum.TopicMessageContainer>();
            User user1 = new User()
            {
                FirstName = "Julie",
                LastName = "Laracaille" + test
            };
            TopicMessage message1 = new TopicMessage()
            {
                Content = "Bonjour je suis le message n°1",
            };
            DahuUWP.DahuTech.Project.Forum.TopicMessageContainer topicMessage1 = new DahuTech.Project.Forum.TopicMessageContainer()
            {
                Message = message1,
                User = user1
            };

            User user2 = new User()
            {
                FirstName = "Riclu",
                LastName = "Moufle"
            };
            TopicMessage message2 = new TopicMessage()
            {
                Content = "Hola la bas tout va bien ?",
            };
            DahuUWP.DahuTech.Project.Forum.TopicMessageContainer topicMessage2 = new DahuTech.Project.Forum.TopicMessageContainer()
            {
                Message = message2,
                User = user2
            };

            User user3 = new User()
            {
                FirstName = "Paul",
                LastName = "Kiry"
            };
            TopicMessage message3 = new TopicMessage()
            {
                Content = "Ah ouais la forme mon gars, j'ai passé une journée de fou a essayé de tous faire fonctionné. Une vraie galère mais finalement j'ai réussi à faire tourné la machine. Ça roule très bien en ce moment.",
            };
            DahuUWP.DahuTech.Project.Forum.TopicMessageContainer topicMessage3 = new DahuTech.Project.Forum.TopicMessageContainer()
            {
                Message = message3,
                User = user3
            };

            TopicMessages.Add(topicMessage1);
            TopicMessages.Add(topicMessage2);
            TopicMessages.Add(topicMessage3);
            TopicMessages.Add(topicMessage1);
            TopicMessages.Add(topicMessage2);
            TopicMessages.Add(topicMessage3);
            TopicMessages.Add(topicMessage1);
            TopicMessages.Add(topicMessage2);
            TopicMessages.Add(topicMessage3);
        }

        private void InitMenuWithSearch()
        {
            MenuWithSearch = new MenuWithSearchAndButtonsContainer();
            MenuWithSearch.Name = "Topics";
            MenuWithSearch.MenuTheme = Theme.Dark;
            NodeMenu node = new NodeMenu()
            {
                Title = "General",
                NodeTheme = Theme.Dark,
                Active = true,
                FuncListener = MenuWithSearchNodeClicked
            };
            NodeMenu node2 = new NodeMenu()
            {
                Title = "Design",
                NodeTheme = Theme.Dark,
                Active = false,
                FuncListener = MenuWithSearchNodeClicked
            };
            NodeMenu node3 = new NodeMenu()
            {
                Title = "User needs",
                NodeTheme = Theme.Dark,
                Active = false,
                FuncListener = MenuWithSearchNodeClicked
            };
            NodeMenu nodeButton = new NodeMenu()
            {
                Title = "+ Nouveau topic",
                NodeTheme = Theme.Dark
            };
            MenuWithSearch.Nodes.Add(node);
            MenuWithSearch.Nodes.Add(node2);
            MenuWithSearch.Nodes.Add(node3);
            MenuWithSearch.Buttons.Add(nodeButton);
        }

        private MenuWithSearchAndButtonsContainer _menuWithSearch;
        public MenuWithSearchAndButtonsContainer MenuWithSearch
        {
            get { return _menuWithSearch; }
            set
            {
                NotifyPropertyChanged(ref _menuWithSearch, value);
            }
        }

        private ObservableCollection<DahuUWP.DahuTech.Project.Forum.TopicMessageContainer> _topicMessages;
        public ObservableCollection<DahuUWP.DahuTech.Project.Forum.TopicMessageContainer> TopicMessages
        {
            get { return _topicMessages; }
            set
            {
                NotifyPropertyChanged(ref _topicMessages, value);
            }
        }

        private bool NotifyPropertyChanged<T>(ref T variable, T valeur, [CallerMemberName] string nomPropriete = null)
        {
            if (object.Equals(variable, valeur)) return false;

            variable = valeur;
            RaisePropertyChanged(nomPropriete);
            return true;
        }
    }
}
