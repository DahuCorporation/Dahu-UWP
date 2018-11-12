using DahuUWP.DahuTech.Inputs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DahuUWP.Models
{
    public class ScrumBoardTask : INotifyPropertyChanged
    {
        private string _name;

        [JsonProperty(PropertyName = "name")]
        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
                this.NotifyPropertyChanged("Name");
            }
        }

        [JsonProperty(PropertyName = "id")]
        public string Uuid { get; set; }

        [JsonProperty(PropertyName = "order")]
        public int Order { get; set; }

        [JsonProperty(PropertyName = "board_id")]
        public string ScrumBoardUuid { get; set; }

        [JsonProperty(PropertyName = "column_id")]
        public string ScrumBoardColumnUuid { get; set; }

        [JsonProperty(PropertyName = "label_uuid")]
        public string LabelUuid { get; set; }

        [JsonProperty(PropertyName = "column")]
        public ScrumBoardColumn Column { get; set; }

        private DahuButtonBindings _renameTaskButtonBindings;
        public DahuButtonBindings RenameTaskButtonBindings
        {
            get
            {
                return _renameTaskButtonBindings;
            }

            set
            {
                _renameTaskButtonBindings = value;
                this.NotifyPropertyChanged("RenameTaskButtonBindings");
            }
        }

        private DahuButtonBindings _deleteTaskButtonBindings;
        public DahuButtonBindings DeleteTaskButtonBindings
        {
            get
            {
                return _deleteTaskButtonBindings;
            }

            set
            {
                _deleteTaskButtonBindings = value;
                this.NotifyPropertyChanged("DeleteTaskButtonBindings");
            }
        }

        //public string Title { get; set; }

        //public int Id { get; set; }

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
