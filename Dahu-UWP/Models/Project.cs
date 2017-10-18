using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dahu_UWP.Models
{
    class Project
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "account_id")]
        public int AccountId { get; set; }

        [JsonProperty(PropertyName = "name")]
        public String Name { get; set; }

        [JsonProperty(PropertyName = "description")]
        public String Description { get; set; }

        [JsonProperty(PropertyName = "profile_picture")]
        public String ProfilePicture { get; set; }

        [JsonProperty(PropertyName = "banner_picture")]
        public String BannerPicture { get; set; }
        /*
        [JsonIgnore]
        public DateTime LastModified { get; set; }
        */
    }
}
