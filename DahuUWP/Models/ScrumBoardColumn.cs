using DahuUWP.DahuTech.Inputs;
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
    public class ScrumBoardColumn : INotifyPropertyChanged
    {

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "id")]
        public string Uuid { get; set; }

        [JsonProperty(PropertyName = "order")]
        public int Order { get; set; }

        [JsonProperty(PropertyName = "board_id")]
        public string ScrumBoardUuid { get; set; }

        //public int Id { get; set; }

        //public string Title { get; set; }

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
