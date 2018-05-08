using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DahuUWP.DahuTech.ScrumBoard
{
    public class ScrumBoardColumn : INotifyPropertyChanged
    {
        public int Id { get; set; }

        public string Title { get; set; }

        private ObservableCollection<ScrumBoardTask> _tasks;
        public ObservableCollection<ScrumBoardTask> Tasks
        {
            get
            {
                return _tasks;
            }

            set
            {
                _tasks = value;
                this.NotifyPropertyChanged("Tasks");
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
