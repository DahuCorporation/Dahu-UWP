using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DahuUWP.Models
{
    public class Media
    {
        [JsonProperty(PropertyName = "uuid")]
        public string Uuid { get; set; }

        [JsonProperty(PropertyName = "name_for_user")]
        public string NameBinding { get; set; }

        [JsonProperty(PropertyName = "path")]
        public string ImagePath { get; set; }

        [JsonProperty(PropertyName = "user_uuid")]
        public string UserUuid { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }
    }
}
