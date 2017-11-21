using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DahuUWP.Models
{
    public class Test
    {
        [JsonProperty(PropertyName = "prenom")]
        public string Prenom { get; set; }
        public string titi { get; set; }
        public int Age { get; set; }
        public bool EstBonClient { get; set; }
    }
}
