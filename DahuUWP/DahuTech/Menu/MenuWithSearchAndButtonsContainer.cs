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
    public class MenuWithSearchAndButtonsContainer : Menu
    {
        public ObservableCollection<NodeMenu> Buttons { get; set; }

        public bool SearchOption { get; set; }

        private Theme _menuTheme;
        public Theme MenuTheme
        {
            get
            {
                return _menuTheme;
            }

            set
            {
                _menuTheme = value;
                this.NotifyPropertyChanged("MenuTheme");
            }
        }

        public MenuWithSearchAndButtonsContainer()
        {
            Buttons = new ObservableCollection<NodeMenu>();
        }
    }
}
