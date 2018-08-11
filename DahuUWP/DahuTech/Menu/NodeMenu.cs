using DahuUWP.ViewModels;
using DahuUWP.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace DahuUWP.DahuTech.Menu
{
    public class NodeMenu : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public NodeMenu()
        {
            Active = false;
        }

        private string _title;
        public string Title {

            get
            {
                return _title;
            }

            set
            {
               _title = value;

                this.NotifyPropertyChanged("Title");
            }
        }

        /// <summary>
        /// Cette fonction gère les liens du bouton, soit il redirige vers une autre page
        /// ou appel une fonction
        /// </summary>
        public void LinkIt()
        {
            if (PageLink != null)
            {
                HomePage.DahuFrame.Navigate(PageLink);
            } else if (FuncListener != null)
            {
                FuncListener.Invoke(Parameter);
            }
        }

        private Type _pageLink;
        public Type PageLink {
            get
            {
                return _pageLink;
            }

            set
            {
                _pageLink = value;

                this.NotifyPropertyChanged("PageLink");
            }
        }

        private Action<object> _funcListener;
        public Action<object> FuncListener
        {
            get
            {
                return _funcListener;
            }

            set
            {
                _funcListener = value;

                this.NotifyPropertyChanged("FuncListener");
            }
        }

        private object _parameter;
        public object Parameter
        {
            get
            {
                return _parameter;
            }

            set
            {
                _parameter = value;

                this.NotifyPropertyChanged("Parameter");
            }
        }

        private Theme _nodeTheme;
        public Theme NodeTheme
        {
            get
            {
                return _nodeTheme;
            }

            set
            {
                _nodeTheme = value;

                this.NotifyPropertyChanged("NodeTheme");
            }
        }

        private bool _active;
        public bool Active
        {

            get
            {
                return _active;
            }

            set
            {
                _active = value;

                this.NotifyPropertyChanged("Active");
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
