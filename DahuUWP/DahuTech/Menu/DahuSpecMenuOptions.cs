using DahuUWP.Views.Project.Managing;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace DahuUWP.DahuTech.Menu
{
    public class DahuSpecMenuOptions : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public DahuSpecMenuOptions(List<TopBarNodeMenu> listNodes)
        {
            ReasearchVisibility = Visibility.Collapsed;
            DahuSpecMenuVisibility = Visibility.Visible;
            NodesTopBarMenu = new ObservableCollection<TopBarNodeMenu>(listNodes);
        }

        public void SwitchOrActiveCurrentTopBarNodeMenu(Type pageLink)
        {
            ReasearchVisibility = Visibility.Collapsed;
            DahuSpecMenuVisibility = Visibility.Visible;
            foreach (TopBarNodeMenu node in NodesTopBarMenu)
            {
                node.HoverRectangleOpacity = (node.PageLink == pageLink) ? 100 : 0;
                node.IsActive = (node.PageLink == pageLink) ? true : false;
            }
        }

        private void InitNodesTopBarMenu()
        {
            
        }

        private Visibility _reasearchVisibility;
        public Visibility ReasearchVisibility
        {
            get
            {
                return _reasearchVisibility;
            }

            set
            {
                _reasearchVisibility = value;

                this.NotifyPropertyChanged("ReasearchVisibility");
            }
        }

        private Visibility _dahuSpecMenuVisibility;
        public Visibility DahuSpecMenuVisibility
        {
            get
            {
                return _dahuSpecMenuVisibility;
            }

            set
            {
                _dahuSpecMenuVisibility = value;

                this.NotifyPropertyChanged("DahuSpecMenuVisibility");
            }
        }

        private ObservableCollection<TopBarNodeMenu> _nodesTopBarMenu;
        public ObservableCollection<TopBarNodeMenu> NodesTopBarMenu
        {
            get { return _nodesTopBarMenu; }
            set
            {
                _nodesTopBarMenu = value;

                this.NotifyPropertyChanged("NodesTopBarMenu");
            }
        }

        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }


    }
}
