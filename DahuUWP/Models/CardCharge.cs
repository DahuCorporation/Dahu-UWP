using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DahuUWP.Models
{
    public class CardCharge
    {
        private string _projectCounterpartUuid;
        [JsonProperty(PropertyName = "project_counterpart_uuid")]
        public string ProjectCounterpartUuid
        {
            get
            {
                return _projectCounterpartUuid;
            }

            set
            {
                _projectCounterpartUuid = value;
                this.NotifyPropertyChanged("ProjectCounterpartUuid");
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

        private string _lastName;
        [JsonProperty(PropertyName = "last_name")]
        public string LastName
        {
            get
            {
                return _lastName;
            }

            set
            {
                _lastName = value;
                this.NotifyPropertyChanged("LastName");
            }
        }

        private string _firstName;
        [JsonProperty(PropertyName = "first_name")]
        public string FirstName
        {
            get
            {
                return _firstName;
            }

            set
            {
                _firstName = value;
                this.NotifyPropertyChanged("FirstName");
            }
        }

        private string _cvc;
        [JsonProperty(PropertyName = "cvc")]
        public string Cvc
        {
            get
            {
                return _cvc;
            }

            set
            {
                _cvc = value;
                this.NotifyPropertyChanged("Cvc");
            }
        }

        private string _expYear;
        [JsonProperty(PropertyName = "exp_year")]
        public string ExpYear
        {
            get
            {
                return _expYear;
            }

            set
            {
                _expYear = value;
                this.NotifyPropertyChanged("ExpYear");
            }
        }

        private string _expMonth;
        [JsonProperty(PropertyName = "exp_month")]
        public string ExpMonth
        {
            get
            {
                return _expMonth;
            }

            set
            {
                _expMonth = value;
                this.NotifyPropertyChanged("ExpMonth");
            }
        }

        private string _cardNumber;
        [JsonProperty(PropertyName = "card_number")]
        public string CardNumber
        {
            get
            {
                return _cardNumber;
            }

            set
            {
                _cardNumber = value;
                this.NotifyPropertyChanged("CardNumber");
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
