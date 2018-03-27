using DahuUWP.Views.Project.Managing;
using System;
using System.Collections.Generic;
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

        private Type _activeLink;
        public Type ActiveLink
        {
            get
            {
                return _activeLink;
            }

            set
            {
                _activeLink = value;

                this.NotifyPropertyChanged("ActiveLink");
            }
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

        private Visibility _menuVisibility;
        public Visibility MenuVisibility
        {
            get
            {
                return _menuVisibility;
            }

            set
            {
                _menuVisibility = value;

                this.NotifyPropertyChanged("MenuVisibility");
            }
        }

        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public DahuSpecMenuOptions()
        {
            ReasearchVisibility = Visibility.Collapsed;
            MenuVisibility = Visibility.Collapsed;
            ActiveLink = typeof(ManageProject);
        }
    }
}
