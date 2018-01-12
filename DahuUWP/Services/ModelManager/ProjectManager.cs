using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace DahuUWP.Models.ModelManager
{
    public class ProjectManager : IModelManager
    {
        public List<object> Charge(Dictionary<string, object> routeParams)
        {
            return new List<object>
            {
                new Project { Age = 30, EstBonClient = true, Prenom = "Nico"},
                new Project { Age = 20, EstBonClient = false, Prenom = "Jérémie"},
                new Project { Age = 30, EstBonClient = true, Prenom = "Delphine"}
            };
        }

        public bool Create(object obj)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int objId)
        {
            return false;
        }

        public object DeSerialize(JObject jObject)
        {
            throw new NotImplementedException();
        }

        public bool Edit(object obj)
        {
            throw new NotImplementedException();
        }

        public JObject Serialize()
        {
            throw new NotImplementedException();
        }
    }

    public class DesignProjectManager : IModelManager
    {
        public List<object> Charge(Dictionary<string, object> routeParams)
        {
            return new List<object>
            {
                new Project { Age = 26, EstBonClient = true, Prenom = "Nico Mode design"},
                new Project { Age = 18, EstBonClient = false, Prenom = "Jérémie Mode design"},
                new Project { Age = 24, EstBonClient = true, Prenom = "Delphine Mode design"}
            };
        }

        public bool Create(object obj)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int objId)
        {
            return true;
        }

        public object DeSerialize(JObject jObject)
        {
            throw new NotImplementedException();
        }

        public bool Edit(object obj)
        {
            throw new NotImplementedException();
        }

        public JObject Serialize()
        {
            throw new NotImplementedException();
        }
    }
}
