using Dahu_UWP.Utils.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dahu_UWP.Models
{
    class User
    {
        /// <summary>
        /// A la serialisation pour l'envoie à l'API ne prendre que l'id de l'account
        /// </summary>
        [JsonProperty(PropertyName = "account_id")]
        public Account Account { get; set; }

        [JsonProperty(PropertyName = "gender")]
        public UserUtility.Gender Gender;

        [JsonProperty(PropertyName = "first_name")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "last_name")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "birthdate")]
        public string Birthdate { get; set; }

        [JsonProperty(PropertyName = "biography")]
        public string Biography { get; set; }

        [JsonProperty(PropertyName = "adress")]
        public string Adress { get; set; }

        [JsonProperty(PropertyName = "postal_code")]
        public string PostalCode { get; set; }

        [JsonProperty(PropertyName = "city")]
        public string City { get; set; }

        [JsonProperty(PropertyName = "country")]
        public string Country { get; set; }

        [JsonProperty(PropertyName = "phone")]
        public string Phone { get; set; }
    }
}
