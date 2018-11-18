using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DahuUWP.Models
{
    public class Counterpart
    { 

        private string _uuid;
        [JsonProperty(PropertyName = "uuid")]
        public string Uuid
        {
            get
            {
                return _uuid;
            }

            set
            {
                _uuid = value;
                this.NotifyPropertyChanged("Uuid");
            }
        }

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

        private string _description;
        [JsonProperty(PropertyName = "description")]
        public string Description
        {
            get
            {
                return _description;
            }

            set
            {
                _description = value;
                this.NotifyPropertyChanged("Description");
            }
        }

        private string _projectUuid;
        [JsonProperty(PropertyName = "project_uuid")]
        public string ProjectUuid
        {
            get
            {
                return _projectUuid;
            }

            set
            {
                _projectUuid = value;
                this.NotifyPropertyChanged("ProjectUuid");
            }
        }

        private string _amount;
        [JsonProperty(PropertyName = "amount")]
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
