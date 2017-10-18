using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dahu_UWP.Models
{
    class Account
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }

        [JsonProperty(PropertyName = "uuid")]
        public string UuId { get; set; }

        [JsonProperty(PropertyName = "cgu")]
        public int Cgu { get; set; }

        [JsonProperty(PropertyName = "validate")]
        public bool Validate { get; set; }

        /// <summary>
        /// Account type, is it a user or entreprise account
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public object Type { get; set; }

        [JsonProperty(PropertyName = "picture")]
        public string Picture { get; set; }
    }
}
