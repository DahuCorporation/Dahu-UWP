using DahuUWP.DahuTech;
using DahuUWP.DahuTech.Inputs;
using DahuUWP.DahuTech.Menu;
using DahuUWP.Models;
using DahuUWP.Models.ModelManager;
using DahuUWP.Services;
using DahuUWP.Services.ModelManager;
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

namespace DahuUWP.ViewModels.Project.Chat
{
    public class ChatViewModel : DahuViewModelBase
    {
        public ICommand OnPageLoadedCommand { get; private set; }

        public ChatViewModel(IDataService service)
        {
            dataService = service;

            OnPageLoadedCommand = new RelayCommand(OnPageLoaded);
        }

        private async void OnPageLoaded()
        {
            TopicMessages = new ObservableCollection<DahuTech.Project.Forum.TopicMessageContainer>();
            Project = (DahuUWP.Models.Project)ViewModelLocator.HomePageViewModel.NavigationParam;
            if (Project == null)
            {
                Project = (DahuUWP.Models.Project)NavigationParam;
            }

            ChargeMessages();
            InitButtonBindings();
            InitMenuWithSearch();
        }

        public async void ChargeMessages()
        {
            ChatManager chatManager = new ChatManager();
            

            List<Models.Chat> chatMessages = await chatManager.ChargeMessages(Project.Uuid, 0, 10);
            chatMessages.Reverse();
            foreach (Models.Chat msg in chatMessages)
            {
                DahuUWP.DahuTech.Project.Forum.TopicMessageContainer topicMessage = new DahuTech.Project.Forum.TopicMessageContainer()
                {
                    Message = new TopicMessage
                    {
                        Content = msg.Message
                    },
                    User = msg.User
                };
                TopicMessages.Add(topicMessage);
            }
        }

        private void InitButtonBindings()
        {
            SendMessageButtonBindings = new DahuButtonBindings()
            {
                Name = "Envoyer",
                FuncListener = SendMessage
            };
        }


        public async void SendMessage(object param)
        {
            if (!String.IsNullOrEmpty(MessageToSendValue))
            {
                ChatManager chatManager = new ChatManager();
                UserManager userManager = new UserManager();

                bool ret = await chatManager.CreateMessage(MessageToSendValue, Project.Uuid);
                DahuUWP.DahuTech.Project.Forum.TopicMessageContainer topicMessage = new DahuTech.Project.Forum.TopicMessageContainer()
                {
                    Message = new TopicMessage
                    {
                        Content = MessageToSendValue
                    },
                    User = await userManager.Charge(AppStaticInfo.Account.Uuid)
                };
                TopicMessages.Add(topicMessage);
                MessageToSendValue = "";
            } else
            {
                AppGeneral.UserInterfaceStatusDico["Wrong message format."].Display();
            }
            
        }



        public async void AddTopicNodeClicked(object parameter)
        {
            var res = new ResourceLoader();
            InputStringDialog dialog = new InputStringDialog();
            string name = await dialog.InputStringDialogAsync(res.GetString("AddATopic"), res.GetString("TopicName"), res.GetString("Add"), res.GetString("Cancel"));
        }

        private void InitMenuWithSearch()
        {
            MenuWithSearch = new MenuWithSearchAndButtonsContainer
            {
                Name = "Discussion",
                MenuTheme = Theme.Dark
            };
            NodeMenu node = new NodeMenu()
            {
                Title = "General",
                NodeTheme = Theme.Dark,
                Active = true
            };
            MenuWithSearch.Nodes.Add(node);
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

        private DahuButtonBindings _sendMessageButtonBindings;
        public DahuButtonBindings SendMessageButtonBindings
        {
            get { return _sendMessageButtonBindings; }
            set
            {
                NotifyPropertyChanged(ref _sendMessageButtonBindings, value);
            }
        }

        private string _messageToSendValue;
        public string MessageToSendValue
        {
            get { return _messageToSendValue; }
            set
            {
                NotifyPropertyChanged(ref _messageToSendValue, value);
            }
        }

        private bool NotifyPropertyChanged<T>(ref T variable, T valeur, [CallerMemberName] string nomPropriete = null)
        {
            if (object.Equals(variable, valeur)) return false;

            variable = valeur;
            RaisePropertyChanged(nomPropriete);
            return true;
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
