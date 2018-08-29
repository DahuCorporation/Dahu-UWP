using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DahuUWP.Models
{
    public class ScrumBoard : INotifyPropertyChanged
    {
        
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "uuid")]
        public string Uuid { get; set; }
        

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
