using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DahuUWP.Models
{
    public class Skill
    {
        [JsonProperty(PropertyName = "id")]
        public string Uuid { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        //pour lumen......
        [JsonProperty(PropertyName = "skill")]
        public Skill lumenSkill { get; set; }
    }
}
