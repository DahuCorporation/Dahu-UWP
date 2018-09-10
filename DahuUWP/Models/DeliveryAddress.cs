using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DahuUWP.Models
{
    public class DeliveryAddress
    {
        public string Name { get; set; }

        public string FirstName { get; set; }

        public string Address { get; set; }

        [JsonProperty(PropertyName = "postal_code")]
        public string PostalCode { get; set; }

        [JsonProperty(PropertyName = "city")]
        public string City { get; set; }

        [JsonProperty(PropertyName = "country")]
        public string Country { get; set; }

        public bool PrincipalAddress { get; set; }
    }
}
