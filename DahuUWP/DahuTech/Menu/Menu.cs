using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DahuUWP.DahuTech.Menu
{
    public class Menu : INotifyPropertyChanged
    {
        public string Name { get; set; }

        public ObservableCollection<NodeMenu> Nodes { get; set; }

        public Menu()
        {
            Nodes = new ObservableCollection<NodeMenu>();
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
