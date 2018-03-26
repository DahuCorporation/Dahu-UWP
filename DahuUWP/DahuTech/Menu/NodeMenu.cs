using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DahuUWP.DahuTech.Menu
{
    public class NodeMenu : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

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

        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
