using DahuUWP.DahuTech.Selector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DahuUWP.Models
{
    public class Contribute
    {
        private string _amount;
        public string Amount
        {
            get
            {
                return _amount;
            }

            set
            {
                _amount = value;
                this.NotifyPropertyChanged("Amount");
            }
        }

        private FlatSelectorElem _flatSelectorElem;
        public FlatSelectorElem FlatSelectorElem
        {
            get
            {
                return _flatSelectorElem;
            }

            set
            {
                _flatSelectorElem = value;
                this.NotifyPropertyChanged("FlatSelectorElem");
            }
        }

        private Project _project;
        public Project Project
        {
            get
            {
                return _project;
            }

            set
            {
                _project = value;
                this.NotifyPropertyChanged("Project");
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
}
