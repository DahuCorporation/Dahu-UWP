using DahuUWP.DahuTech;
using DahuUWP.DahuTech.Inputs;
using DahuUWP.DahuTech.Menu;
using DahuUWP.DahuTech.Project.Forum;
using DahuUWP.Models;
using DahuUWP.Models.ModelManager;
using DahuUWP.Services;
using DahuUWP.Services.ModelManager;
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
using Windows.ApplicationModel.Resources;
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
            ProjectManager projectManager = new ProjectManager();
            Project = await projectManager.ChargeOneProject(((DahuUWP.Models.Project)ViewModelLocator.HomePageViewModel.NavigationParam).Uuid);
            InitMenuWithSearch();
            SendMessageButtonBindings = new DahuButtonBindings
            {
                FuncListener = SendMessageOfTopic
            };
        }

        private async void MenuWithSearchNodeClicked(object parameter)
        {
            GetMessageOfTopic(parameter as Topic);
        }
            
        private async void SendMessageOfTopic(object param)
        {
            if (!String.IsNullOrWhiteSpace(MessageToSend))
            {
                foreach (NodeMenu nodeMenu in MenuWithSearch.Nodes)
                {
                    if (nodeMenu.Active)
                    {
                        ForumManager forumManager = new ForumManager();
                        await forumManager.CreateMessage(MessageToSend, Project.Uuid, (nodeMenu.Parameter as Topic).Uuid);

                        UserManager userManager = new UserManager();
                        User user = await userManager.Charge(AppStaticInfo.Account.Uuid);
                        TopicMessage message = new TopicMessage()
                        {
                            Content = MessageToSend
                        };
                        DahuUWP.DahuTech.Project.Forum.TopicMessageContainer topicMessage = new DahuTech.Project.Forum.TopicMessageContainer()
                        {
                            Message = message,
                            User = user
                        };
                        TopicMessages.Add(topicMessage);
                    }
                }
                
            }


        }

        private async void GetMessageOfTopic(Topic topic)
        {
            //TopicMessages.d
            if (topic == null)
                return;
            TopicMessages = new ObservableCollection<DahuTech.Project.Forum.TopicMessageContainer>();
            ForumManager forumManager = new ForumManager();
            List<TopicMessage> topicList = await forumManager.ChargeAllMessageOfTopics(Project.Uuid, topic.Uuid);
            if (topicList != null)
            {
                foreach (TopicMessage elem in topicList)
                {
                    UserManager userManager = new UserManager();
                    User user = await userManager.Charge(elem.AccountUuid);
                    TopicMessage message = new TopicMessage()
                    {
                        Content = elem.Content
                    };
                    DahuUWP.DahuTech.Project.Forum.TopicMessageContainer topicMessage = new DahuTech.Project.Forum.TopicMessageContainer()
                    {
                        Message = message,
                        User = user
                    };
                    TopicMessages.Add(topicMessage);
                }
            }
        //User user2 = new User()
        //    {
        //        FirstName = "Riclu",
        //        LastName = "Moufle"
        //    };
        //    TopicMessage message2 = new TopicMessage()
        //    {
        //        Content = "Hola la bas tout va bien ?",
        //    };
        //    DahuUWP.DahuTech.Project.Forum.TopicMessageContainer topicMessage2 = new DahuTech.Project.Forum.TopicMessageContainer()
        //    {
        //        Message = message2,
        //        User = user2
        //    };

        //    User user3 = new User()
        //    {
        //        FirstName = "Paul",
        //        LastName = "Kiry"
        //    };
        //    TopicMessage message3 = new TopicMessage()
        //    {
        //        Content = "Ah ouais la forme mon gars, j'ai passé une journée de fou a essayé de tous faire fonctionné. Une vraie galère mais finalement j'ai réussi à faire tourné la machine. Ça roule très bien en ce moment.",
        //    };
        //    DahuUWP.DahuTech.Project.Forum.TopicMessageContainer topicMessage3 = new DahuTech.Project.Forum.TopicMessageContainer()
        //    {
        //        Message = message3,
        //        User = user3
        //    };

        //    //TopicMessages.Add(topicMessage1);
        //    TopicMessages.Add(topicMessage2);
        //    TopicMessages.Add(topicMessage3);
        //    //TopicMessages.Add(topicMessage1);
        //    TopicMessages.Add(topicMessage2);
        //    TopicMessages.Add(topicMessage3);
        //    //TopicMessages.Add(topicMessage1);
        //    TopicMessages.Add(topicMessage2);
        //    TopicMessages.Add(topicMessage3);
        }

        public async void AddTopicNodeClicked(object parameter)
        {
            var res = new ResourceLoader();
            InputStringDialog dialog = new InputStringDialog();
            string name = await dialog.InputStringDialogAsync(res.GetString("AddATopic"), res.GetString("TopicName"), res.GetString("Add"), res.GetString("Cancel"));
            if (!String.IsNullOrWhiteSpace(name))
            {
                ForumManager forumManager = new ForumManager();
                Topic topic = await forumManager.CreateTopic(name, Project.Uuid);
                if (topic != null)
                {
                    NodeMenu node = new NodeMenu()
                    {
                        Title = topic.Name,
                        NodeTheme = Theme.Dark,
                        Active = false,
                        FuncListener = MenuWithSearchNodeClicked,
                        Parameter = topic
                    };
                    MenuWithSearch.Nodes.Add(node);
                }
            }
        }

        private async void InitMenuWithSearch()
        {
            MenuWithSearch = new MenuWithSearchAndButtonsContainer
            {
                Name = "Topics",
                MenuTheme = Theme.Dark
            };
            ForumManager forumManager = new ForumManager();
            List<Topic> topicList = await forumManager.ChargeAllTopicsOfProject(Project.Uuid);
            if (topicList != null)
            {
                foreach (Topic elem in topicList)
                {
                    NodeMenu node = new NodeMenu()
                    {
                        Title = elem.Name,
                        NodeTheme = Theme.Dark,
                        Active = false,
                        FuncListener = MenuWithSearchNodeClicked,
                        Parameter = elem
                    };
                    MenuWithSearch.Nodes.Add(node);
                }
            }
            if (MenuWithSearch.Nodes.Count > 0)
            {
                MenuWithSearch.Nodes[0].Active = true;
                GetMessageOfTopic(MenuWithSearch.Nodes[0].Parameter as Topic);
            }
            NodeMenu nodeButton = new NodeMenu()
            {
                Title = "+ Nouveau topic",
                NodeTheme = Theme.Dark,
                FuncListener = AddTopicNodeClicked
            };

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

        private DahuUWP.Models.Project _project;
        public DahuUWP.Models.Project Project
        {
            get { return _project; }
            set
            {
                NotifyPropertyChanged(ref _project, value);
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

        private DahuButtonBindings _sendMessageButtonBindings;
        public DahuButtonBindings SendMessageButtonBindings
        {
            get { return _sendMessageButtonBindings; }
            set
            {
                NotifyPropertyChanged(ref _sendMessageButtonBindings, value);
            }
        }

        private string _messageToSend;
        public string MessageToSend
        {
            get { return _messageToSend; }
            set
            {
                NotifyPropertyChanged(ref _messageToSend, value);
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
