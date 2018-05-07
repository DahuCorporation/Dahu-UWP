using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DahuUWP.DahuTech.ScrumBoard
{
    class ScrumBoard : INotifyPropertyChanged
    {

        private ObservableCollection<ScrumBoardColumn> _columns;
        public ObservableCollection<ScrumBoardColumn> Columns
        {
            get
            {
                return _columns;
            }

            set
            {
                _columns = value;
                this.NotifyPropertyChanged("Columns");
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
