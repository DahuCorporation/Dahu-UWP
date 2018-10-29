using DahuUWP.Utils.Enum;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DahuUWP.Utils.Converter.ColorConverter;

namespace DahuUWP.Models
{
    public class User
    {
        /// <summary>
        /// A la serialisation pour l'envoie à l'API ne prendre que l'id de l'account
        /// </summary>
        //[JsonProperty(PropertyName = "account_id")]
        [JsonIgnore]
        public Account Account { get; set; }

        //[JsonProperty(PropertyName = "gender")]
        [JsonIgnore]
        public Gender Gender { get; set; }

        [JsonProperty(PropertyName = "first_name")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "last_name")]
        public string LastName { get; set; }

        [JsonIgnore]
        public DateTime Birthdate { get; set; }

        [JsonProperty(PropertyName = "biography")]
        public string Biography { get; set; }

        [JsonProperty(PropertyName = "address")]
        public string Address { get; set; }

        [JsonProperty(PropertyName = "postal_code")]
        public string PostalCode { get; set; }

        [JsonProperty(PropertyName = "city")]
        public string City { get; set; }

        [JsonProperty(PropertyName = "country")]
        public string Country { get; set; }

        [JsonProperty(PropertyName = "phone")]
        public string Phone { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Mail { get; set; }

        [JsonProperty(PropertyName = "skills")]
        public List<Skill> Skills { get; set; }
    }
}
