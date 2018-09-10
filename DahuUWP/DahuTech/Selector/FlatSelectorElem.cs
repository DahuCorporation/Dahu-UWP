using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DahuUWP.DahuTech.Selector
{
    public class FlatSelectorElem : INotifyPropertyChanged
    {
        private string _title;
        public string Title
        {
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

        private string _contentElem;
        public string ContentElem
        {
            get
            {
                return _contentElem;
            }

            set
            {
                _contentElem = value;

                this.NotifyPropertyChanged("ContentElem");
            }
        }

        private string _price;
        public string Price
        {
            get
            {
                return _price;
            }

            set
            {
                _price = value;

                this.NotifyPropertyChanged("Price");
            }
        }

        private Checked _checked;
        public Checked Checked
        {
            get
            {
                return _checked;
            }

            set
            {
                _checked = value;

                this.NotifyPropertyChanged("Checked");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    public enum Checked { False, True, Blocked, Hover };
}
