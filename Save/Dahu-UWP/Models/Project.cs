using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dahu_UWP.Models
{
    public class Project
    {
        public string Prenom { get; set; }
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
            return new Project { Prenom = "Nico", Age = 30, EstBonClient = true };
        }
    }

    public class DesignServiceClient : IServiceClient
    {
        public Project Charger()
        {
            return new Project { Prenom = "Nico2", Age = 30, EstBonClient = true };
        }
    }

    /*
    public class Project
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
        
        //[JsonIgnore]
        //public DateTime LastModified { get; set; }
        
    }

    public interface IServiceProject
    {
        Project Charge();
    }

    public class ServiceProject : IServiceProject
    {
        public Project Charge()
        {
            return new Project { Id = 1, AccountId = 30, Name = "Dahu", Description = "Description de Dahu", ProfilePicture = "PICTURE", BannerPicture = "BANNER_PICTURE" };
        }
    }

    public class DesignServiceProject : IServiceProject
    {
        public Project Charge()
        {
            return new Project { Id = 1, AccountId = 30, Name = "Dahu", Description = "Description de Dahu", ProfilePicture = "PICTURE", BannerPicture = "BANNER_PICTURE" };
        }
    }
    */
}
