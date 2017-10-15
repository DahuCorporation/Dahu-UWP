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
        
        [JsonProperty(PropertyName = "FooBar")]
        public string Foo { get; set; }
        [JsonIgnore]
        public DateTime LastModified { get; set; }

    }
}
