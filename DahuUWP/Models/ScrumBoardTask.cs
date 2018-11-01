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
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

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

        public DahuButtonBindings DeleteTaskButtonBindings;

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
