using DahuUWP.Models.ModelManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace DahuUWP.Services.ModelManager
{
    public class SkillManager : IModelManager
    {
        public List<object> Charge(Dictionary<string, object> routeParams)
        {
            throw new NotImplementedException();
        }

        public bool Create(object obj)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int objId)
        {
            throw new NotImplementedException();
        }

        public bool Edit(object obj)
        {
            throw new NotImplementedException();
        }

        public object DeSerialize(JObject jObject)
        {
            throw new NotImplementedException();
        }

        public JObject Serialize(object serializeObject)
        {
            throw new NotImplementedException();
        }
    }

    public class DesignSkillManager : IModelManager
    {
        public List<object> Charge(Dictionary<string, object> routeParams)
        {
            throw new NotImplementedException();
        }

        public bool Create(object obj)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int objId)
        {
            throw new NotImplementedException();
        }

        public object DeSerialize(JObject jObject)
        {
            throw new NotImplementedException();
        }

        public bool Edit(object obj)
        {
            throw new NotImplementedException();
        }

        public JObject Serialize(object serializeObject)
        {
            throw new NotImplementedException();
        }
    }
}
