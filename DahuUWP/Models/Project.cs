using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DahuUWP.Models
{
    public class Project
    {
        [JsonProperty(PropertyName = "prenom")]
        public string Prenom { get; set; }
        public string titi { get; set; }
        public int Age { get; set; }
        public bool EstBonClient { get; set; }
    }

    public interface IServiceClient
    {
        Project Charger();
    }

    public class ServiceClient : IServiceClient
    {
        public Project Charger()
        {
            return new Project { Prenom = "Nicodu33!", Age = 30, EstBonClient = true };
        }

    }

    public class DesignServiceClient : IServiceClient
    {
        public Project Charger()
        {
            Project tit = new Project();

            tit.Prenom = "efe";
            
            return new Project { Prenom = "Mode Design", Age = 30, EstBonClient = true };
        }
    }

}
